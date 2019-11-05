using scheduler.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.Repository.IRepositories
{
    interface IFeedBackRepository
    {

        bool UpdateMoreInfomation(FeedBack objFeedBack);
        bool AddFirstNotification(string name);
    }
}
