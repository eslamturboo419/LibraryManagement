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
    public class BooksController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;

        public BooksController(MyDbContext context , IBookRepository bookRepository , IAuthorRepository authorRepository)
        {
            _context = context;
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        // GET: Books
        public async Task<IActionResult> Index(int ? authorId , int  ? BorrowId)
        {
            if(authorId != null)
            {
                var books = authorRepository.GetWithBooks((int)authorId);
                return View(books);
            }
            if(BorrowId != null)
            {
                var books = bookRepository.FindWithAuthorAndBorrower(x=>x.BorrowerId==BorrowId);
                return View(books);
            }
            else
            {
                var books = bookRepository.GetAllWithAuthor();
                return View(books);

            }
           // var myDbContext = _context.Books.Include(b => b.Author).Include(b => b.Borrower);
            //return View(await myDbContext.ToListAsync());
        }
 
        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(authorRepository.GetAll(), "AuthorId", "Name");
            //ViewData["BorrowerId"] = new SelectList(_context.Customers, "CustomerId", "Name");
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Book book)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(book);
                //await _context.SaveChangesAsync();
                 
                bookRepository.Create(book);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(authorRepository.GetAll(), "AuthorId", "Name", book.AuthorId);
            //ViewData["BorrowerId"] = new SelectList(_context.Customers, "CustomerId", "Name", book.BorrowerId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookRepository.GetById((int)id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(authorRepository.GetAll(), "AuthorId", "Name", book.AuthorId);
            //ViewData["BorrowerId"] = new SelectList(_context.Customers, "CustomerId", "Name", book.BorrowerId);
            return View(book);
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
          
            if (ModelState.IsValid)
            {
                try
                {
                    bookRepository.Update(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            ViewData["AuthorId"] = new SelectList(authorRepository.GetAll(), "AuthorId", "Name", book.AuthorId);
            //ViewData["BorrowerId"] = new SelectList(_context.Customers, "CustomerId", "Name", book.BorrowerId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookRepository.GetById((int) id);
            if (book == null)
            {
                return NotFound();
            }
            bookRepository.Delete(book);
            return RedirectToAction(nameof(Index));
        }
 

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
