using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using scheduler.service.IService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace webAppAnguler.Filters
{
    public class CustomAuth : AuthorizeAttribute, IAuthorizationFilter
    {
       // private readonly IUnitOfWork _unitOfWork;
        private IHttpContextAccessor httpContextAccessor;
        private readonly ICaltUserService _userServices;
        // public CustomAuth(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        public CustomAuth(ICaltUserService userServices, IHttpContextAccessor httpContextAccessor)
        {
            _userServices = userServices;
            this.httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext actionContext)
        {
            //this.httpContextAccessor = httpContextAccessor;

            //HttpRequest request = actionContext.HttpContext.Request;
            try
            {
                var authtoken = actionContext.HttpContext.Request.Headers.SingleOrDefault(x => x.Key == "Authentication");
                if (authtoken.Key != null)
                {
                    var tokenarr = Convert.ToString(authtoken.Value.SingleOrDefault()).Split(':');
                    if (tokenarr.Length > 1 && tokenarr[0] == "Bearer")
                    {
                        string tokenstring = tokenarr[1].ToString();
                        if (!IsAuthorized(tokenstring))
                        {
                            actionContext.Result = new UnauthorizedResult();

                            return;
                        }
                        else
                        {
                            httpContextAccessor.HttpContext.Session.SetString("Userdetail", GetUserDetail(tokenstring).ToString());
                            //actionContext.RouteData.Values.Add("UserDetail", JsonConvert.DeserializeObject(GetUserDetail(tokenstring).ToString()));
                        }
                        return;
                    }
                    else
                    {
                        actionContext.Result = new UnauthorizedResult();

                    }
                }
                else
                {
                    //actionContext.HttpContext.Response = request.CreateResponse(System.Net.HttpStatusCode.Forbidden, "Auth Token is missing. Please provide a auth token");
                    actionContext.Result = new ForbidResult();
                }
            }
            catch (Exception ex)
            {
                //actionContext.Response = request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, "Internal Server Error");
                actionContext.Result = new UnauthorizedResult();

            }

        }

        private bool IsAuthorized(string tokenstring)
        {
           // return true;
            try
            {
                JwtSecurityTokenHandler tokenhandler = new JwtSecurityTokenHandler();
                if (tokenhandler.CanReadToken(tokenstring))
                {
                    JwtSecurityToken token = tokenhandler.ReadJwtToken(tokenstring);
                    JwtPayload payload = token.Payload;
                    if (payload.Count() > 0 && payload["Username"] != null)
                    {
                        string Username = payload["Username"].ToString();

                        //string role = payload["Role"].ToString();
                        //if (Roles != null)
                        //{
                        //    if (!Roles.Contains(role))
                        //    {
                        //        return false;
                        //    }
                        //}

                        var data = _userServices.GetuserByCustomuserName(Username).data;
                        if (data != null)
                        {
                            //Setting Userid in Session
                            //this.httpContextAccessor.HttpContext.Session.SetString("UserId", data.Id.ToString());


                            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF32.GetBytes(Username));
                            var validationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = securityKey,
                                ValidateAudience = false,
                                ValidateIssuer = false,
                                ValidateActor = false,
                                ValidateLifetime = true,
                                ValidateTokenReplay = false,
                                LifetimeValidator = LifetimeValidator
                            };
                            SecurityToken validatedToken;
                            try
                            {
                                tokenhandler.ValidateToken(tokenstring, validationParameters, out validatedToken);
                            }
                            catch (Exception ex)
                            {
                                return false;
                            }
                            return validatedToken != null;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        private bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters @params)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }

        private object GetUserDetail(string tokenstring)
        {
            JwtSecurityTokenHandler tokenhandler = new JwtSecurityTokenHandler();
            if (tokenhandler.CanReadToken(tokenstring))
            {
                JwtSecurityToken token = tokenhandler.ReadJwtToken(tokenstring);
                JwtPayload payload = token.Payload;
                if (payload.Count() > 0 && payload["Userrecord"] != null)
                {
                    var Userrecord = payload["Userrecord"].ToString();
                    return Userrecord;
                }
            }
            return null;
        }
    }
}
