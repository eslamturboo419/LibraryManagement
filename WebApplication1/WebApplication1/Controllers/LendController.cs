using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.Interfaces;

namespace WebApplication1.Controllers
{
    public class LendController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly ICustomerRepository customerRepository;

        public LendController(IBookRepository bookRepository , ICustomerRepository customerRepository)
        {
            this.bookRepository = bookRepository;
            this.customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            // avaliable book
            //var avaliable= bookRepository.FindWithAuthor(x=>x.BorrowerId == null);
            var avaliable= bookRepository.FindWithAuthor(x=>x.Borrower == null);
            if (avaliable == null) { return RedirectToAction("Index", "Home"); }


            return View(avaliable);
        }

        public IActionResult LendBook(int bookId)
        {

            var val = bookRepository.GetById(bookId);
            ViewData["BorrowerId"] = new SelectList(customerRepository.GetAll(), "CustomerId", "Name");

            return View(val);
        }


        [HttpPost]
        public IActionResult LendBook(Book book)
        {
            if (ModelState.IsValid)
            {
                //var book2 = bookRepository.GetById(book.BookId);
                //var customer = customerRepository.GetById((int) book.BorrowerId);
                //book.Borrower = customer;
               // bookRepository.Update(book2);

                bookRepository.Update(book);
                return RedirectToAction("Index");
            }
            ViewData["BorrowerId"] = new SelectList(customerRepository.GetAll(), "CustomerId", "Name",book.BorrowerId);
            return View(book);
        }


    }
}
