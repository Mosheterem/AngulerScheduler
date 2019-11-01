using Dapper;
using Microsoft.EntityFrameworkCore;
using scheduler.EF.ComplexTypes;
using scheduler.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.EF.Model
{


    public partial class SchedulerContext : DbContext
    {
        public SchedulerContext()
        {
        }

        //public SchedulerContext()
        //{
        //}

        public SchedulerContext(DbContextOptions<SchedulerContext> options)
                : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=tcp:81.218.198.91\SQLEXPRESS2014,49682;Database=CALT83203; User ID=Terem; Password=terem32!;MultipleActiveResultSets=true");
            }
        }

        public DbSet<TASK> TASKs { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Resources> Resources { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }


        public async Task<Tuple<List<TempData>, dynamic>> GetLabels(string ClinetId, string ConnectionString)
        {
            Label result = new Label();
            try
            {
                string strConnectionString = GetConnectionString(ConnectionString, ClinetId);
                List<Label> groupMeetingsList = new List<Label>();
                var Errormsg = new SqlParameter
                {
                    ParameterName = "@ErrorMsg",
                    Size = -1,
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output
                };
                using (IDbConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    //string sqlQuery = "select * from Task";
                    // groupMeetingsList = await this.Query<TASK>().FromSql(sqlQuery, Errormsg).ToListAsync();
                    List<TempData> obj = new List<TempData>();
                    groupMeetingsList = con.Query<Label>("Select * from labels").ToList();
                    foreach (var item in groupMeetingsList)
                    {
                        obj.Add(new TempData { color = item.Color, id = item.PrimeID, IsCheked = false, text = item.SugErua });
                    }
                    return new Tuple<List<TempData>, dynamic>(obj, Errormsg.Value);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  

        }

        public async Task<Tuple<List<TempData>, dynamic>> GetResources(string ClientId, string ConnectionString, string emailId)
        {
            TASK result = new TASK();
            try
            {
                string strConnectionString = GetConnectionString(ConnectionString, ClientId);
                List<Resources> groupMeetingsList = new List<Resources>();
                var Errormsg = new SqlParameter
                {
                    ParameterName = "@ErrorMsg",
                    Size = -1,
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output
                };

                int idex = 0;
                using (IDbConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var CurrentUserSettings = con.Query<UserSettings>("Select * from UserSettings where Email='" + emailId + "'").FirstOrDefault();
                    string uidS = CurrentUserSettings.Resoucresgroup;
                    string[] Settings = uidS.Split(',');
                    groupMeetingsList = con.Query<Resources>("Select * from Resources").ToList();




                    List<TempData> obj = new List<TempData>();
                    foreach (var item in groupMeetingsList)
                    {
                        var ss = Settings[idex];
                        obj.Add(new TempData { color = item.Color, id = item.UniqueID, IsCheked = Settings[idex] == "1" ? true : false, text = item.ResourceName });
                        idex++;
                    }

                    return new Tuple<List<TempData>, dynamic>(obj, Errormsg.Value);
                    //return new Tuple<List<TempData>, dynamic>(groupMeetingsList, Errormsg.Value);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  

        }


        public IEnumerable<dynamic> GetAppointmentbyDate(string ClientId, string ConnectionString, bool isdelete, DateTime startTimeold, DateTime endTimeold)
        {
            TASK result = new TASK();
            var startTime = startTimeold.Date.ToString("yyyy-MM-dd HH:mm:ss");
            var endTime = endTimeold.AddHours(23.59).Date.ToString("yyyy-MM-dd HH:mm:ss");
            string test = "SELECT  * FROM    Tasks WHERE   startTime >= '" + startTime + "' AND EndTime   <= '" + endTime + "'  and IsDeleted = 0";

            List<dynamic> groupMeetingsList = new List<dynamic>();



            try
            {
                string strConnectionString = GetConnectionString(ConnectionString, ClientId);
                // List<TASK> groupMeetingsList = new List<TASK>();
                var Errormsg = new SqlParameter
                {
                    ParameterName = "@ErrorMsg",
                    Size = -1,
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output
                };


                using (IDbConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();


                    if (isdelete)
                        groupMeetingsList = con.Query<dynamic>("SELECT * FROM    Tasks WHERE   startTime >= '" + startTime + "' AND EndTime <= '" + endTime + "'").ToList();
                    else
                        groupMeetingsList = con.Query<dynamic>("SELECT * FROM    Tasks WHERE   startTime >= '" + startTime + "' AND EndTime   <= '" + endTime + "'  and IsDeleted = 0").ToList();

                    return groupMeetingsList;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  

        }
        public Tuple<List<TASK>, dynamic> GetAppointments(string ClientId, string ConnectionString)
        {
            TASK result = new TASK();
            try
            {
                string strConnectionString = GetConnectionString(ConnectionString, ClientId);
                List<TASK> groupMeetingsList = new List<TASK>();
                var Errormsg = new SqlParameter
                {
                    ParameterName = "@ErrorMsg",
                    Size = -1,
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output
                };
                using (IDbConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();


                    groupMeetingsList = con.Query<TASK>("Select * from Tasks where isdeleted=0").ToList();
                    return new Tuple<List<TASK>, dynamic>(groupMeetingsList, Errormsg.Value);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  

        }
        public Tuple<int, dynamic> AddAppointments(TASK groupMeeting, string ClientId, string ConnectionString)
        {
            var Errormsg = new SqlParameter
            {
                ParameterName = "@ErrorMsg",
                Size = -1,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Output
            };

            try

            {
                string strConnectionString = GetConnectionString(ConnectionString, ClientId);
                int rowAffected = 0;
                using (SqlConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();



                    string query = "insert into Tasks (remark,StartTime,Label,EndTime,LakNum,OwnerIds,LtName,PLACE,Teur,TIK_IN,WITHWHO,AllDayEvent) values('" + groupMeeting.Remark + "','" + Convert.ToDateTime(groupMeeting.StartTime).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "','" + groupMeeting.Label + "','" + Convert.ToDateTime(groupMeeting.EndTime).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "','" + groupMeeting.lakNum + "','" + groupMeeting.OwnerIds + "','" + groupMeeting.LtName + "','" + groupMeeting.PLACE + "','" + groupMeeting.Teur + "','" + groupMeeting.TIK + "','" + groupMeeting.withwho + "','" + groupMeeting.AllDayEvent + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    rowAffected = cmd.ExecuteNonQuery();
                }




                return new Tuple<int, dynamic>(rowAffected, Errormsg.Value);
            }
            catch (Exception e) { throw e.InnerException; }
        }

        public Tuple<int, dynamic> UpdateAppointments(string PrimekeyName, TASK value, string ClientId, string ConnectionString, int id)
        {
            var Errormsg = new SqlParameter
            {
                ParameterName = "@ErrorMsg",
                Size = -1,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Output
            };

            try

            {
                string strConnectionString = GetConnectionString(ConnectionString, ClientId);
                int rowAffected = 0;
                using (SqlConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();




                    //rowAffected = cmd.ExecuteNonQuery();
                    rowAffected = UpdateAppointment(PrimekeyName, id, value, "", strConnectionString);
                }




                return new Tuple<int, dynamic>(rowAffected, Errormsg.Value);
            }
            catch (Exception e) { throw e.InnerException; }
        }

        public int UpdateAppointment(string PrimekeyName, int id, TASK value, string ss, string strConnectionString)
        {
            // DataSet ds = new DataSet();
            // return ds;
            string strSQL = "SELECT * FROM Tasks where " + PrimekeyName + "=" + id + "";
            DataRow Ts = default(DataRow);
            SqlConnection con = new SqlConnection(strConnectionString);
            con.Open();

            SqlDataAdapter Adodapter = new SqlDataAdapter(strSQL, con); // ("SELECT * FROM [DOCS] WHERE [FullName]=@fullname", objConn)
            Adodapter.AcceptChangesDuringUpdate = true;
            Adodapter.AcceptChangesDuringFill = true;

            DataSet ds = new DataSet("TASKS");


            Adodapter.FillSchema(ds, SchemaType.Mapped, "TASKS");
            Adodapter.Fill(ds, "TASKS");

            SqlCommandBuilder cb = new SqlCommandBuilder(Adodapter);
            cb.GetInsertCommand();
            Adodapter.Update(ds.Tables[0]);


            if (ds.Tables["TASKS"].Rows.Count > 0)
                Ts = ds.Tables["TASKS"].Rows[0];
            else
            {
                Ts = ds.Tables["TASKS"].NewRow();

            }


            SqlCommand cmd = new SqlCommand(strSQL, con);

            //   Ts["teur"] = value.Teur;
            Ts["teur"] = value.Teur;
            Ts["ownerIds"] = value.OwnerIds;
            Ts["starttime"] = value.StartTime;
            Ts["endtime"] = value.EndTime;
            Ts["allDayEvent"] = value.AllDayEvent;
            Ts["label"] = value.Label;
            Ts["ltName"] = value.LtName;
            Ts["remark"] = value.Remark;
            Ts["place"] = value.PLACE;
            Ts["tiK"] = value.TIK;
            Ts["withwho"] = value.withwho;
            Ts["lakNum"] = value.lakNum;

            Ts["attendees"] = value.Teur;

            return Adodapter.Update(ds, "TASKS");
        }
        public Tuple<int, dynamic> UpdateUserSettings(UsersSettingsViewModel basicinfo, string ClientId, string ConnectionString, string emailId)
        {
            var Errormsg = new SqlParameter
            {
                ParameterName = "@ErrorMsg",
                Size = -1,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Output
            };


            string[] OldList = basicinfo.Settings.Resoucresgroup.Split(',');
            foreach (var item in basicinfo.owners)
            {
                if (item.IsCheked)
                {
                    OldList[item.Id - 1] = "1";
                }
                else
                {
                    OldList[item.Id - 1] = "0";
                }


            }
            string newvalue = String.Join(",", OldList);

            try

            {
                string strConnectionString = GetConnectionString(ConnectionString, ClientId);
                int rowAffected = 0;
                using (SqlConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var IsCrud = basicinfo.Settings.IsCrud == true ? 1 : 0;
                    var StartViewAsMerged = basicinfo.Settings.StartViewAsMerged == true ? 1 : 0;
                    var IsShowDeletedRecords = basicinfo.Settings.IsShowDeletedRecords == true ? 1 : 0;

                    string query = "update UserSettings set CellDuration='" + basicinfo.Settings.CellDuration + "', Resoucresgroup='" + newvalue + "',IsCrud=" + IsCrud + ",StartViewAsMerged=" + StartViewAsMerged + ",IsShowDeletedRecords=" + IsShowDeletedRecords + ",StartHour=" + basicinfo.Settings.StartHour + ",EndHour=" + basicinfo.Settings.EndHour + " where email='" + emailId + "'";
                    SqlCommand cmd = new SqlCommand(query, con);

                    //
                    //  Resoucresgroup(basicinfo, strConnectionString);

                    //string query = "update Tasks set teur='" + value.Teur + "',ownerIds='" + value.OwnerIds + "',starttime ='" + Convert.ToDateTime(value.StartTime).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "', endtime='" + Convert.ToDateTime(value.EndTime).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' ,allDayEvent='" + value.AllDayEvent + "',label='" + value.Label + "', ltName='" + value.LtName + "',remark='" + value.Remark + "',place='" + value.PLACE + "',tiK_IN='" + value.TIK_IN + "',withwho='" + value.withwho + "',lakNum='" + value.lakNum + "',attendees='" + value.attendees + "' where " + PrimekeyName + "=" + id + "";
                    //SqlCommand cmd = new SqlCommand(query, con);
                    rowAffected = cmd.ExecuteNonQuery();
                }




                return new Tuple<int, dynamic>(rowAffected, Errormsg.Value);
            }
            catch (Exception e) { throw e.InnerException; }
        }
        //private void Resoucresgroup(UsersSettingsViewModel basicinfo, string strConnectionString)
        //{
        //    using (SqlConnection con = new SqlConnection(strConnectionString))
        //    {
        //        if (con.State == ConnectionState.Closed)
        //            con.Open();

        //        var IsCrud = basicinfo.Settings.IsCrud == true ? 1 : 0;


        //        string query = "update Resources set IsCheked=" + IsCrud +  " where email='" + emailId + "'";
        //        SqlCommand cmd = new SqlCommand(query, con);



        //        //string query = "update Tasks set teur='" + value.Teur + "',ownerIds='" + value.OwnerIds + "',starttime ='" + Convert.ToDateTime(value.StartTime).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "', endtime='" + Convert.ToDateTime(value.EndTime).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' ,allDayEvent='" + value.AllDayEvent + "',label='" + value.Label + "', ltName='" + value.LtName + "',remark='" + value.Remark + "',place='" + value.PLACE + "',tiK_IN='" + value.TIK_IN + "',withwho='" + value.withwho + "',lakNum='" + value.lakNum + "',attendees='" + value.attendees + "' where " + PrimekeyName + "=" + id + "";
        //        //SqlCommand cmd = new SqlCommand(query, con);
        //       var rowAffected = cmd.ExecuteNonQuery();
        //    }
        //}
        private UserSettings GetUserInfo(string ClientId, string ConnectionString)
        {
            List<UserSettings> usersettings = new List<UserSettings>();
            string strConnectionString = GetConnectionString(ConnectionString, ClientId);
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                usersettings = con.Query<UserSettings>("Select * from UserSettings").ToList();
            }

            return usersettings.FirstOrDefault();
        }
        public Tuple<int, dynamic> DeleteAppointments(string PrimekeyName, string clinetId, string ConnectionString, int KeyValueForDelete)
        {
            var Errormsg = new SqlParameter
            {
                ParameterName = "@ErrorMsg",
                Size = -1,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Output
            };

            try

            {

                string strConnectionString = GetConnectionString(ConnectionString, clinetId);
                int rowAffected = 0;
                using (SqlConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();


                    string query = "update Tasks set IsDeleted=" + 1 + "  from Tasks where " + PrimekeyName + "=" + KeyValueForDelete + "";
                    SqlCommand cmd = new SqlCommand(query, con);
                    rowAffected = cmd.ExecuteNonQuery();

                }




                return new Tuple<int, dynamic>(rowAffected, Errormsg.Value);
            }
            catch (Exception e) { throw e.InnerException; }
        }

        public Tuple<UserSettings, dynamic> GetUserSettings(string ClinetId, string ConnectionString, string Email)
        {
            UserSettings groupMeetingsList = new UserSettings();
            try
            {
                string strConnectionStringFix = @"Server=tcp:23.97.186.48\\mssqlserver,50841;Database=CALT" + ClinetId + "; User ID=sa; Password=r4199357!;MultipleActiveResultSets=true";
                string strConnectionString = String.IsNullOrEmpty(ConnectionString) == true ? strConnectionStringFix : ConnectionString;
                // List<TASK> groupMeetingsList = new List<TASK>();
                var Errormsg = new SqlParameter
                {
                    ParameterName = "@ErrorMsg",
                    Size = -1,
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output
                };
                using (IDbConnection con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    groupMeetingsList = con.Query<UserSettings>("Select * from UserSettings where Email='" + Email + "'").FirstOrDefault();
                    return new Tuple<UserSettings, dynamic>(groupMeetingsList, Errormsg.Value);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  

        }

        private string GetConnectionString(string ConnectionString, string clinetId)
        {
            string strConnectionStringFix = @"Server=tcp:23.97.186.48\\mssqlserver,50841;Database=CALT" + clinetId + "; User ID=sa; Password=r4199357!;MultipleActiveResultSets=true";
            return String.IsNullOrEmpty(ConnectionString) == true ? strConnectionStringFix : ConnectionString;
        }
    }
//    }using Dapper;
//using Microsoft.EntityFrameworkCore;
//using scheduler.EF.ComplexTypes;
//using scheduler.Model;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace scheduler.EF.Model
//    {


//        public partial class SchedulerContext : DbContext
//        {
//            public SchedulerContext()
//            {
//            }

//            //public SchedulerContext()
//            //{
//            //}

//            public SchedulerContext(DbContextOptions<SchedulerContext> options)
//                    : base(options)
//            {
//            }
//            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//            {
//                if (!optionsBuilder.IsConfigured)
//                {
//                    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                    optionsBuilder.UseSqlServer(@"Server=tcp:81.218.198.91\SQLEXPRESS2014,49682;Database=CALT83203; User ID=Terem; Password=terem32!;MultipleActiveResultSets=true");
//                }
//            }

//            public DbSet<TASK> TASKs { get; set; }
//            public DbSet<Label> Labels { get; set; }
//            public DbSet<Resources> Resources { get; set; }
//            public DbSet<UserSettings> UserSettings { get; set; }


//            public async Task<Tuple<List<TempData>, dynamic>> GetLabels(string ClinetId, string ConnectionString)
//            {
//                Label result = new Label();
//                try
//                {
//                    string strConnectionString = GetConnectionString(ConnectionString, ClinetId);
//                    List<Label> groupMeetingsList = new List<Label>();
//                    var Errormsg = new SqlParameter
//                    {
//                        ParameterName = "@ErrorMsg",
//                        Size = -1,
//                        DbType = System.Data.DbType.String,
//                        Direction = System.Data.ParameterDirection.Output
//                    };
//                    using (IDbConnection con = new SqlConnection(strConnectionString))
//                    {
//                        if (con.State == ConnectionState.Closed)
//                            con.Open();

//                        //string sqlQuery = "select * from Task";
//                        // groupMeetingsList = await this.Query<TASK>().FromSql(sqlQuery, Errormsg).ToListAsync();
//                        List<TempData> obj = new List<TempData>();
//                        groupMeetingsList = con.Query<Label>("Select * from labels").ToList();
//                        foreach (var item in groupMeetingsList)
//                        {
//                            obj.Add(new TempData { color = item.Color, id = item.PrimeID, IsCheked = false, text = item.SugErua });
//                        }
//                        return new Tuple<List<TempData>, dynamic>(obj, Errormsg.Value);
//                    }

//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }

//                // Info.  

//            }

//            public async Task<Tuple<List<TempData>, dynamic>> GetResources(string ClientId, string ConnectionString, string emailId)
//            {
//                TASK result = new TASK();
//                try
//                {
//                    string strConnectionString = GetConnectionString(ConnectionString, ClientId);
//                    List<Resources> groupMeetingsList = new List<Resources>();
//                    var Errormsg = new SqlParameter
//                    {
//                        ParameterName = "@ErrorMsg",
//                        Size = -1,
//                        DbType = System.Data.DbType.String,
//                        Direction = System.Data.ParameterDirection.Output
//                    };

//                    int idex = 0;
//                    using (IDbConnection con = new SqlConnection(strConnectionString))
//                    {
//                        if (con.State == ConnectionState.Closed)
//                            con.Open();
//                        var CurrentUserSettings = con.Query<UserSettings>("Select * from UserSettings where Email='" + emailId + "'").FirstOrDefault();
//                        string uidS = CurrentUserSettings.Resoucresgroup;
//                        string[] Settings = uidS.Split(',');
//                        groupMeetingsList = con.Query<Resources>("Select * from Resources").ToList();




//                        List<TempData> obj = new List<TempData>();
//                        foreach (var item in groupMeetingsList)
//                        {
//                            var ss = Settings[idex];
//                            obj.Add(new TempData { color = item.Color, id = item.UniqueID, IsCheked = Settings[idex] == "1" ? true : false, text = item.ResourceName });
//                            idex++;
//                        }

//                        return new Tuple<List<TempData>, dynamic>(obj, Errormsg.Value);
//                        //return new Tuple<List<TempData>, dynamic>(groupMeetingsList, Errormsg.Value);
//                    }

//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }

//                // Info.  

//            }


//            public IEnumerable<dynamic> GetAppointmentbyDate(string ClientId, string ConnectionString, bool isdelete, DateTime startTimeold, DateTime endTimeold)
//            {
//                TASK result = new TASK();
//                var startTime = startTimeold.Date.ToString("yyyy-MM-dd HH:mm:ss");
//                var endTime = endTimeold.Date.ToString("yyyy-MM-dd HH:mm:ss");
//                string test = "SELECT  * FROM    Tasks WHERE   startTime >= '" + startTime + "' AND EndTime   <= '" + endTime + "'  and IsDeleted = 0";

//                List<dynamic> groupMeetingsList = new List<dynamic>();



//                try
//                {
//                    string strConnectionString = GetConnectionString(ConnectionString, ClientId);
//                    // List<TASK> groupMeetingsList = new List<TASK>();
//                    var Errormsg = new SqlParameter
//                    {
//                        ParameterName = "@ErrorMsg",
//                        Size = -1,
//                        DbType = System.Data.DbType.String,
//                        Direction = System.Data.ParameterDirection.Output
//                    };


//                    using (IDbConnection con = new SqlConnection(strConnectionString))
//                    {
//                        if (con.State == ConnectionState.Closed)
//                            con.Open();


//                        if (isdelete)
//                            groupMeetingsList = con.Query<dynamic>("SELECT * FROM    Tasks WHERE   startTime >= '" + startTime + "' AND EndTime <= '" + endTime + "'").ToList();
//                        else
//                            groupMeetingsList = con.Query<dynamic>("SELECT * FROM    Tasks WHERE   startTime >= '" + startTime + "' AND EndTime   <= '" + endTime + "'  and IsDeleted = 0").ToList();

//                        return groupMeetingsList;
//                    }

//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }

//                // Info.  

//            }
//            public Tuple<List<TASK>, dynamic> GetAppointments(string ClientId, string ConnectionString)
//            {
//                TASK result = new TASK();
//                try
//                {
//                    string strConnectionString = GetConnectionString(ConnectionString, ClientId);
//                    List<TASK> groupMeetingsList = new List<TASK>();
//                    var Errormsg = new SqlParameter
//                    {
//                        ParameterName = "@ErrorMsg",
//                        Size = -1,
//                        DbType = System.Data.DbType.String,
//                        Direction = System.Data.ParameterDirection.Output
//                    };
//                    using (IDbConnection con = new SqlConnection(strConnectionString))
//                    {
//                        if (con.State == ConnectionState.Closed)
//                            con.Open();


//                        groupMeetingsList = con.Query<TASK>("Select * from Tasks where isdeleted=0").ToList();
//                        return new Tuple<List<TASK>, dynamic>(groupMeetingsList, Errormsg.Value);
//                    }

//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }

//                // Info.  

//            }
//            public Tuple<int, dynamic> AddAppointments(TASK groupMeeting, string ClientId, string ConnectionString)
//            {
//                var Errormsg = new SqlParameter
//                {
//                    ParameterName = "@ErrorMsg",
//                    Size = -1,
//                    DbType = System.Data.DbType.String,
//                    Direction = System.Data.ParameterDirection.Output
//                };

//                try

//                {
//                    string strConnectionString = GetConnectionString(ConnectionString, ClientId);
//                    int rowAffected = 0;
//                    using (SqlConnection con = new SqlConnection(strConnectionString))
//                    {
//                        if (con.State == ConnectionState.Closed)
//                            con.Open();



//                        string query = "insert into Tasks (remark,StartTime,Label,EndTime,LakNum,OwnerIds,LtName,PLACE,Teur,TIK_IN,WITHWHO,AllDayEvent) values('" + groupMeeting.Remark + "','" + Convert.ToDateTime(groupMeeting.StartTime).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "','" + groupMeeting.Label + "','" + Convert.ToDateTime(groupMeeting.EndTime).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "','" + groupMeeting.lakNum + "','" + groupMeeting.OwnerIds + "','" + groupMeeting.LtName + "','" + groupMeeting.PLACE + "','" + groupMeeting.Teur + "','" + groupMeeting.TIK_IN + "','" + groupMeeting.withwho + "','" + groupMeeting.AllDayEvent + "')";
//                        SqlCommand cmd = new SqlCommand(query, con);
//                        rowAffected = cmd.ExecuteNonQuery();
//                    }




//                    return new Tuple<int, dynamic>(rowAffected, Errormsg.Value);
//                }
//                catch (Exception e) { throw e.InnerException; }
//            }

//            public Tuple<int, dynamic> UpdateAppointments(string PrimekeyName, TASK value, string ClientId, string ConnectionString, int id)
//            {
//                var Errormsg = new SqlParameter
//                {
//                    ParameterName = "@ErrorMsg",
//                    Size = -1,
//                    DbType = System.Data.DbType.String,
//                    Direction = System.Data.ParameterDirection.Output
//                };

//                try

//                {
//                    string strConnectionString = GetConnectionString(ConnectionString, ClientId);
//                    int rowAffected = 0;
//                    using (SqlConnection con = new SqlConnection(strConnectionString))
//                    {
//                        if (con.State == ConnectionState.Closed)
//                            con.Open();




//                        //rowAffected = cmd.ExecuteNonQuery();
//                        rowAffected = UpdateAppointment(PrimekeyName, id, value, "", strConnectionString);
//                    }




//                    return new Tuple<int, dynamic>(rowAffected, Errormsg.Value);
//                }
//                catch (Exception e) { throw e.InnerException; }
//            }

//            public int UpdateAppointment(string PrimekeyName, int id, TASK value, string ss, string strConnectionString)
//            {
//                // DataSet ds = new DataSet();
//                // return ds;
//                string strSQL = "SELECT * FROM Tasks where " + PrimekeyName + "=" + id + "";
//                DataRow Ts = default(DataRow);
//                SqlConnection con = new SqlConnection(strConnectionString);
//                con.Open();

//                SqlDataAdapter Adodapter = new SqlDataAdapter(strSQL, con); // ("SELECT * FROM [DOCS] WHERE [FullName]=@fullname", objConn)
//                Adodapter.AcceptChangesDuringUpdate = true;
//                Adodapter.AcceptChangesDuringFill = true;

//                DataSet ds = new DataSet("TASKS");


//                Adodapter.FillSchema(ds, SchemaType.Mapped, "TASKS");
//                Adodapter.Fill(ds, "TASKS");

//                SqlCommandBuilder cb = new SqlCommandBuilder(Adodapter);
//                cb.GetInsertCommand();
//                Adodapter.Update(ds.Tables[0]);


//                if (ds.Tables["TASKS"].Rows.Count > 0)
//                    Ts = ds.Tables["TASKS"].Rows[0];
//                else
//                {
//                    Ts = ds.Tables["TASKS"].NewRow();

//                }


//                SqlCommand cmd = new SqlCommand(strSQL, con);

//                //   Ts["teur"] = value.Teur;
//                Ts["teur"] = value.Teur;
//                Ts["ownerIds"] = value.OwnerIds;
//                Ts["starttime"] = value.StartTime;
//                Ts["endtime"] = value.EndTime;
//                Ts["allDayEvent"] = value.AllDayEvent;
//                Ts["label"] = value.Label;
//                Ts["ltName"] = value.LtName;
//                Ts["remark"] = value.Remark;
//                Ts["place"] = value.PLACE;
//                Ts["tiK_IN"] = value.TIK_IN;
//                Ts["withwho"] = value.withwho;
//                Ts["lakNum"] = value.lakNum;

//                Ts["attendees"] = value.Teur;

//                return Adodapter.Update(ds, "TASKS");
//            }
//            public Tuple<int, dynamic> UpdateUserSettings(UsersSettingsViewModel basicinfo, string ClientId, string ConnectionString, string emailId)
//            {
//                var Errormsg = new SqlParameter
//                {
//                    ParameterName = "@ErrorMsg",
//                    Size = -1,
//                    DbType = System.Data.DbType.String,
//                    Direction = System.Data.ParameterDirection.Output
//                };


//                string[] OldList = basicinfo.Settings.Resoucresgroup.Split(',');
//                foreach (var item in basicinfo.owners)
//                {
//                    if (item.IsCheked)
//                    {
//                        OldList[item.Id - 1] = "1";
//                    }
//                    else
//                    {
//                        OldList[item.Id - 1] = "0";
//                    }


//                }
//                string newvalue = String.Join(",", OldList);

//                try

//                {
//                    string strConnectionString = GetConnectionString(ConnectionString, ClientId);
//                    int rowAffected = 0;
//                    using (SqlConnection con = new SqlConnection(strConnectionString))
//                    {
//                        if (con.State == ConnectionState.Closed)
//                            con.Open();

//                        var IsCrud = basicinfo.Settings.IsCrud == true ? 1 : 0;
//                        var StartViewAsMerged = basicinfo.Settings.StartViewAsMerged == true ? 1 : 0;
//                        var IsShowDeletedRecords = basicinfo.Settings.IsShowDeletedRecords == true ? 1 : 0;

//                        string query = "update UserSettings set CellDuration='" + basicinfo.Settings.CellDuration + "', Resoucresgroup='" + newvalue + "',IsCrud=" + IsCrud + ",StartViewAsMerged=" + StartViewAsMerged + ",IsShowDeletedRecords=" + IsShowDeletedRecords + ",StartHour=" + basicinfo.Settings.StartHour + ",EndHour=" + basicinfo.Settings.EndHour + " where email='" + emailId + "'";
//                        SqlCommand cmd = new SqlCommand(query, con);

//                        //
//                        //  Resoucresgroup(basicinfo, strConnectionString);

//                        //string query = "update Tasks set teur='" + value.Teur + "',ownerIds='" + value.OwnerIds + "',starttime ='" + Convert.ToDateTime(value.StartTime).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "', endtime='" + Convert.ToDateTime(value.EndTime).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' ,allDayEvent='" + value.AllDayEvent + "',label='" + value.Label + "', ltName='" + value.LtName + "',remark='" + value.Remark + "',place='" + value.PLACE + "',tiK_IN='" + value.TIK_IN + "',withwho='" + value.withwho + "',lakNum='" + value.lakNum + "',attendees='" + value.attendees + "' where " + PrimekeyName + "=" + id + "";
//                        //SqlCommand cmd = new SqlCommand(query, con);
//                        rowAffected = cmd.ExecuteNonQuery();
//                    }




//                    return new Tuple<int, dynamic>(rowAffected, Errormsg.Value);
//                }
//                catch (Exception e) { throw e.InnerException; }
//            }
//            //private void Resoucresgroup(UsersSettingsViewModel basicinfo, string strConnectionString)
//            //{
//            //    using (SqlConnection con = new SqlConnection(strConnectionString))
//            //    {
//            //        if (con.State == ConnectionState.Closed)
//            //            con.Open();

//            //        var IsCrud = basicinfo.Settings.IsCrud == true ? 1 : 0;


//            //        string query = "update Resources set IsCheked=" + IsCrud +  " where email='" + emailId + "'";
//            //        SqlCommand cmd = new SqlCommand(query, con);



//            //        //string query = "update Tasks set teur='" + value.Teur + "',ownerIds='" + value.OwnerIds + "',starttime ='" + Convert.ToDateTime(value.StartTime).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "', endtime='" + Convert.ToDateTime(value.EndTime).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' ,allDayEvent='" + value.AllDayEvent + "',label='" + value.Label + "', ltName='" + value.LtName + "',remark='" + value.Remark + "',place='" + value.PLACE + "',tiK_IN='" + value.TIK_IN + "',withwho='" + value.withwho + "',lakNum='" + value.lakNum + "',attendees='" + value.attendees + "' where " + PrimekeyName + "=" + id + "";
//            //        //SqlCommand cmd = new SqlCommand(query, con);
//            //       var rowAffected = cmd.ExecuteNonQuery();
//            //    }
//            //}
//            private UserSettings GetUserInfo(string ClientId, string ConnectionString)
//            {
//                List<UserSettings> usersettings = new List<UserSettings>();
//                string strConnectionString = GetConnectionString(ConnectionString, ClientId);
//                using (IDbConnection con = new SqlConnection(strConnectionString))
//                {
//                    if (con.State == ConnectionState.Closed)
//                        con.Open();

//                    usersettings = con.Query<UserSettings>("Select * from UserSettings").ToList();
//                }

//                return usersettings.FirstOrDefault();
//            }
//            public Tuple<int, dynamic> DeleteAppointments(string PrimekeyName, string clinetId, string ConnectionString, int KeyValueForDelete)
//            {
//                var Errormsg = new SqlParameter
//                {
//                    ParameterName = "@ErrorMsg",
//                    Size = -1,
//                    DbType = System.Data.DbType.String,
//                    Direction = System.Data.ParameterDirection.Output
//                };

//                try

//                {

//                    string strConnectionString = GetConnectionString(ConnectionString, clinetId);
//                    int rowAffected = 0;
//                    using (SqlConnection con = new SqlConnection(strConnectionString))
//                    {
//                        if (con.State == ConnectionState.Closed)
//                            con.Open();


//                        string query = "update Tasks set IsDeleted=" + 1 + "  from Tasks where " + PrimekeyName + "=" + KeyValueForDelete + "";
//                        SqlCommand cmd = new SqlCommand(query, con);
//                        rowAffected = cmd.ExecuteNonQuery();

//                    }




//                    return new Tuple<int, dynamic>(rowAffected, Errormsg.Value);
//                }
//                catch (Exception e) { throw e.InnerException; }
//            }

//            public Tuple<UserSettings, dynamic> GetUserSettings(string ClinetId, string ConnectionString, string Email)
//            {
//                UserSettings groupMeetingsList = new UserSettings();
//                try
//                {
//                    string strConnectionStringFix = "Server=tcp:192.168.1.3\\mssqlserver,50841;Database=CALT" + ClinetId + "; User ID=sa; Password=r4199357!;MultipleActiveResultSets=true";
//                    string strConnectionString = String.IsNullOrEmpty(ConnectionString) == true ? strConnectionStringFix : ConnectionString;
//                    // List<TASK> groupMeetingsList = new List<TASK>();
//                    var Errormsg = new SqlParameter
//                    {
//                        ParameterName = "@ErrorMsg",
//                        Size = -1,
//                        DbType = System.Data.DbType.String,
//                        Direction = System.Data.ParameterDirection.Output
//                    };
//                    using (IDbConnection con = new SqlConnection(strConnectionString))
//                    {
//                        if (con.State == ConnectionState.Closed)
//                            con.Open();

//                        groupMeetingsList = con.Query<UserSettings>("Select * from UserSettings where Email='" + Email + "'").FirstOrDefault();
//                        return new Tuple<UserSettings, dynamic>(groupMeetingsList, Errormsg.Value);
//                    }

//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }

//                // Info.  

//            }

//            private string GetConnectionString(string ConnectionString, string clinetId)
//            {
//                string strConnectionStringFix = "Server=tcp:192.168.1.3\\mssqlserver,50841;Database=CALT" + clinetId + "; User ID=sa; Password=r4199357!;MultipleActiveResultSets=true";
//                return String.IsNullOrEmpty(ConnectionString) == true ? strConnectionStringFix : ConnectionString;
//            }
//        }
//    }

}
