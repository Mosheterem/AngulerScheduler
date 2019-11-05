using scheduler.Model.ViewModel;
using scheduler.service.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.service.Services
{
    public class FeedBackService : IFeedBackService
    {

        protected IFeedBackService _feedBackService;
        public FeedBackService(IFeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }
        public ResponseModel AddFeedback(FeedBack feedBack)
        {
            ResponseModel response = new ResponseModel();
            response.data = String.Empty;

            try
            {
                var usre = _feedBackService.AddFeedback(feedBack);
                if (usre != null)
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
                var usre = _feedBackService.AddFirstResponse(emailId);
                if (usre != null)
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
