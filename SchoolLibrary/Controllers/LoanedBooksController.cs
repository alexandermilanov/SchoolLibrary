#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary.Models;

namespace SchoolLibrary.Controllers
{
    public class LoanedBooksController : Controller
    {
        private readonly LibraryContext _context;

        public LoanedBooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: LoanedBooks
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.LoanedBooks.Include(l => l.Book);
            return View(await libraryContext.ToListAsync());
        }

        // GET: LoanedBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanedBook = await _context.LoanedBooks
                .Include(l => l.Book)
                .FirstOrDefaultAsync(m => m.LoanedBookId == id);
            if (loanedBook == null)
            {
                return NotFound();
            }

            return View(loanedBook);
        }

        // GET: LoanedBooks/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId");
            return View();
        }

        // POST: LoanedBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanedBookId,BookId,UserId,DateLoaned")] LoanedBook loanedBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loanedBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", loanedBook.BookId);
            return View(loanedBook);
        }

        // GET: LoanedBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanedBook = await _context.LoanedBooks.FindAsync(id);
            if (loanedBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", loanedBook.BookId);
            return View(loanedBook);
        }

        // POST: LoanedBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoanedBookId,BookId,UserId,DateLoaned")] LoanedBook loanedBook)
        {
            if (id != loanedBook.LoanedBookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loanedBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanedBookExists(loanedBook.LoanedBookId))
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
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", loanedBook.BookId);
            return View(loanedBook);
        }

        // GET: LoanedBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanedBook = await _context.LoanedBooks
                .Include(l => l.Book)
                .FirstOrDefaultAsync(m => m.LoanedBookId == id);
            if (loanedBook == null)
            {
                return NotFound();
            }

            return View(loanedBook);
        }

        // POST: LoanedBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loanedBook = await _context.LoanedBooks.FindAsync(id);
            _context.LoanedBooks.Remove(loanedBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanedBookExists(int id)
        {
            return _context.LoanedBooks.Any(e => e.LoanedBookId == id);
        }
    }
}
