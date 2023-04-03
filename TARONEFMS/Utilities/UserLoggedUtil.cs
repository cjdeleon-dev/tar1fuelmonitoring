using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using TARONEFMS.Models;

namespace TARONEFMS.Utilities
{
    public class UserLoggedUtil
    {
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ConnectionString;

        public UserLoggedModel GetUserInfo(int id)
        {
            UserLoggedModel ulm = new UserLoggedModel();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("select usr.Id, UPPER(usr.firstname + ' ' + left(usr.middlename,1) + '. ' + usr.lastname) [name], pos.position, usr.userpic " +
                                                         "from tblUsers usr inner join tblPositions pos on usr.positionid = pos.id " +
                                                         "where usr.id = ?; ", con);

                    ocmd.Parameters.AddWithValue("@id", id);
                    
                    OleDbDataReader ordr = ocmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            ulm.Id = Convert.ToInt32(ordr["id"]);
                            ulm.Name = ordr["name"].ToString();
                            ulm.Position = ordr["position"].ToString();
                            ulm.UserPic = ordr["userpic"] != DBNull.Value ? (byte[])ordr["userpic"] : null;
                            if (ulm.UserPic == null)
                            {
                                MemoryStream ms = new MemoryStream();
                                string vp = HostingEnvironment.ApplicationVirtualPath;
                                if (vp == "/fuelmonapp")
                                {
                                    //To be changed when publish to online "/fuelmonapp"
                                    Image img = Image.FromFile(HostingEnvironment.MapPath("/fuelmonapp/Content/images/UserDefault.jpg"));
                                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                                else
                                {
                                    //To be changed when publish to online "/fuelmonapp"
                                    Image img = Image.FromFile(HostingEnvironment.MapPath("/Content/images/UserDefault.jpg"));
                                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                                

                                ulm.UserPic = ms.ToArray();
                            }
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