using scheduler.EF.Model.Pub;
using scheduler.Model.ViewModel;
using scheduler.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.Repository.Repositories
{
    public class FeedBackRepository : IFeedBackRepository
    {
        protected PubContext _pubContext;
        public FeedBackRepository(PubContext pubContext)
        {
            _pubContext = pubContext;

        }
        public bool AddFirstNotification(string name)
        {
            try { 
            FeedBack obje = new FeedBack();
            obje.Email = name;
                _pubContext.AddFirstCall(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateMoreInfomation(FeedBack objFeedBack)
        {
            try
            {
                _pubContext.AddFeedback(objFeedBack);


                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
