using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using scheduler.EF.ComplexTypes;
using scheduler.EF.Model;
using scheduler.Model;
using scheduler.Model.ViewModel;
using scheduler.service.IService;
using scheduler.service.Services;
using webAppAnguler.Filters;


namespace webAppAnguler.Controllers
{

    [ApiController]
    [TypeFilter(typeof(CustomException))]
    public class AppointmentController
    {
        public static int GetPropValue(dynamic src, string propName)
        {
           // var val = ((System.Collections.Generic.IDictionary<string, object>)src).FirstOrDefault().Value;
            //return val;

            var propertyInfo = src.GetType().GetProperty(propName);
          return (int)propertyInfo.GetValue(src, null);


        }

        public static object GetPropValueRepo(dynamic src, string propName)
        {
            var val = ((System.Collections.Generic.IDictionary<string, object>)src).FirstOrDefault().Value;
            return val;

           // var propertyInfo = src.GetType().GetProperty(propName);
           // return (int)propertyInfo.GetValue(src, null);


        }
        private CaltUsers SessionUser { get; set; }
        private readonly IAppontmentService _appontmentService;
        public IHttpContextAccessor _httpContextaccessor;
        public AppointmentController(IAppontmentService appontmentService, IHttpContextAccessor httpContextAccessor)
        {
            _appontmentService = appontmentService;

            _httpContextaccessor = httpContextAccessor;
          //  _unitOfWork = unitOfWork;
            SessionUser = JsonConvert.DeserializeObject<CaltUsers>(_httpContextaccessor.HttpContext.Session.GetString("Userdetail"));

        }
        [TypeFilter(typeof(CustomAuth))]
        [HttpGet]
        [Route("api/appointment/events")]
        public object Getdata(DataSourceLoadOptions loadOptions)
        {
            var detail = _appontmentService.GetAppointments("").data;
            IEnumerable<dynamic> _result = detail;
            var data = _result.Select(p => new
            {
                PrimeryKey = GetPropValue(p, SessionUser.PrimeIDName),
                Teur = p.Teur,
                OwnerIds = getData(p.OwnerIds,p),
                StartTime = p.StartTime,
                EndTime = p.EndTime,
                AllDayEvent = p.AllDayEvent,
                Label = p.Label,
                LtName = p.LtName,
                Remark = p.Remark,
                PLACE = p.PLACE,
                TIK = p.TIK,
                WITHWHO = p.WITHWHO,
                LakNum = p.LakNum,
                recurrenceRule = p.recurrenceRule,
                attendees = p.attendees,
                DATE_OUT = p.DATE_OUT,
                duraion = p.EndTime.Subtract(p.StartTime).TotalMinutes,
                IsDeleted = p.IsDeleted,
                HexBackColor = p.HexBackColor,
                HexBarColor = p.HexBarColor,
                HexForeColor = p.HexForeColor,


            });
            //return DataSourceLoader.Load(data.ToList(), loadOptions);
            return data.ToList();
        }

