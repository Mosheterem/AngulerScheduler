using scheduler.EF.Model;
using scheduler.Model.ViewModel;
using scheduler.Repository.IRepositories;
using scheduler.service.Common_Utilities;
using scheduler.service.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace scheduler.service.Services
{

    public static class SecureEmailClass
    {

        public static string DecryptString(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            //cipherText = cipherText.Replace(" ", "");
            cipherText= cipherText.Replace("_", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static string ConvertStringToHex(String input, System.Text.Encoding encoding)
        {
            Byte[] stringBytes = encoding.GetBytes(input);
            StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
            foreach (byte b in stringBytes)
            {
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }
        public static string ConvertHexToString(String hexInput, System.Text.Encoding encoding)
    {
         int numberChars = hexInput.Length;
        byte[] bytes = new byte[numberChars / 2];
         for (int i = 0; i < numberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16);
         }
         return encoding.GetString(bytes);
    }

        public static string EnryptString(string encryptString)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString.Replace("+", "_");
        }
    }
    public class CaltUserService : ICaltUserService
    {
        protected ICaltUserRepository _caltUserRepository;
        public CaltUserService(ICaltUserRepository caltUserRepository)
        {
            _caltUserRepository = caltUserRepository;
        }
        public ResponseModel FindUser(string Username, string Password)
        {


            ResponseModel response = new ResponseModel();
            response.data = String.Empty;

            try
            {
                var usre = _caltUserRepository.AuthenticateUser(Username, Password);
                if (usre != null)
                {
                    usre.PrimeIDName = GetPrimeIdName(usre);
                    response.data = Utilities.GenerateToken(usre, usre.eMail);
                    response.status = Status.Success;
                }
                else
                {
                    response.data = null;
                    response.message = "not valid user";
                    response.status = Status.Warning;
                }
            }
            catch (Exception ex)
            {

                response.data = null;
                response.message = "internal Server Error";
                response.status = Status.Error;

            }

            return response;
        }
        private string GetPrimeIdName(CaltUsers user)
        {
            // string strConnectionString = "Server=tcp:192.168.1.3\\mssqlserver,50841;Database=CALT" + clinetId + "; User ID=sa; Password=r4199357!;MultipleActiveResultSets=true";
            string strConnectionStringFix = @"Server=tcp:23.97.186.48\\mssqlserver,50841;Database=CALT" + user.ClientID.Trim() + "; User ID=sa; Password=r4199357!;MultipleActiveResultSets=true";
            string strConnectionString = String.IsNullOrEmpty(user.ConnectionString) == true ? strConnectionStringFix : user.ConnectionString;
            var idName = "";

            using (SqlConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE where TABLE_NAME = 'TASKS' ";
                SqlCommand cmd = new SqlCommand(query, con);
                idName = cmd.ExecuteScalar().ToString();

            }

            //  string query = "update UserSettings set Resoucresgroup='" + value + "' where UserID=" + id;
            return idName;


        }
        public ResponseModel FindUserByname(string Username)
        {
            ResponseModel response = new ResponseModel();
            response.data = String.Empty;

            try
            {
                var usre = _caltUserRepository.GetuserByuserName(Username);
                if (usre != null)
                {

                    response.data = usre;
                    response.status = Status.Success;
                }
                else
                {
                    response.data = null;
                    response.message = "not valid  user";
                    response.status = Status.Warning;
                }
            }
            catch (Exception ex)
            {

                response.data = null;
                response.message = "internal Server Error";
                response.status = Status.Error;

            }

            return response;
        }


        public ResponseModel Forgotpassword(string Username)
        {
            ResponseModel response = new ResponseModel();
            response.data = String.Empty;
            var email = "";
            var pass = "";
            try
            {

                {
                    if (Username.Contains(","))
                    {
                         email = SecureEmailClass.ConvertHexToString(Username.Split(",")[0], System.Text.Encoding.Unicode);
                        //usre.RestorEmail
                        pass = Username.Split(",")[1];
                        var usre = _caltUserRepository.GetuserByuserName(email);
                        if (usre != null)
                        {
                           
                            var usre1 = _caltUserRepository.UpdatePassword(email, pass);
                            response.status = Status.Success;
                            response.message = "הססמא שלך שונתה, נא היכנס עם הססמא החדשה";
                        }
                        else
                        {
                            response.data = null;
                            response.message = "error while update password ";
                            response.status = Status.Warning;
                        }
                    }
                    else
                    {
                        var usre = _caltUserRepository.GetuserByuserName(Username);
                        if (usre != null)
                        {




                            response.status = Status.Success;
                            response.message = "קישור נשלח לתיבת האימייל שלך הרשומה אצלנו, לשם אתחול הססמא";

                            //Utilities.SendEmail();
                            var QweryString =SecureEmailClass.ConvertStringToHex(usre.RestorEmail, System.Text.Encoding.Unicode); //SecureEmailClass.EnryptString(usre.RestorEmail);

                            var temp = "http://23.97.186.48:8082/public/resetPassword/" + QweryString;
                            var body = new StringBuilder();
                            body.AppendFormat("Hello, {0}\n", usre.ResourceName);
                            body.AppendLine(@"click here to reset  your password");
                            body.AppendLine("<a href=\"" + temp + "\">click</a>");

                            string email2 = usre.RestorEmail;
                            //string email = usre.RestorEmail;
                            string subject = "שלום" + " " + usre.ResourceName;
                            // string message = "הססמא החדשה היא" + "  " + usre1;

                            string message = body.ToString();

                            Utilities.SendEmail(email2, subject, message);

                        }
                        else
                        {
                            response.data = null;
                            response.message = "not valid  user";
                            response.status = Status.Warning;
                        }
                        //response.data = usre;
                        //response.status = Status.Success;
                    }

                }
            }
            catch (Exception ex)
            {

                response.data = null;
                response.message = "internal Server Error";
                response.status = Status.Error;

            }

            return response;
        }

        public ResponseModel GetuserByCustomuserName(string Username)
        {
            ResponseModel response = new ResponseModel();
            response.data = String.Empty;

            try
            {
                var usre = _caltUserRepository.GetuserByCustomuserName(Username);
                if (usre != null)
                {

                    response.data = usre;
                    response.status = Status.Success;
                }
                else
                {
                    response.data = null;
                    response.message = "not valid  user";
                    response.status = Status.Warning;
                }
            }
            catch (Exception ex)
            {

                response.data = null;
                response.message = "internal Server Error";
                response.status = Status.Error;

            }

            return response;
        }
    }
}
