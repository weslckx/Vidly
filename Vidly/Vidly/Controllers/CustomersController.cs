﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

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
            // add System.Data.Entity
            //add include to eager load. We need membershiptype, won't be loaded by default!
            return View(_dbContext.Customers.Include(c=> c.MembershipType).ToList());
        }

        public ActionResult Details(int id)
        {
            var customer = _dbContext.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();
            else
                return View(customer);
        }

        public ActionResult New()
        {
            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = _dbContext.MembershipTypes.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Customer customer) //viewmodel gaat hier, maar customer ook; zo slim om te zien dat customer wordt gebruikt
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Customers");
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