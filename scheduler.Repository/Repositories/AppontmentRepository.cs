using scheduler.EF.ComplexTypes;
using scheduler.EF.Model;
using scheduler.Model;
using scheduler.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.Repository.Repositories
{

    //public class PatientAssociationRepository : Generic<Sets>, IPatientAssociationRepository
    //{
    //    public PatientAssociationRepository(InstruTrackContext context)
    //    {
    //        this.context = context;
    //    }

    //    public string AssociationInsert(AssociationViewModel associationViewModel)
    //    {
    //        return context.AssociationInsert(associationViewModel).Result;
    //    }//: Generic<Sets>, IPatientAssociationRepository
    public class AppontmentRepository : Generic<TASK>, IAppontmentRepository
    {

        public AppontmentRepository(SchedulerContext context)
        {
            this.context = context;
        }

        //public int AddAppointments(Appointment appointment)
        //{
        //    throw new NotImplementedException();
        //}

        public Tuple<int,dynamic> AddAppointments(TASK appointment,string ClinetId, string ConnectionString)
        {
            return context.AddAppointments(appointment,ClinetId, ConnectionString);
        }

        public Tuple<int, dynamic> DeleteAppointments(string PrimeIdName, string ClinetId, string ConnectionString,int value)
        {
            return context.DeleteAppointments(PrimeIdName, ClinetId, ConnectionString, value);
        }

        public IEnumerable<dynamic> GetAppointmentByDate(string ClinetId,DateTime startDate, DateTime endDate, string ConnectionString, bool Isdeleted)
        {
            return context.GetAppointmentbyDate(ClinetId, ConnectionString, Isdeleted, startDate,endDate);
        }

        public  Tuple<List<TASK>, dynamic> GetAppointments(string ClinetId, string ConnectionString)
        {
            return context.GetAppointments(ClinetId, ConnectionString);
        }

        public Tuple<List<TempData>, dynamic> GetLabels(string ClinetId, string ConnectionString)
        {
            return context.GetLabels(ClinetId, ConnectionString).Result;
        }

        public Tuple<List<TempData>, dynamic> GetResources(string ClinetId, string ConnectionString, string Email)
        {
            return context.GetResources(ClinetId, ConnectionString, Email).Result;
        }

        public Tuple<UserSettings, dynamic> GetUserSettings(string ClinetId, string ConnectionString,string Email)
        {
            return context.GetUserSettings(ClinetId, ConnectionString, Email);
        }
        public Tuple<int, dynamic> UpdateUserSettings(UsersSettingsViewModel data,string ClinetId, string ConnectionString, string Email)
        {
            return context.UpdateUserSettings(data, ClinetId, ConnectionString, Email);
        }

        public Tuple<int, dynamic> UpdateAppointments(string PrimeIdName,TASK appointment, string ClinetId, string ConnectionString,int value)
        {
            return context.UpdateAppointments(PrimeIdName,appointment, ClinetId, ConnectionString,value);
        }
    }
}
