using System;
using System.Collections.Generic;
using System.Text;
using scheduler.Model.ViewModel;
using scheduler.service.IService;

namespace scheduler.service.Services
{
    public class UserService : IUserService
    {
        public ResponseModel ChangePassword(UserViewModel user)
        {
            throw new NotImplementedException();
        }

        public ResponseModel EmailAddress(string email)
        {
            throw new NotImplementedException();
        }

        public ResponseModel FindUser(string Username, string Password)
        {
            throw new NotImplementedException();
        }

        public ResponseModel Forgotpassword(string username)
        {
            throw new NotImplementedException();
        }

        public ResponseModel GetActiveUsers()
        {
            throw new NotImplementedException();
        }

        public ResponseModel GetBarcode(string barcode)
        {
            throw new NotImplementedException();
        }

        public ResponseModel GetDesignation()
        {
            throw new NotImplementedException();
        }

        public ResponseModel GetRole()
        {
            throw new NotImplementedException();
        }

        public ResponseModel ResetPassword(resetViewModel user)
        {
            throw new NotImplementedException();
        }

        public ResponseModel SetPassword(SetPasswordModel user)
        {
            throw new NotImplementedException();
        }

        public ResponseModel userName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
