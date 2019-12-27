using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Claims;

namespace scheduler.service.Common_Utilities
{
    public class CustomConfiguration
    {
        public string PageItem { get; set; }
    }
    public class Utilities
    {
        private static IOptions<CustomConfiguration> _config;
        private static IConfiguration Configuration;

        public static string GetConfiguration(string key)
        {

            PropertyInfo info = _config.Value.GetType().GetProperty(key);
            if (info == null)
                return null;
            else
            {
                var val = info.GetValue(_config.Value);
                if (val != null)
                    return val.ToString();
                else
                    return null;
            }
        }

        public static void Configure(IOptions<CustomConfiguration> config)
        {
            _config = config;
        }

        public static void Configure(IConfiguration config)
        {
            Configuration = config;
        }

        //to create authentication token based on username and data
        public static string GenerateToken(dynamic data, string Username)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF32.GetBytes(Username));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
            JwtHeader header = new JwtHeader(signingCredentials);
            List<Claim> claims = new List<Claim>
            {
                new Claim("Username",Username),
                new Claim("Userrecord",JsonConvert.SerializeObject(data)),
            };
            JwtPayload payload = new JwtPayload(issuer: "*", audience: "*", issuedAt: DateTime.Now, expires: DateTime.Now.AddDays(7), claims: claims, notBefore: DateTime.Now);
            JwtSecurityToken token = new JwtSecurityToken(header: header, payload: payload);
            return tokenHandler.WriteToken(token);
        }
        //Common Functionality to send Emails. 
        public static void SendEmail(string email, string subject, string message)
        {//"terembingo@gmail.com", "terem32#"

            string EmailSystemAdminEmail = "support@mtlp.co.il";
            string EmailSystemAdminPassword = "Tuv74633";


            //   Attachment at1 = new Attachment(ms, name, mimeType);
            MailMessage mail = new MailMessage();

            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.office365.com");
            NetworkCredential loginInfo = new NetworkCredential(EmailSystemAdminEmail, EmailSystemAdminPassword);
            mail.From = new MailAddress("support@mtlp.co.il", "הוצאת עוד משה טרם בעמ");
            mail.To.Add(new MailAddress(email));

            mail.Subject = subject; //EmailId will be parentId
            mail.Body = message;
            mail.IsBodyHtml = true;

            mail.Sender = new MailAddress("support@mtlp.co.il");
            SmtpServer.Port = 587;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("support@mtlp.co.il", "Tuv74633");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
        // Global page size property for pagination. 
        public static int PageSize
        {
            get
            {
                //return mySettings.pageitem;

                //return Convert.ToInt32(Configuration.GetConnectionString("PageItem"));
                return 10;
            }
        }
       

    }
}
