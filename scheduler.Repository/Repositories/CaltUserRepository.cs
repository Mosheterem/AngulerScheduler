using scheduler.EF.Model;
using scheduler.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scheduler.Repository.Repositories
{
    public class CaltUserRepository : ICaltUserRepository
    {
        protected LoginDataContext _loginDataContext;
        public CaltUserRepository(LoginDataContext loginDataContext)
        {
            _loginDataContext = loginDataContext;

        }
        public CaltUsers AuthenticateUser(string username, string password)
        {
            var user= _loginDataContext.CaltUsers.Where(x => x.eMail == username && x.Password == password).FirstOrDefault();
            if (user != null)
            {
                user.LastUserLogin = DateTime.Now;
                _loginDataContext.CaltUsers.Update(user);
            }

            return user;
        }

        public CaltUsers GetuserByuserName(string username)
        {
            return _loginDataContext.CaltUsers.Where(x => x.RestorEmail == username).FirstOrDefault();
        }
        public CaltUsers GetuserByCustomuserName(string username)
        {
            return _loginDataContext.CaltUsers.Where(x => x.eMail == username).FirstOrDefault();
        }
        public string UpdatePassword(string email, string password)
        {
            try { 
            Random generator = new Random();
                String newPassword = password;
            var user= _loginDataContext.CaltUsers.Where(x => x.RestorEmail == email).FirstOrDefault();
            user.Password = newPassword;
            _loginDataContext.Update(user);

               // Utilities.SendEmail(email, subject, message);
                _loginDataContext.SaveChanges();
                return newPassword;
            }
            catch
            {
                return "error";
            }
        }
    }
}
