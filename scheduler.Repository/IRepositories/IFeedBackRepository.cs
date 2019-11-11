using scheduler.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.Repository.IRepositories
{
    public interface IFeedBackRepository
    {

        bool UpdateMoreInfomation(FeedBack objFeedBack);
        int AddFirstNotification(string name);
    }
}
