using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mica.Models;
using Mica.ViewModel;

namespace Mica.Controllers
{
    public class BanksController : Controller
    {
        private ApplicationDbContext _context;
        public BanksController()
        {
            _context = new ApplicationDbContext();
        }

        public BanksController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Banks
        public ActionResult Index()
        {
            var banks = _context.Banks.Include(b => b.Country).ToList(); // eager loading
            return View("List", banks);
        }
        /*
        public ActionResult GetBanks()
        {
            var banks = _context.Banks.ToList(); // eager loading
            return View("List", banks);
        }
        */
        public ActionResult Details(int id)
        {
            var bank = _context.Banks.Find(id);
            _context.Entry(bank).Reference(b => b.Country).Load(); // explicit loading

            return View("Details", bank);
        }

        public ActionResult New()
        {
            var countries = _context.Countries.ToList();
            var viewModel = new FormBanksViewModel
            {
                Countries = countries
            };

            return View("Form", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var bank = _context.Banks.Find(id);
            _context.Entry(bank).Reference(b => b.Country).Load(); // explicit loading
            var countries = _context.Countries.ToList();
            var viewModel = new FormBanksViewModel
            {
                Bank = bank,
                Countries = countries
            };

            return View("Form", viewModel);
        }

        [HttpPost]
        public ActionResult Update(Bank bank)
        {
            try
            {
                var bankInDb = _context.Banks.Single(b => b.Id == bank.Id);
                bankInDb.Name = bank.Name;
                bankInDb.CountryId = bank.CountryId;
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                return View("Error", e);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
            return RedirectToAction("Index", "Banks");
        }

        [HttpPost]
        public ActionResult Create(Bank bank)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Banks.Add(bank);
                    _context.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    return View("Error", e);
                }
                catch (Exception e)
                {
                    //string exceptiontype = e.InnerException.GetType().FullName;
                    //exceptiontype = e.InnerException.InnerException.GetType().FullName;
                    return View("Error", e);
                }

                return RedirectToAction("Index", "Banks");

            }

            var countries = _context.Countries.ToList();
            var viewModel = new FormBanksViewModel
            {
                Bank = bank,
                Countries = countries
            };

            return View("Form", viewModel);
        }
    }
}