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
    public class AuthorsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IAuthorRepository authorRepository;

        public AuthorsController(MyDbContext context , IAuthorRepository authorRepository)
        {
            _context = context;
            this.authorRepository = authorRepository;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var val = authorRepository.GetAll();
            

            return View(val);
        }

        

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(  Author author)
        {
            if (ModelState.IsValid)
            {
                authorRepository.Create(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var author = authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( Author author)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    authorRepository.Update(author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.AuthorId))
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
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {


            var author = authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            authorRepository.Delete(author);
            return RedirectToAction(nameof(Index));
        }

        

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorId == id);
        }
    }
}
