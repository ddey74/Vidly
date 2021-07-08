using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.DTOs;
using AutoMapper;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        
        
        //IHttpActionResult we will return and can have many helper methods 
        //so insted of raising exception we can give message using those helper method




        //GET: /api/customers
        public IHttpActionResult GetCustomer()
        {
            var customerList = _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerList);
        
        }





        //GET: /api/customers/1   to get single customer
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customer==null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }


       



        //POST: /api/Customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {//changing retuen type from CustomerDto to IHttpActionResult
            if(!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                BadRequest();//insted of throwing exception we are using helper method of IHttpActionResult
            }
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri+"/"+customer.Id),customerDto);//customerDto;
        }





        //PUT: /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id,CustomerDto customerDto)
        {
            //id and customer object we are getting from the HTTP request body
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customerInDb==null)
            {
                return NotFound();
            }
            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            #region Updating Db customer object
            //due to automapper the manually updation is not needed
            //customerInDb.Name = customer.Name;
            //customerInDb.BirthDate = customer.BirthDate;
            //customerInDb.IsSuscribedToNewsLetter = customer.IsSuscribedToNewsLetter;
            //customerInDb.MembershipTypeID = customer.MembershipTypeID;

            #endregion

            _context.SaveChanges();

            return Ok();
        }





        //DELETE: /api/customer/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customerInDb==null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }



    }
}
