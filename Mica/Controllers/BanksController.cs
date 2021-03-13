using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mica.Controllers
{
    public class BanksController : Controller
    {

        // GET: Banks
        public ActionResult Index()
        {
            return View();
        }
    }
}