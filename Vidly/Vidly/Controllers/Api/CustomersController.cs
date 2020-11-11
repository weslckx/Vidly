using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Vidly.Dtos;
using Vidly.Models;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute; // checken of goed werkt
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {

        private ApplicationDbContext _dbContext;

        public CustomersController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET/api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _dbContext.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>); //delegate
        }

        public CustomerDto GetCustomer(int id)
        {
            var customer= _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Customer,CustomerDto>(customer);

        }

        // POST /api/customers
       [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // new object
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            //DB generated ID
            customerDto.Id = customer.Id;

            return customerDto;
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);
           // Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb); //excisting object, can add it as a second param

            // not needed anymore
            //customerInDb.Name = customerDto.Name;
            //customerInDb.MembershipTypeId = customerDto.MembershipTypeId;
            //customerInDb.Birthdate = customerDto.Birthdate;
            //customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;

            _dbContext.SaveChanges();

        }

        // DELETE /api/customer/id
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _dbContext.Customers.Remove(customerInDb);
            _dbContext.SaveChanges();
        }
    }
}
