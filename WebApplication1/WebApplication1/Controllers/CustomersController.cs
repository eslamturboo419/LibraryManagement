using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.Interfaces;
using WebApplication1.Models.VM;

namespace WebApplication1.Controllers
{
    public class CustomersController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IBookRepository bookRepository;
        private readonly ICustomerRepository customerRepository;

        public CustomersController(MyDbContext context , IBookRepository bookRepository , ICustomerRepository customerRepository)
        {
            _context = context;
            this.bookRepository = bookRepository;
            this.customerRepository = customerRepository;
        }

        [Route("Customer")] // if you type "localhost/Customer " it go to this method directly
        public async Task<IActionResult> Index()
        {
            var customerVM = new List<CustomerVM>();
            var customers = customerRepository.GetAll();

            foreach (var item in customers)
            {
                customerVM.Add(new CustomerVM()
                {
                     Customer = item, BookCount = bookRepository.Count(x=>x.BorrowerId==item.CustomerId)
                });
            }

            //var val = customerRepository.GetAll();


            return View(customerVM);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerRepository.Create(customer);

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
         
            var customer =  customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( Customer customer)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    customerRepository.Update(customer);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

     
        public async Task<IActionResult> Delete(int id)
        {
            var customer = customerRepository.GetById(id);
                 
            if (customer == null)
            {
                return NotFound();
            }
            customerRepository.Delete(customer);

            return RedirectToAction(nameof(Index));
        }
         
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
