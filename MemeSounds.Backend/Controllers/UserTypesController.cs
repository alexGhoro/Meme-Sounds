using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemeSounds.Backend.Data;
using MemeSounds.Domain;
using Microsoft.AspNetCore.Authorization;

namespace MemeSounds.Backend.Controllers
{
  [Authorize(Roles = "SystemAdmin")]
  public class UserTypesController : Controller
  {
    private readonly ApplicationDbContext _context;

    public UserTypesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: UserTypes
    public async Task<IActionResult> Index()
    {
        return View(await _context.UserType.ToListAsync());
    }

    // GET: UserTypes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userType = await _context.UserType
            .FirstOrDefaultAsync(m => m.UserTypeId == id);
        if (userType == null)
        {
            return NotFound();
        }

        return View(userType);
    }

    // GET: UserTypes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: UserTypes/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("UserTypeId,Name")] UserType userType)
    {
        if (ModelState.IsValid)
        {
            _context.Add(userType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(userType);
    }

    // GET: UserTypes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userType = await _context.UserType.FindAsync(id);
        if (userType == null)
        {
            return NotFound();
        }
        return View(userType);
    }

    // POST: UserTypes/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("UserTypeId,Name")] UserType userType)
    {
        if (id != userType.UserTypeId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(userType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeExists(userType.UserTypeId))
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
        return View(userType);
    }

    // GET: UserTypes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userType = await _context.UserType
            .FirstOrDefaultAsync(m => m.UserTypeId == id);
        if (userType == null)
        {
            return NotFound();
        }

        return View(userType);
    }

    // POST: UserTypes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userType = await _context.UserType.FindAsync(id);
        _context.UserType.Remove(userType);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UserTypeExists(int id)
    {
        return _context.UserType.Any(e => e.UserTypeId == id);
    }
  }
}
