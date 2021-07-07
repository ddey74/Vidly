using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //GET: /api/customers
        public IEnumerable<Customer> GetCustomer()
        {
            return _context.Customers.ToList();
        
        }

        //GET: /api/customers/1   to get single customer
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customer==null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return customer;
        }

        //POST: /api/Customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        //PUT: /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id,Customer customer)
        {
            //id and customer object we are getting from the HTTP request body
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customerInDb==null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            #region Updating Db customer object

            customerInDb.Name = customer.Name;
            customerInDb.BirthDate = customer.BirthDate;
            customerInDb.IsSuscribedToNewsLetter = customer.IsSuscribedToNewsLetter;
            customerInDb.MembershipTypeID = customer.MembershipTypeID;

            #endregion

            _context.SaveChanges();
        }

        //DELETE: /api/customer/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customerInDb==null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}
