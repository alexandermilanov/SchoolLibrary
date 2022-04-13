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
    public class ConditionsController : Controller
    {
        private readonly LibraryContext _context;

        public ConditionsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Conditions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Conditions.ToListAsync());
        }

        // GET: Conditions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condition = await _context.Conditions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (condition == null)
            {
                return NotFound();
            }

            return View(condition);
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

        // GET: Conditions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookCondition")] Condition condition)
        {
            bool itemExists = false;

            foreach (var record in _context.Conditions)
            {
                if (FormatString(record.BookCondition).ToUpper() == FormatString(condition.BookCondition).ToUpper())
                {
                    itemExists = true;
                    break;
                }
            }

            if (itemExists == false)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(condition);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Conditions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condition = await _context.Conditions.FindAsync(id);
            if (condition == null)
            {
                return NotFound();
            }
            return View(condition);
        }

        // POST: Conditions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookCondition")] Condition condition)
        {
            if (id != condition.Id)
            {
                return NotFound();
            }

            bool itemExists = false;

            foreach (var record in _context.Conditions)
            {
                if (FormatString(condition.BookCondition).ToUpper() == FormatString(condition.BookCondition).ToUpper())
                {
                    itemExists = true;
                    break;
                }
            }

            if (itemExists == false)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(condition);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ConditionExists(condition.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Conditions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condition = await _context.Conditions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (condition == null)
            {
                return NotFound();
            }

            return View(condition);
        }

        // POST: Conditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var condition = await _context.Conditions.FindAsync(id);
            _context.Conditions.Remove(condition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConditionExists(int id)
        {
            return _context.Conditions.Any(e => e.Id == id);
        }
    }
}
