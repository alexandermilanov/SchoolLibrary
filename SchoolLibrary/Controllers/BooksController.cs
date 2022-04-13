#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary.Models;
//using System.Web.UI;

namespace SchoolLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Books.Include(b => b.Author).Include(b => b.Condition).Include(b => b.Genre).Include(b => b.Publisher);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Condition)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        private string FormatString(string s)
        {
            s = s.Replace('\t', ' ');
            s = s.Trim();

            while (s.Contains("  "))
            {
                s = s.Replace("  ", " ");
            }
            return s;
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name");
            ViewData["ConditionId"] = new SelectList(_context.Conditions, "Id", "BookCondition");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,AuthorId,PublisherId,Year,GenreId,ConditionId")] Book book)
        {
            bool itemExists = false;

            foreach (var record in _context.Books)
            {
                if (record.AuthorId == book.AuthorId &&
                    FormatString(record.Title.ToUpper()) == FormatString(book.Title.ToUpper()) &&
                    record.PublisherId == book.PublisherId &&
                    record.Year == book.Year &&
                    record.GenreId == book.GenreId &&
                    record.ConditionId == book.ConditionId)
                {
                    itemExists = true;
                    break;
                }
            }

            if (itemExists == false)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                return RedirectToAction(nameof(Index));
            }


            //if (ModelState.IsValid)
            //{
            //    _context.Add(book);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            ////ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", book.AuthorId);
            ////ViewData["ConditionId"] = new SelectList(_context.Conditions, "Id", "BookCondition", book.ConditionId);
            ////ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", book.GenreId);
            ////ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", book.PublisherId);

            //ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", book.AuthorId);
            //ViewData["ConditionId"] = new SelectList(_context.Conditions, "Id", "Id", book.ConditionId);
            //ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", book.GenreId);
            //ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Id", book.PublisherId);
            //return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", book.AuthorId);
            ViewData["ConditionId"] = new SelectList(_context.Conditions, "Id", "BookCondition", book.ConditionId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", book.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", book.PublisherId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,AuthorId,PublisherId,Year,GenreId,ConditionId")] Book book)
        {
            if (!_context.Books.Any(
                e => e.Title == book.Title &&
                e.AuthorId == book.AuthorId &&
                e.PublisherId == book.PublisherId &&
                e.Year == book.Year &&
                e.GenreId == book.GenreId &&
                e.ConditionId == book.ConditionId))
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Condition)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
