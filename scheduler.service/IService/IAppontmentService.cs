using scheduler.EF.ComplexTypes;
using scheduler.Model;
using scheduler.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.service.IService
{
  public interface IAppontmentService
    {

        ResponseModel GetAppointments(string clinetId);
        ResponseModel AddAppointments(Appointment appointment, string clinetId);
        IEnumerable<dynamic> GetAppointmentByDate(DateTime startDate, DateTime endDate,bool Isdeleted);
        ResponseModel UpdateAppointment(Appointment appointment,int key);
        ResponseModel DeleteAppointment(int key);
        ResponseModel GetLabels();
        ResponseModel Getresources();
        ResponseModel GetUserSettings();
        ResponseModel UpdateUserSettings( int id, UsersSettingsViewModel data);

        ResponseModel sendReportAsEmail(int id, reportsendPayload data);


    }
}
