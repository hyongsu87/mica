using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mica.Models;

namespace Mica.Controllers
{
    public class BanksController : Controller
    {
        private ApplicationDbContext _context;
        public BanksController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Banks
        public ActionResult List()
        {
            var banks = _context.Banks.ToList();
            return View("List", banks);
        }
    }
}