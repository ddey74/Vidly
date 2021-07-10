using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModel;

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
            //var customer = _context.Customers.Include(c => c.MembershipType).ToList();//Include() method is in sytem.data.entity

            //Now we are loading the table from API call from AJAX section of view so no need to pass customer to the view
            return View();
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

        //Added to create a new form and add Customer
        public ActionResult New()
        {
            //adding to get the membership type from database and will show that on view
            //added DbSet<membershipType> in the identity model or we do not have the access
            var membershipTypes = _context.MembershipTypes.ToList();
            //we can pass this membershiptype but later we may need to pass customer details to the view
            //so we need to have a custom viewmodel

            var viewModel = new NewCustomerViewModel()
            {
                Customer=new Customer(),//we need to add this or in validation summary we can see Customer ID not mentioned, by doing this customer will be initilized to default value
                //we can see in hidden field there is no value
                MembershipType = membershipTypes
            };
            //return View(viewModel);//Passing this view model to view so View of New controller will change
            return View("CustomerForm", viewModel);//as after edit controller we are redicting 
                                                    //to New() view which does not feel Promising so renamed New.schtml to CustomerForm.schtml
        }



        //Form New() view after filling all details will search fro the Crete Actionmethod
        //we are getting NewCustomerViewModel in the input and will be Http post call
        //but we want to add customer for now, MVC framework is smart enough to bind the data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            #region To Add new Customer old code
            //to add customer we first need to add that to DbContext
            //_context.Customers.Add(customer);//not written to DB yet it is in memory still
            //_context.SaveChanges();
            #endregion
            #region Cheack if model not valid then return same view
            if (!ModelState.IsValid)//Means the model which is passed is not valid, and no update or add operation is performed
            {
                var viewmodel = new NewCustomerViewModel
                {
                    Customer = customer,
                    MembershipType = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm",viewmodel);
            }
            #endregion

            #region To add or Update customer
            if (customer.Id==0)//means new customer
            {
                _context.Customers.Add(customer);
            }
            else
            {
                //need to update customer in database
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeID = customer.MembershipTypeID;
                customerInDb.IsSuscribedToNewsLetter = customer.IsSuscribedToNewsLetter;
            }
            _context.SaveChanges();
            #endregion
            return RedirectToAction("Index", "Customer");
        }

        //From index view when we click on particular customer we will edit that customer so we will fill the details to New() action view
        //Edit action will fill the details from db and return to New action view
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            //View of New() accepts viewmodel so created viewmodel object
            var viewmodel = new NewCustomerViewModel()
            {
                Customer = customer,
                MembershipType = _context.MembershipTypes.ToList()//as we have to return all membership type again

            };
            if (customer == null)
            {
                return HttpNotFound();
            }
            //return View("New",viewmodel);
            //Now after editiing we are redirecting to New() action view which does not feel so promissing
            //so rename New.schtml to CustomerForm.scthml
            //Make changes in the New() Action also
            return View("CustomerForm", viewmodel);
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