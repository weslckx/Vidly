using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            return View(GetCustomers());
        }

        public ActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();
            else
                return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>()
            {
                new Customer{ Id=1, Name="Guido vant Eiland"},
                new Customer{ Id=2, Name="Franky vant Eiland"}
            };
        }

    }
}