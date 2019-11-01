using scheduler.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.service.IService
{
    public interface ICaltUserService
    {
        ResponseModel FindUser(string Username, string Password);
        ResponseModel FindUserByname(string Username);
        ResponseModel GetuserByCustomuserName(string Username);
        ResponseModel Forgotpassword(string username);
    }
}

