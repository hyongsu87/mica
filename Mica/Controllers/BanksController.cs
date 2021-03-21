using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var banks = _context.Banks.Include(b => b.Country).ToList(); // eager loading
            return View("List", banks);
        }

        public ActionResult Details(int Id)
        {
            var bank = _context.Banks.Find(Id);
            _context.Entry(bank).Reference(b => b.Country).Load(); // explicit loading

            return View("Details", bank);
        }

        
    }
}