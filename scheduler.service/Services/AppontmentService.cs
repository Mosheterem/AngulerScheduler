using Newtonsoft.Json;
using scheduler.Model.ViewModel;
using scheduler.service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using scheduler.EF.Model;
using scheduler.Repository.Infrastructure;
using scheduler.EF.ComplexTypes;
using scheduler.Model;
using scheduler.service.Common_Utilities;

namespace scheduler.service.Services
{
    public class AppontmentService : IAppontmentService
    {

        private readonly IUnitOfWork _unitOfWork;
        protected IAppontmentService _appontmentService;
        public IHttpContextAccessor _httpContextaccessor;
        private CaltUsers SessionUser { get; set; }
        public AppontmentService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {

            _httpContextaccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            SessionUser = JsonConvert.DeserializeObject<CaltUsers>(_httpContextaccessor.HttpContext.Session.GetString("Userdetail"));

        }

        public ResponseModel GetAppointments(string clinetId)
        {

            ResponseModel response = new ResponseModel();

            var getmiscellaneous = _unitOfWork.AppontmentRepository.GetAppointments(SessionUser.ClientID, SessionUser.ConnectionString);
            response.data = getmiscellaneous.Item1;
            response.status = Status.Success;
            return response;
        }

        //public ResponseModel AddAppointments(Appointment appointment)
        //{

        //}

        public ResponseModel AddAppointments(Appointment appointment, string clinetId)
        {
            ResponseModel response = new ResponseModel();

            var task = new TASK
            {
                Remark = appointment.description,
                StartTime = appointment.startTime,
                Label = appointment.label,
                AllDayEvent = appointment.AllDayEvent,
                EndTime = appointment.endTime,
                LakNum = appointment.lakNum,
                OwnerIds = string.Join(",", appointment.ownerIds),
                LtName = appointment.ltName,
                PLACE = appointment.place,
                Teur = appointment.teur,
                TIK = appointment.tiK,
                WITHWHO = appointment.withwho,
               
            };

            var getmiscellaneous = _unitOfWork.AppontmentRepository.AddAppointments(task, SessionUser.ClientID, SessionUser.ConnectionString);
            response.data = getmiscellaneous.Item1;
            response.status = Status.Success;
            return response;
        }

        public ResponseModel GetLabels()
        {
            ResponseModel response = new ResponseModel();

            var getmiscellaneous = _unitOfWork.AppontmentRepository.GetLabels(SessionUser.ClientID, SessionUser.ConnectionString);
            response.data = getmiscellaneous.Item1;
            response.status = Status.Success;
            return response;
        }

        public ResponseModel Getresources()
        {
            ResponseModel response = new ResponseModel();

            var getmiscellaneous = _unitOfWork.AppontmentRepository.GetResources(SessionUser.ClientID, SessionUser.ConnectionString,SessionUser.eMail);
            response.data = getmiscellaneous.Item1;
            response.status = Status.Success;
            return response;
        }

        public ResponseModel GetUserSettings()
        {
            ResponseModel response = new ResponseModel();

            var getmiscellaneous = _unitOfWork.AppontmentRepository.GetUserSettings(SessionUser.ClientID, SessionUser.ConnectionString, SessionUser.eMail);
            response.data = getmiscellaneous.Item1;
            response.status = Status.Success;
            return response;
        }

        public ResponseModel UpdateAppointment(Appointment appointment, int key)
        {
            ResponseModel response = new ResponseModel();
            var task = new TASK
            {
                Remark = appointment.description,
                StartTime = appointment.startTime,
                Label = appointment.label,
                AllDayEvent = appointment.AllDayEvent,
                EndTime = appointment.endTime,
                LakNum = appointment.lakNum,
                OwnerIds = string.Join(",", appointment.ownerIds),
                LtName = appointment.ltName,
                PLACE = appointment.place,
                Teur = appointment.teur,
                TIK = appointment.tiK,
                WITHWHO = appointment.withwho,
                //AllDayEvent = false
            };
            var getmiscellaneous = _unitOfWork.AppontmentRepository.UpdateAppointments(SessionUser.PrimeIDName, task, SessionUser.ClientID, SessionUser.ConnectionString, key);
            response.data = getmiscellaneous.Item1;
            response.status = Status.Success;
            return response;
        }

        public ResponseModel DeleteAppointment(int key)
        {
            ResponseModel response = new ResponseModel();

            var getmiscellaneous = _unitOfWork.AppontmentRepository.DeleteAppointments(SessionUser.PrimeIDName, SessionUser.ClientID, SessionUser.ConnectionString, key);
            response.data = getmiscellaneous.Item1;
            response.status = Status.Success;
            return response;
        }

        public IEnumerable<dynamic> GetAppointmentByDate( DateTime startDate, DateTime endDate ,bool isdelete)
        {
            ResponseModel response = new ResponseModel();
            var getmiscellaneous = _unitOfWork.AppontmentRepository.GetAppointmentByDate(SessionUser.ClientID, startDate, endDate, SessionUser.ConnectionString, isdelete);
            response.data = getmiscellaneous;
            response.status = Status.Success;
            return getmiscellaneous;
        }

        public ResponseModel UpdateUserSettings(int id, UsersSettingsViewModel data)
        {
            ResponseModel response = new ResponseModel();
            var getmiscellaneous = _unitOfWork.AppontmentRepository.UpdateUserSettings(data,SessionUser.ClientID, SessionUser.ConnectionString, SessionUser.eMail);
            response.data = getmiscellaneous.Item2;
            response.status = Status.Success;
            return response;
        }

        public ResponseModel sendReportAsEmail(int id, reportsendPayload data)
        {
            ResponseModel response = new ResponseModel();
            Utilities.SendEmail(data.email, "Report"+DateTime.Now.ToShortDateString(), data.body.Replace(@"_ngcontent-c9=""", "").Replace(@"_ngcontent-c6=""",""));
            return response;
        }
    }
}
