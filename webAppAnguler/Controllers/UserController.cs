using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using scheduler.Model.ViewModel;
using scheduler.service.Common_Utilities;
using scheduler.service.IService;
using webAppAnguler.Filters;


namespace webAppAnguler.Controllers
{
   
        [ApiController]
        [TypeFilter(typeof(CustomException))]
        public class UserController 
        {
       private readonly ICaltUserService _userServices;
        public UserController(ICaltUserService userServices)
        {
            _userServices = userServices;
        }
        [HttpPost]
        [Route("api/User/login")]
        public async Task<ResponseModel> userLogin([FromBody] LoginViewModel user)
        {

            var detail = _userServices.FindUser(user.Username, user.Password);
            return await Task.FromResult(detail);
        }

        [HttpGet]
        [Route("api/User/forgotpassword")]
        public async Task<ResponseModel> ForgetPassword(string Username)
        {
            return await Task.FromResult(_userServices.Forgotpassword(Username));
        }
        [HttpGet]
        [Route("api/User/resetpassword")]
        public async Task<ResponseModel> ResetPassword(string email,string password)
        {
          
            return await Task.FromResult(_userServices.Forgotpassword(email+","+password));
        }
        // [TypeFilter(typeof(CustomAuth))]resetpassword
        // [Route("api/User/setpassword")]
        // public async Task<ResponseModel> SetPassword([FromBody] SetPasswordModel user)
        // {
        //     var result = _userServices.SetPassword(user);
        //     return await Task.FromResult(result);
        // }
        // [TypeFilter(typeof(CustomAuth))]
        // [Route("api/User/resetpassword")]
        // public async Task<ResponseModel> ResetPassword([FromBody] resetViewModel user)
        // {
        //     var result = _userServices.ResetPassword(user);
        //     return await Task.FromResult(result);
        // }
        // [TypeFilter(typeof(CustomAuth))]
        // [Route("api/User/changepassword")]
        // public async Task<ResponseModel> ChangePassword([FromBody] UserViewModel user)
        // {

        //     var result = _userServices.ChangePassword(user);
        //     return await Task.FromResult(result);
        // }
        // [TypeFilter(typeof(CustomAuth))]
        // [Route("api/User/role")]
        // public async Task<ResponseModel> Role()
        // {
        //     var result = _userServices.GetRole();
        //     return await Task.FromResult(result);
        // }
        // [TypeFilter(typeof(CustomAuth))]
        // [Route("api/User/designation")]
        // public async Task<ResponseModel> Designation()
        // {
        //     var result = _userServices.GetDesignation();
        //     return await Task.FromResult(result);
        // }
        // [Route("api/User/barcode")]
        // public async Task<ResponseModel> BarcodeReader(string barcode)
        // {
        //     var barresult = _userServices.GetBarcode(barcode);
        //     return await Task.FromResult(barresult);
        // }
        // [TypeFilter(typeof(CustomAuth))]
        // [Route("api/Operator/username")]
        // public async Task<ResponseModel> UserName(string username)
        // {
        //     var user = _userServices.userName(username);
        //     return await Task.FromResult(user);
        // }
        // [TypeFilter(typeof(CustomAuth))]
        // [Route("api/Operator/email")]
        // public async Task<ResponseModel> EmailAddress(string email)
        // {
        //     var user = _userServices.EmailAddress(email);
        //     return await Task.FromResult(user);
        // }
        // [TypeFilter(typeof(CustomAuth))]
        // [Route("api/users/getactiveusers")]
        // public async Task<ResponseModel> GeActiveUsers()
        // {
        //     var user = _userServices.GetActiveUsers();
        //     return await Task.FromResult(user);
        // }
        // [HttpGet]
        // [Route("api/users/forgotpassword")]
        // public async Task<ResponseModel> ForgetPassword(string Username)
        // {
        //     return await Task.FromResult(_userServices.Forgotpassword(Username));
        // }
    }

}
