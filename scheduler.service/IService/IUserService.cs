using scheduler.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.service.IService
{
    public interface IUserService
    {
        ResponseModel FindUser(string Username, string Password);
        ResponseModel SetPassword(SetPasswordModel user);
        ResponseModel ResetPassword(resetViewModel user);
        ResponseModel ChangePassword(UserViewModel user);
        ResponseModel GetRole();
        ResponseModel GetDesignation();
        ResponseModel GetBarcode(string barcode);
        ResponseModel userName(string name);
        ResponseModel EmailAddress(string email);
        ResponseModel GetActiveUsers();
        ResponseModel Forgotpassword(string username);
    }
}
