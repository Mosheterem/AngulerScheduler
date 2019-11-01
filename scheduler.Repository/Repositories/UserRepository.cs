using scheduler.EF.Model;
using scheduler.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using scheduler.Model.ViewModel;
using scheduler.EF.ComplexTypes;

namespace scheduler.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Tuple<CaltUsers, string> AuthenticateUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool ChangeAuthenticationPwd(UserViewModel user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Tuple<bool, string, string> Forgotpassword(string username)
        {
            throw new NotImplementedException();
        }

        public CaltUsers Get(int id)
        {
            throw new NotImplementedException();
        }

        public CaltUsers Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CaltUsers> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(CaltUsers entity)
        {
            throw new NotImplementedException();
        }

        public Tuple<bool, dynamic> ResetAuthenticationPwd(resetViewModel user)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool SetAuthenticationPwd(SetPasswordModel user)
        {
            throw new NotImplementedException();
        }

        public void Update(CaltUsers entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateInfo(int id, CaltUsers entity)
        {
            throw new NotImplementedException();
        }

        public Logins Username(string name)
        {
            throw new NotImplementedException();
        }
    }
}
