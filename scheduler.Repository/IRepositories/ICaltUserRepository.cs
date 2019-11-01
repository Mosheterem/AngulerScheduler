using scheduler.EF.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.Repository.IRepositories
{
   public interface ICaltUserRepository
    {
        CaltUsers AuthenticateUser(string username, string password);
        CaltUsers GetuserByuserName(string username);
        string UpdatePassword(string email,string password);
        //GetuserByCustomuserName
        CaltUsers GetuserByCustomuserName(string username);
    }
}
