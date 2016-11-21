using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tinkerMyCloud.Data;
using tinkerMyCloud.Models;

namespace tinkerMyCloud.Controllers
{
    public class FileItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FileItemsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: FileItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.dbFileItem.ToListAsync());
        }

        // GET: FileItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileItem = await _context.dbFileItem.SingleOrDefaultAsync(m => m.Id == id);
            if (fileItem == null)
            {
                return NotFound();
            }

            return View(fileItem);
        }

        // GET: FileItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FileItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Created,FileIsPrivate,Type,URL,UserId")] FileItem fileItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fileItem);
        }

        // GET: FileItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileItem = await _context.dbFileItem.SingleOrDefaultAsync(m => m.Id == id);
            if (fileItem == null)
            {
                return NotFound();
            }
            return View(fileItem);
        }

        // POST: FileItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Created,FileIsPrivate,Type,URL,UserId")] FileItem fileItem)
        {
            if (id != fileItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileItemExists(fileItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(fileItem);
        }

        // GET: FileItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileItem = await _context.dbFileItem.SingleOrDefaultAsync(m => m.Id == id);
            if (fileItem == null)
            {
                return NotFound();
            }

            return View(fileItem);
        }

        // POST: FileItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileItem = await _context.dbFileItem.SingleOrDefaultAsync(m => m.Id == id);
            _context.dbFileItem.Remove(fileItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FileItemExists(int id)
        {
            return _context.dbFileItem.Any(e => e.Id == id);
        }
    }
}
