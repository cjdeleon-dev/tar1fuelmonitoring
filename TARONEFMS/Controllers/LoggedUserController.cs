using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TARONEFMS.Utilities;

namespace TARONEFMS.Controllers
{
    public class LoggedUserController : Controller
    {
        // GET: LoggedUser
        public PartialViewResult UserInfo()
        {
            UserLoggedUtil ulu = new UserLoggedUtil();

            int idid = Convert.ToInt32(User.Identity.Name);
                        
            return PartialView(ulu.GetUserInfo(idid));
        }
    }
}