using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using TARONEFMS.Models;

namespace TARONEFMS.Utilities
{
    public class LoginUtil
    {
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ToString();

        public UserLoginModel GetUserLoginByCredentials(string username, string password)
        {
            UserLoginModel ulm = new UserLoginModel();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("select id,username,password,firstname + ' ' + left(middlename,1)+'. '+lastname [name] " +
                                                         "from tblUsers where username=? and password=?;", con);

                    ocmd.Parameters.AddWithValue("@username", username);
                    ocmd.Parameters.AddWithValue("@password", password);

                    OleDbDataReader ordr = ocmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            ulm.Id = Convert.ToInt32(ordr["id"]);
                            ulm.UserId = ordr["username"].ToString();
                            ulm.Password = ordr["password"].ToString();
                            ulm.Name = ordr["name"].ToString();
                        }
                    }
                    else
                        ulm = null;
                }
                catch (Exception)
                {
                    ulm = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return ulm;

        }

        
    }
}