using scheduler.EF.ComplexTypes;
using scheduler.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.Repository.IRepositories
{
    public interface IAppontmentRepository
    {


        Tuple<List<TASK>, dynamic> GetAppointments(string barcode, string ConnectionString);
        IEnumerable<dynamic> GetAppointmentByDate(string ClinetId, DateTime startDate, DateTime endDate, string ConnectionString, bool isdelete);
        Tuple<int, dynamic> AddAppointments(TASK appointment, string ClinetId, string ConnectionString);
        Tuple<int, dynamic> UpdateAppointments(string PrimeIdName, TASK appointment, string ClinetId, string ConnectionString, int value);
        Tuple<int, dynamic> DeleteAppointments(string PrimeIdName, string ClinetId, string ConnectionString, int value);
        Tuple<List<TempData>, dynamic> GetLabels(string ClinetId,string ConnectionString);


        Tuple<List<TempData>, dynamic> GetResources(string ClinetId, string ConnectionString, string Email);

        Tuple<UserSettings, dynamic> GetUserSettings(string ClinetId, string ConnectionString,string email);
        Tuple<int, dynamic> UpdateUserSettings(UsersSettingsViewModel settings ,string ClinetId, string ConnectionString, string email);
    }
}
