using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ViewResult Index()
        {
            var customers = GetCustomers();
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);
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
        }
    }
}