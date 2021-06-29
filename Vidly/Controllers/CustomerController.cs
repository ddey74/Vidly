using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }
        //ApplicationDbContext is disposable object 
        //so we need to properly dispose this object by overriding dispose method of base controller class

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customer
        public ViewResult Index()
        {
            //var customers = GetCustomers();
            //var customer = _context.Customers.ToList();//but we want to load membershiptype data as well
            //Eager loading
            var customer = _context.Customers.Include(c => c.MembershipType).ToList();//Include() method is in sytem.data.entity
            return View(customer);
        }

        public ActionResult Details(int id)
        {
            //var customer = GetCustomers().SingleOrDefault(c => c.Id == id);//old code
            // var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            //we want to load membershipdata as well
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //This method will return all the Customers
        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer> {
                new Customer() {Name="John Smith" ,Id=1},
                new Customer() {Name="Marry Jane" ,Id=2 },
                new Customer() {Name="Marvel " ,Id=3 },
                new Customer() {Name="Johnson" ,Id=4 }
            };
        }//we want to get rid of this method and want to fetch data from the databse
    }
}