        [TypeFilter(typeof(CustomAuth))]
        [HttpPost]
        [Route("api/appointment/eventByDate")]
        public object GetdataByDate([FromBody]GetreportData payload)
        {
            IEnumerable<dynamic> _result = _appontmentService.GetAppointmentByDate(payload.minDate,payload.maxDate,false);
           // IEnumerable<dynamic> _result = detail;
            var data = _result.Select(p => new
            {
                PrimeryKey = GetPropValueRepo(p, SessionUser.PrimeIDName),
                Teur = p.TEUR,
               OwnerIds = getData(p.OwnerIDs, p),
                StartTime = p.StartTime,
                EndTime = p.EndTime,
                AllDayEvent = p.AllDayEvent,
                Label = p.Label,
                LtName = p.LtName,
                Remark = p.Remark,
                PLACE = p.PLACE,
                TIK_IN = p.TIK_IN,
                WITHWHO = p.WITHWHO,
                LakNum = p.LakNum,
                recurrenceRule = p.recurrenceRule,
                attendees = p.attendees,
                DATE_OUT = p.DATE_OUT,
                duraion = p.EndTime.Subtract(p.StartTime).TotalMinutes,
                IsDeleted = p.IsDeletedm,
                HexBackColor = p.HexBackColor,
                HexBarColor = p.HexBarColor,
                HexForeColor = p.HexForeColor,

            });
            //return DataSourceLoader.Load(data.ToList(), loadOptions);
            return data.ToList();
        }
        [TypeFilter(typeof(CustomAuth))]
        [Route("api/appointment/addEvent")]
        [HttpPost]
        public async Task<ResponseModel> InsertOrder([FromForm] string values)
        {
            //string values = "";
            var newAppointment = new Appointment();
            JsonConvert.PopulateObject(values, newAppointment);

           
            var detail = _appontmentService.AddAppointments(newAppointment, "");
            return await Task.FromResult(detail);
        }
        [TypeFilter(typeof(CustomAuth))]
        [Route("api/appointment/update")]
        [HttpPut]
         public async Task<ResponseModel> Post([FromForm] int key, [FromForm] string values)
        {
            var newAppointment = new Appointment();
            JsonConvert.PopulateObject(values, newAppointment);
            var task = new TASK
            {
                Remark = newAppointment.remark,
                StartTime =Convert.ToDateTime(newAppointment.startTime),
                Label = newAppointment.label,
                AllDayEvent=Convert.ToBoolean(newAppointment.AllDayEvent),
                EndTime = Convert.ToDateTime(newAppointment.endTime),
                LakNum = newAppointment.lakNum,
                OwnerIds = string.Join(",", newAppointment.ownerIds),
                LtName = newAppointment.ltName,
                PLACE = newAppointment.place,
                Teur = newAppointment.teur,
                TIK = newAppointment.tiK,
                WITHWHO = newAppointment.HexBackColor,
               HexBackColor= newAppointment.HexBackColor,
                HexBarColor = newAppointment.HexBarColor,
                HexForeColor = newAppointment.HexForeColor,

            };
            var detail = _appontmentService.UpdateAppointment(newAppointment, key);
            return await Task.FromResult(detail);
        }
        [TypeFilter(typeof(CustomAuth))]
        [Route("api/appointment/Delete")]
        [HttpDelete]
        [TypeFilter(typeof(CustomAuth))]
        public async Task<ResponseModel> DeleteAsync([FromForm] int key)
        {


            var detail = _appontmentService.DeleteAppointment(key);
            return await Task.FromResult(detail);
        }
        //private dynamic getData(string pro)
        //{
        //    var myint = Array.ConvertAll<string, int>(pro.Split(','), int.Parse);
        //    return myint;
        //}
        private dynamic getData(string pro, dynamic p)
        {
            if (pro != null)
            {
                var myint = Array.ConvertAll<string, int>(pro.Split(','), int.Parse);
                return myint;
            }
            else
            {
                return "1,2";
            }
        }

        [TypeFilter(typeof(CustomAuth))]
        [Route("api/appointment/getlabels")]
        public async Task<ResponseModel> Getlabels()
        {
            var result = _appontmentService.GetLabels();
            return await Task.FromResult(result);
        }
        [TypeFilter(typeof(CustomAuth))]
        [Route("api/appointment/getResources")]
        public async Task<ResponseModel> GetResources()
        {
            var result = _appontmentService.Getresources();
            return await Task.FromResult(result);
        }
        [TypeFilter(typeof(CustomAuth))]
        [Route("api/appointment/getUserSettings")]
        public async Task<ResponseModel> GetUserSettings()
        {

            var result = _appontmentService.GetUserSettings();
            return await Task.FromResult(result);
        }

        [TypeFilter(typeof(CustomAuth))]
        [Route("api/appointment/UpdateUserSettings")]
        [HttpPost]
        public async Task<ResponseModel> UpdateUserSettings([FromBody]UsersSettingsViewModel data)
        {

            var result = _appontmentService.UpdateUserSettings(1,data);
            return await Task.FromResult(result);
        }
        [TypeFilter(typeof(CustomAuth))]
        [Route("api/appointment/sendReportAsemail")]
        [HttpPost]
        public async Task<ResponseModel> sendReportAsemail([FromBody]reportsendPayload data)
        {

            var result = _appontmentService.sendReportAsEmail(1, data);
            return await Task.FromResult(result);
        }

    }

}
