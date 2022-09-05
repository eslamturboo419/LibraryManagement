using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Interfaces;
using WebApplication1.Models.VM;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly ICustomerRepository customerRepository;

        public HomeController(ILogger<HomeController> logger ,
                                IBookRepository bookRepository,  IAuthorRepository authorRepository 
                                    ,ICustomerRepository customerRepository )
        {
            _logger = logger;
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.customerRepository = customerRepository;
        }


        public IActionResult Index()
        {
            var item = new HomeVM()
            {
                AuthorCount = authorRepository.Count(X => true),
                BookCount = bookRepository.Count(x => true),
                CustomerCount = customerRepository.Count(x => true),
                LendBookCount = bookRepository.Count(x => x.Borrower != null)
            };


            return View(item);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
