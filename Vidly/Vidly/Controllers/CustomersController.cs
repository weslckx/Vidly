using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;
using System.Data.Entity.Migrations;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _dbContext;

        public CustomersController()
        {
            _dbContext = new ApplicationDbContext(); // without DI
        }

        protected override void Dispose(bool disposing) // without DI
        {
            _dbContext.Dispose();
        }


        // GET: Customers
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
      
           
            return View("ReadOnlyList");
        }

        public ActionResult Details(int id)
        {
            var customer = _dbContext.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();
            else
                return View(customer);
        }

        [Authorize(Roles =RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = _dbContext.MembershipTypes.ToList(),
                Customer = new Customer() // otherwise in validationsummery, error for our hidden field ID, now id=0
                
            };

            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer) //viewmodel gaat hier, maar customer ook; zo slim om te zien dat customer wordt gebruikt
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _dbContext.MembershipTypes.ToList()
                };

                return View("CustomerForm",viewmodel);

            }
                

            if(customer.Id==0)
            _dbContext.Customers.Add(customer);

            else
            {
                // werkt hier met id, dus best nog ID meegeven in Form (hidden)
                var customerInDb = _dbContext.Customers.Single(c => c.Id == customer.Id);
                //TryUpdateModel(customerInDb); Safety problems

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                // or use AutoMapper (library) Mapper.map(customer,customerinDB) - nog op te zoeken
                // ipv customerclass kan je bv customerDto (data transafer object maken, afgeslankte versie van customer)


            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _dbContext.MembershipTypes.ToList()
                
            };

            return View("CustomerForm",viewModel);
        }








        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>()
        //    {
        //        new Customer{ Id=1, Name="Guido vant Eiland"},
        //        new Customer{ Id=2, Name="Franky vant Eiland"}
        //    };
        //}

    }
}