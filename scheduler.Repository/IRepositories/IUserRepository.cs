using System;
using System.Collections.Generic;
using System.Text;
using scheduler.EF.ComplexTypes;
using scheduler.EF.Model;
using scheduler.Model.ViewModel;
namespace scheduler.Repository.IRepositories
{
    public interface IUserRepository : IGeneric<CaltUsers>
    {
        Tuple<CaltUsers, string> AuthenticateUser(string username, string password);
        bool ChangeAuthenticationPwd(UserViewModel user);
        Logins Username(string name);

        bool SetAuthenticationPwd(SetPasswordModel user);
        Tuple<bool, dynamic> ResetAuthenticationPwd(resetViewModel user);
        Tuple<bool, string, string> Forgotpassword(string username);
    }
}
