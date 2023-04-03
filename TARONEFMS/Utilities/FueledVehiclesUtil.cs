using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using TARONEFMS.Models;
using System.Web.Mvc;


namespace TARONEFMS.Utilities
{
    public class FueledVehiclesUtil
    {
        private string constr = ConfigurationManager.ConnectionStrings["getconnectionstring"].ConnectionString;

        public List<FueledVehicleModel> GetFueledVehiclesByUserId(int userid)
        {
            List<FueledVehicleModel> lstfvm = new List<FueledVehicleModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("sp_GetFueledVehiclesByUserId", con);
                    ocmd.CommandType = System.Data.CommandType.StoredProcedure;


                    ocmd.Parameters.Clear();
                    ocmd.Parameters.AddWithValue("@userid", userid);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstfvm.Add(new FueledVehicleModel
                            {
                                Id = Convert.ToInt32(ordr["id"]),
                                VehicleId = Convert.ToInt32(ordr["vehicleid"]),
                                Vehicle = ordr["vehicle"].ToString(),
                                VehicleNo = ordr["vehicleno"].ToString(),
                                PlateNo = ordr["plateno"].ToString(),
                                DateFueled = ordr["datefueled"].ToString(),
                                Liter = Convert.ToDouble(ordr["litersfueled"]),
                                Amount = Convert.ToDouble(ordr["amtfueled"]),
                                DateEntry = ordr["dateencoded"].ToString()
                            });
                        }
                    }
                    else
                        lstfvm = null;
                }
                catch (Exception)
                {
                    lstfvm = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstfvm;
        }

        public List<VehiclesModel> GetVehiclesPerOfficeIdOfUser(int userid)
        {
            List<VehiclesModel> lstvm = new List<VehiclesModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("sp_GetVehiclesPerOfficeIdOfUser", con);
                    ocmd.CommandType = System.Data.CommandType.StoredProcedure;

                    ocmd.Parameters.Clear();
                    ocmd.Parameters.AddWithValue("@userid", userid);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstvm.Add(new VehiclesModel
                            {
                                Id = Convert.ToInt32(ordr["id"]),
                                Vehicle = ordr["vehicle"].ToString(),
                                YearAcquired = ordr["yearacquired"].ToString(),
                                PlateNo = ordr["plateno"].ToString(),
                                EngineNo = ordr["engineno"].ToString(),
                                VehicleNo = ordr["vehicleno"].ToString(),
                                MakeId = Convert.ToInt32(ordr["makeid"]),
                                Make = ordr["make"].ToString(),
                                OfficeId = Convert.ToInt32(ordr["officeid"]),
                                Office = ordr["office"].ToString(),
                                StatusId = Convert.ToInt32(ordr["statusid"]),
                                Status = ordr["status"].ToString()
                            });
                        }
                    }
                    else
                        lstvm = null;
                }
                catch (Exception)
                {
                    lstvm = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstvm;
        }

        public List<VehiclesModel> GetAllVehicles()
        {
            List<VehiclesModel> lstvm = new List<VehiclesModel>();

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("sp_GetAllVehicles", con);
                    ocmd.CommandType = System.Data.CommandType.StoredProcedure;

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            lstvm.Add(new VehiclesModel
                            {
                                Id = Convert.ToInt32(ordr["id"]),
                                Vehicle = ordr["vehicle"].ToString(),
                                YearAcquired = ordr["yearacquired"].ToString(),
                                PlateNo = ordr["plateno"].ToString(),
                                EngineNo = ordr["engineno"].ToString(),
                                VehicleNo = ordr["vehicleno"].ToString(),
                                MakeId = Convert.ToInt32(ordr["makeid"]),
                                Make = ordr["make"].ToString(),
                                OfficeId = Convert.ToInt32(ordr["officeid"]),
                                Office = ordr["office"].ToString(),
                                StatusId = Convert.ToInt32(ordr["statusid"]),
                                Status = ordr["status"].ToString()
                            });
                        }
                    }
                    else
                        lstvm = null;
                }
                catch (Exception)
                {
                    lstvm = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return lstvm;
        }

        public ProcessModel InsertNewFueledVehicle(FueledVehicleModel fvm)
        {
            ProcessModel pm = new ProcessModel();

            if (fvm != null)
            {
                OleDbConnection con = new OleDbConnection(constr);
                OleDbTransaction trans = null;

                con.Open();
                trans = con.BeginTransaction();

                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    cmd.Transaction = trans;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "insert into tblFueledVehicles(vehicleid,vehicleplateno,datefueled,litersfueled,amtfueled," +
                                      "salesinvoiceno,gasslipno,gasstation,gasstationadd,isprivatecar,userid,dateencoded) " +
                                      "values(?,?,?,?,?,?,?,?,?,?,?,getdate())";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@vehicleid", fvm.VehicleId);
                    cmd.Parameters.AddWithValue("@vehicle", fvm.Vehicle);
                    cmd.Parameters.AddWithValue("@datefueled", fvm.DateFueled);
                    cmd.Parameters.AddWithValue("@litersfueled", fvm.Liter);
                    cmd.Parameters.AddWithValue("@amtfueled", fvm.Amount);
                    cmd.Parameters.AddWithValue("@salesinvoiceno", fvm.SalesInvoiceNo);
                    cmd.Parameters.AddWithValue("@gasslipno", fvm.GasSlipNo);
                    cmd.Parameters.AddWithValue("@gasstation", fvm.GasStation);
                    cmd.Parameters.AddWithValue("@gasstationadd", fvm.GasStationAddress);
                    cmd.Parameters.AddWithValue("@isprivatecar", fvm.IsCompanyVehicle);
                    cmd.Parameters.AddWithValue("@userid", fvm.UserId);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                        pm.IsSuccess = true;
                        pm.ProcessMessage = "Successfully saved.";
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        pm.IsSuccess = false;
                        pm.ProcessMessage = ex.Message;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

            return pm;
        }

        public DataTable FueledVehicleRptByDateRange(int uid, string datefrom, string dateto, string opttype = "summary")
        {
            DataTable dt = new DataTable();
            
            string literPivotHeader = GetLiterPivotHeader(datefrom, dateto);
            string amountPivotHeader = GetAmountPivotHeader(datefrom, dateto);
            string allPivotHeader = GetAllPivotHeader(datefrom, dateto);

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand();
                    ocmd.Connection = con;
                    ocmd.CommandType = System.Data.CommandType.Text;

                    if (opttype == "summary")
                    {

                        ocmd.CommandText = "select a.vehicle,a.plateno,a.office," + allPivotHeader +
                        "from " +
                        " (SELECT vehicle,plateno,office," + literPivotHeader +
                        "FROM " +
                        "( " +
                        "   select vehicle, plateno, office, modateltrs, liters " +
                        "   from " +
                        "   ( " +
                        "       select isnull(v.vehicle, 'OTHER')[vehicle], case when vehicleid = 0 then vehicleplateno else v.plateno end[plateno], " +
                        "       FORMAT(datefueled, 'MMM yyyy') + ' ltrs'[modateltrs], sum(litersfueled)[liters], " +
                        "       case when vehicleid = 0 then ou.officedept else o.officedept end[office] " +
                        "       from tblFueledVehicles fv left join tblVehicles v " +
                        "       on fv.vehicleid = v.id " +
                        "       inner join tblUsers u on fv.userid = u.id " +
                        "       left join tblOffices o on v.officeid = o.id " +
                        "       left join tblOffices ou on u.officeid = ou.id " +
                        "       where datefueled between ? and ? " +
                        "       group by vehicle,case when vehicleid = 0 then vehicleplateno else v.plateno end, " +
                        "       FORMAT(datefueled, 'MMM yyyy') + ' ltrs', " +
                        "       case when vehicleid = 0 then ou.officedept else o.officedept end " +
                        "   ) src " +
                        ") as P " +
                        "PIVOT " +
                        "( " +
                        "   MAX(liters) for modateltrs IN (" + literPivotHeader + ") " +
                        ") pivotLiters) a " +
                        "inner join " +
                        "(select * " +
                        "from " +
                        "( " +
                        "   SELECT vehicle, plateno, office, " + amountPivotHeader +
                        "   FROM " +
                        "   ( " +
                        "       select vehicle, plateno, office,[modateamt], amounts " +
                        "       from " +
                        "       ( " +
                        "           select isnull(v.vehicle, 'OTHER')[vehicle]," +
                        "           case when vehicleid = 0 then vehicleplateno else v.plateno end[plateno]," +
                        "           FORMAT(datefueled, 'MMM yyyy') + ' amt'[modateamt], sum(amtfueled)[amounts]," +
                        "           case when vehicleid = 0 then ou.officedept else o.officedept end[office] " +
                        "           from tblFueledVehicles fv left join tblVehicles v " +
                        "           on fv.vehicleid = v.id " +
                        "           inner join tblUsers u on fv.userid = u.id " +
                        "           left join tblOffices o on v.officeid = o.id " +
                        "           left join tblOffices ou on u.officeid = ou.id " +
                        "           where datefueled between ? and ? " +
                        "           group by vehicle,case when vehicleid = 0 then vehicleplateno else v.plateno end," +
                        "           FORMAT(datefueled, 'MMM yyyy') + ' amt', " +
                        "           case when vehicleid = 0 then ou.officedept else o.officedept end " +
                        "       ) src " +
                        ") as P " +
                        "PIVOT " +
                        "( " +
                        "   MAX(amounts) for modateamt IN (" + amountPivotHeader + ")" +
                        ") pivotAmount " +
                        ")x) as b " +
                        "on a.vehicle = b.vehicle and a.plateno = b.plateno and a.office = b.office " +
                        "order by a.office;";

                        ocmd.Parameters.AddWithValue("@datefrltrs", datefrom);
                        ocmd.Parameters.AddWithValue("@datetoltrs", dateto);
                        ocmd.Parameters.AddWithValue("@dateframt", datefrom);
                        ocmd.Parameters.AddWithValue("@datetoamt", dateto);
                    }
                    else if(opttype=="byuser" && uid!=1 && uid!=2 && uid!=4)
                    {
                        ocmd.CommandText = "select isnull(v.vehicle, 'OTHER')[vehicle],case when vehicleid = 0 then vehicleplateno else v.plateno end[plateno]," +
                                           "datefueled, fv.litersfueled,amtfueled,salesinvoiceno,gasslipno,gasstation,gasstationadd,o.officedept " +
                                           "from tblFueledVehicles fv " +
                                           "inner join tblUsers u " +
                                           "on fv.userid = u.id " +
                                           "left join tblVehicles v " +
                                           "on fv.vehicleid = v.id " +
                                           "inner join tblOffices o " +
                                           "on u.officeid = o.id " +
                                           "where userid = ? and datefueled between ? and ? " +
                                           "union all " +
                                           "select 'TOTAL'[vehicle],''[plateno],NULL,SUM(fv.litersfueled),SUM(amtfueled),'','','','','' " +
                                           "from tblFueledVehicles fv " +
                                           "where userid = ? and datefueled between ? and ?; ";

                        ocmd.Parameters.AddWithValue("@userid", uid);
                        ocmd.Parameters.AddWithValue("@datefr", datefrom);
                        ocmd.Parameters.AddWithValue("@dateto", dateto);
                        ocmd.Parameters.AddWithValue("@useridu", uid);
                        ocmd.Parameters.AddWithValue("@datefru", datefrom);
                        ocmd.Parameters.AddWithValue("@datetou", dateto);
                    }
                    else
                    {
                        ocmd.CommandText = "select isnull(v.vehicle, 'OTHER')[vehicle],case when vehicleid = 0 then vehicleplateno else v.plateno end[plateno]," +
                                           "datefueled, fv.litersfueled,amtfueled,salesinvoiceno,gasslipno,gasstation,gasstationadd,o.officedept " +
                                           "from tblFueledVehicles fv " +
                                           "inner join tblUsers u " +
                                           "on fv.userid = u.id " +
                                           "left join tblVehicles v " +
                                           "on fv.vehicleid = v.id " +
                                           "inner join tblOffices o " +
                                           "on u.officeid = o.id " +
                                           "where datefueled between ? and ? " +
                                           "union all " +
                                           "select 'TOTAL'[vehicle],''[plateno],NULL,SUM(fv.litersfueled),SUM(amtfueled),'','','','','' " +
                                           "from tblFueledVehicles fv " +
                                           "where datefueled between ? and ?;";


                        ocmd.Parameters.AddWithValue("@datefr", datefrom);
                        ocmd.Parameters.AddWithValue("@dateto", dateto);
                        ocmd.Parameters.AddWithValue("@datefru", datefrom);
                        ocmd.Parameters.AddWithValue("@datetou", dateto);
                    }
                    

                    OleDbDataAdapter oda = new OleDbDataAdapter(ocmd);

                    oda.Fill(dt);

                }
                catch (Exception ex)
                {
                    dt = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return dt;
        }

        private string GetLiterPivotHeader(string datefrom, string dateto)
        {
            string result = string.Empty;

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("sp_GetLiterPivotHeader", con);
                    ocmd.CommandType = System.Data.CommandType.StoredProcedure;


                    ocmd.Parameters.Clear();
                    ocmd.Parameters.AddWithValue("@datefr", datefrom);
                    ocmd.Parameters.AddWithValue("@dateto", dateto);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            result = ordr["hdrresult"].ToString();
                        }
                    }
                    else
                        result = string.Empty;
                }
                catch (Exception)
                {
                    result = string.Empty;
                }
                finally
                {
                    con.Close();
                }
            }

            return result;
        }

        private string GetAmountPivotHeader(string datefrom, string dateto)
        {
            string result = string.Empty;

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("sp_GetAmountPivotHeader", con);
                    ocmd.CommandType = System.Data.CommandType.StoredProcedure;


                    ocmd.Parameters.Clear();
                    ocmd.Parameters.AddWithValue("@datefr", datefrom);
                    ocmd.Parameters.AddWithValue("@dateto", dateto);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            result = ordr["hdrresult"].ToString();
                        }
                    }
                    else
                        result = string.Empty;
                }
                catch (Exception)
                {
                    result = string.Empty;
                }
                finally
                {
                    con.Close();
                }
            }

            return result;
        }

        private string GetAllPivotHeader(string datefrom, string dateto)
        {
            string result = string.Empty;

            using (OleDbConnection con = new OleDbConnection(constr))
            {
                try
                {
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("sp_GetPivotHeader", con);
                    ocmd.CommandType = System.Data.CommandType.StoredProcedure;


                    ocmd.Parameters.Clear();
                    ocmd.Parameters.AddWithValue("@datefr", datefrom);
                    ocmd.Parameters.AddWithValue("@dateto", dateto);

                    OleDbDataReader ordr = ocmd.ExecuteReader();

                    if (ordr.HasRows)
                    {
                        while (ordr.Read())
                        {
                            result = ordr["hdrresult"].ToString();
                        }
                    }
                    else
                        result = string.Empty;
                }
                catch (Exception)
                {
                    result = string.Empty;
                }
                finally
                {
                    con.Close();
                }
            }

            return result;
        }
    }
}