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
    public class ReturnController : Controller
    {


        private readonly IBookRepository bookRepository;
        private readonly ICustomerRepository customerRepository;

        public ReturnController(IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            this.bookRepository = bookRepository;
            this.customerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            // Returned books
             var returnBook = bookRepository.FindWithAuthorAndBorrower(x => x.BorrowerId != null);
            // var returnBook = bookRepository.FindWithAuthorAndBorrower(x => x.Borrower != null);
            if (returnBook == null) { return RedirectToAction("Index", "Home"); }


            return View(returnBook);
        }

         
        public IActionResult ReturnBook(int bookid)
        {
            if (ModelState.IsValid)
            {
                var book = bookRepository.GetById(bookid);
                book.Borrower = null;
                book.BorrowerId = null;
                bookRepository.Update(book);
            }
            return RedirectToAction("Index");
        }

    }
}
