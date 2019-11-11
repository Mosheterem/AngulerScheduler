using scheduler.Model.ViewModel;
using scheduler.Repository.IRepositories;
using scheduler.service.IService;
using System;

namespace scheduler.service.Services
{
    public class FeedBackService : IFeedBackService
    {

        protected IFeedBackRepository _feedBackService;
        public FeedBackService(IFeedBackRepository feedBackService)
        {
            _feedBackService = feedBackService;
        }
        public ResponseModel AddFeedback(FeedBack feedBack)
        {
            ResponseModel response = new ResponseModel();
            response.data = String.Empty;

            try
            {
                var usre = _feedBackService.UpdateMoreInfomation(feedBack);
                if (usre)
                {

                    response.data = usre;
                    response.status = Status.Success;
                }
                else
                {
                    response.data = null;
                    response.message = "not valid  user";
                    response.status = Status.Warning;
                }
            }
            catch (Exception ex)
            {

                response.data = null;
                response.message = "internal Server Error";
                response.status = Status.Error;

            }

            return response;
        }

        public ResponseModel AddFirstResponse(string emailId)
        {
            ResponseModel response = new ResponseModel();
            response.data = String.Empty;

            try
            {
                var usre = _feedBackService.AddFirstNotification(emailId);
                if (usre>0)
                {

                    response.data = usre;
                    response.status = Status.Success;
                }
                else
                {
                    response.data = null;
                    response.message = "not valid  user";
                    response.status = Status.Warning;
                }
            }
            catch (Exception ex)
            {

                response.data = null;
                response.message = "internal Server Error";
                response.status = Status.Error;

            }

            return response;
        }
    }
}
