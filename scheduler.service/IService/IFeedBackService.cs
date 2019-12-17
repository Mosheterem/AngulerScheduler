using scheduler.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.service.IService
{
   public interface IFeedBackService
    {
        ResponseModel AddFirstResponse(string key,string seckey);
        ResponseModel Unsubscribe(string key);
        ResponseModel AddFeedback(FeedBack feedBack);
    }
}
