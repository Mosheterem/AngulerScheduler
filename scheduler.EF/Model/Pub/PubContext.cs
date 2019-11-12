using Microsoft.EntityFrameworkCore;
using scheduler.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.EF.Model.Pub
{
    public partial class PubContext : DbContext
    {
        public PubContext()
        {
        }


        public PubContext(DbContextOptions<PubContext> options)
                : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=tcp:81.218.198.91\SQLEXPRESS2014,49682;Database=Pub; User ID=Terem; Password=terem32!;MultipleActiveResultSets=true");
            }
        }
        private string GetConnectionString()
        {
            return @"Server=tcp:23.97.186.48\\mssqlserver,50841;Database=PUB; User ID=sa; Password=r4199357!;MultipleActiveResultSets=true";

        }
        public DbSet<FeedBack> FeedBacks { get; set; }

       

        /// <summary>
        /// AddFirstCall
        /// </summary>
        /// <param name="EmailId"></param>
        /// <returns></returns>
        public Tuple<int, dynamic> AddFirstCall(string ResponseKey,string secKey)
        {
            FeedBack result = new FeedBack();
            try
            {
                string strConnectionString = GetConnectionString();

                var Errormsg = new SqlParameter
                {
                    ParameterName = "@ErrorMsg",
                    Size = -1,
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output
                };
                using (var con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    string query = "insert into Feedback (ResponseKey,ResponseSecKey) values('" + ResponseKey + "','"+secKey+"') SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, con);
                    int modified = Convert.ToInt32(cmd.ExecuteScalar());
                    return new Tuple<int, dynamic>(modified, Errormsg.Value);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            // Info.  

        }

        private string Search(string key,string secKey)
        {
           
            SqlCommand command;
            SqlDataReader dreader;
            string responceKey = "";
            FeedBack result = new FeedBack();
            try
            {
                string strConnectionString = GetConnectionString();

                var Errormsg = new SqlParameter
                {
                    ParameterName = "@ErrorMsg",
                    Size = -1,
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output
                };

             
                using (var con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();


                    string query = "select ResponseKey from FeedBack where key = " + key + " ";
                    command = new SqlCommand(query, con);
                    try
                    {
                        dreader = command.ExecuteReader();
                        if (dreader.Read())
                        {
                            responceKey = dreader[1].ToString();

                            return responceKey;


                        }
                        else
                        {
                            dreader.Close();
                            return responceKey;
                        }
                       
                    }
                    catch (Exception)
                    {
                        return responceKey;
                    }
                    finally
                    {
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            // comm = new SqlCommand("select * from student_detail where roll_no = " + txtrn.Text + " ", conn);

        }
        /// <summary>
        /// AddFeedback
        /// </summary>
        /// <param name="feedBack"></param>
        /// <returns></returns>
        public Tuple<bool, dynamic> AddFeedback(FeedBack feedBack)
        {
            FeedBack result = new FeedBack();
            try
            {
                string strConnectionString = GetConnectionString();

                var Errormsg = new SqlParameter
                {
                    ParameterName = "@ErrorMsg",
                    Size = -1,
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output
                };

                int idex = 0;
                int rowAffected = 0;
                using (var con = new SqlConnection(strConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "insert into FeedBack (FullName,Email,Mobile,Note,ResponseTime,Seen) values('" + feedBack.FullName + "','" + feedBack.Email + "','" + feedBack.PhoneNumber + "','" + feedBack.Note + "','" + DateTime.Now + "','" + 0 + "')";

                    if (feedBack.Id>0)
                    {
                        query = "update FeedBack set fullname='" + feedBack.FullName + "',Email='"+feedBack.Email+ "',Mobile='" + feedBack.PhoneNumber + "',Note='" + feedBack.Note + "',ResponseTime='"+DateTime.Now+"',seen='"+0+ "' where id='" + feedBack.Id + "'";
                    }
                

                    SqlCommand cmd = new SqlCommand(query, con);
                    var ss = cmd.ExecuteNonQuery();
                    return new Tuple<bool, dynamic>(rowAffected > 0 ? true : false, Errormsg.Value);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}






