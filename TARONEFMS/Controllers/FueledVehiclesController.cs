using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TARONEFMS.Models;
using TARONEFMS.Utilities;
using Microsoft.Reporting.WebForms;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TARONEFMS.Controllers
{
    public class FueledVehiclesController : Controller
    {
        private static GridView gv = new GridView();
        // GET: FuelledVehicles
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult FueledVehiclesData()
        {
            int idid = Convert.ToInt32(User.Identity.Name);
            FueledVehiclesUtil fvu = new FueledVehiclesUtil();
            List<FueledVehicleModel> lstfvm = new List<FueledVehicleModel>();

            lstfvm = fvu.GetFueledVehiclesByUserId(idid);

            JsonResult jsonResult;
            if (lstfvm != null)
            {
                jsonResult = Json(new { data = lstfvm }, JsonRequestBehavior.AllowGet);
            }
            else
                jsonResult = Json(new { data = "[]" }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetVehiclesPerOfficeIdOfUser()
        {
            FueledVehiclesUtil fvu = new FueledVehiclesUtil();
            List<VehiclesModel> lstvm = new List<VehiclesModel>();

            int idid = Convert.ToInt32(User.Identity.Name);
            lstvm = fvu.GetVehiclesPerOfficeIdOfUser(idid);

            JsonResult jsonResult;
            jsonResult = Json(lstvm, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public JsonResult GetAllVehicles()
        {
            FueledVehiclesUtil fvu = new FueledVehiclesUtil();
            List<VehiclesModel> lstvm = new List<VehiclesModel>();

            lstvm = fvu.GetAllVehicles();

            JsonResult jsonResult;
            jsonResult = Json(lstvm, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public JsonResult NewEntryFueledVehicle(FueledVehicleModel fvm)
        {
            ProcessModel pm = new ProcessModel();
            FueledVehiclesUtil fvu = new FueledVehiclesUtil();

            fvm.UserId = Convert.ToInt32(User.Identity.Name);

            pm = fvu.InsertNewFueledVehicle(fvm);

            return Json(pm, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult FVReportView()
        //{
        //    LocalReport lr = new LocalReport();
        //    string p = Path.Combine(Server.MapPath("/Reports"), "rptFueledVehicle.rdlc");
        //    lr.ReportPath = p;

        //    DataSet ds = new DataSet();

        //    string mt, enc, f;
        //    string[] s;
        //    Warning[] w;

        //    //Rendering
        //    byte[] b = lr.Render("PDF", null, out mt, out enc, out f, out s, out w);

        //    return File(b, mt);
        //}

        [HttpPost]
        public ActionResult ExportToExcel(FormCollection formCollection)
        {
            FueledVehiclesUtil fvu = new FueledVehiclesUtil();

            string datefrom = formCollection["dtpDateFrom"].ToString();
            string dateto = formCollection["dtpDateTo"].ToString();
            string opt = formCollection["rbOption"].ToString();

            DataTable dt = new DataTable();
            if(opt=="summary")
                dt = fvu.FueledVehicleRptByDateRange(Convert.ToInt32(User.Identity.Name), datefrom, dateto);
            else
                dt = fvu.FueledVehicleRptByDateRange(Convert.ToInt32(User.Identity.Name), datefrom, dateto, opt);

            if (gv.DataSource != null)
                gv.DataSource = null;
            gv.DataSource = dt;
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
                                   
            try
            {
                Response.AddHeader("content-disposition", "attachment; filename=FueledVehicles_" + datefrom + 
                    "_" + dateto + "_" + opt + "_" + User.Identity.Name + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();

                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                return View("Index");
            }

            return View("Index");
        }
    }
}
