using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using scheduler.Model.ViewModel;
using scheduler.service.IService;
using webAppAnguler.Filters;

namespace webAppAnguler.Controllers
{

    [ApiController]
   // [TypeFilter(typeof(CustomException))]
    public class FeedBackController
    {
        private readonly IFeedBackService _feedbackServices;
        public FeedBackController(IFeedBackService feedbackServices)
        {
            _feedbackServices = feedbackServices;
        }
        [HttpPost]
        [Route("api/Feedback/AddFeedback")]
        public async Task<ResponseModel> Addfeedback([FromBody] FeedBack feedBack)
        {

            var detail = _feedbackServices.AddFeedback(feedBack);
            return await Task.FromResult(detail);
        }

        [HttpGet]
        [Route("api/Feedback/AddResponse")]
        public async Task<ResponseModel> AddFirstResponse(string key,string secKey)
        {
            return await Task.FromResult(_feedbackServices.AddFirstResponse(key, secKey));
        }
    }
}
