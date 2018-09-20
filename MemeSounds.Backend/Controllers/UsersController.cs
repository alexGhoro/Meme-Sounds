using MemeSounds.Backend.Data;
using MemeSounds.Backend.Helpers;
using MemeSounds.Backend.Models;
using MemeSounds.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSounds.Backend.Controllers
{
  public class UsersController : Controller
  {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    // GET: Users
    public async Task<IActionResult> Index()
    {
      return View(await _context.User.ToListAsync());
    }

    // GET: Users/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _context.User.FirstOrDefaultAsync(m => m.UserId == id);
      if (user == null)
      {
        return NotFound();
      }

      return View(user);
    }

    // GET: Users/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Users/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserView userView)
    {
      if (ModelState.IsValid)
      {
        var user = ToUser(userView);
        _context.Add(user);
        await _context.SaveChangesAsync();

        //Crear el usuario ASP here
        var userHelper = new UsersHelper(_context, _userManager);
        userHelper.CreateUserASP(userView.Email, userView.Password, "User");

        return RedirectToAction(nameof(Index));
      }
      return View(userView);
    }

    private User ToUser(UserView userView)
    {
      return new User
      {
        Email = userView.Email,
        FirstName = userView.FirstName,
        ImagePath = userView.ImagePath,
        LastName = userView.LastName,
        Telephone = userView.Telephone,
        UserId = userView.UserId
      };
    }

    // GET: Users/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _context.User.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }
      return View(user);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("UserId,FirstName,LastName,Email,Telephone,ImagePath")] User user)
    {
      if (id != user.UserId)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(user);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!UserExists(user.UserId))
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
      return View(user);
    }

    // GET: Users/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _context.User.FirstOrDefaultAsync(m => m.UserId == id);
      if (user == null)
      {
        return NotFound();
      }

      return View(user);
    }

    // POST: Users/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var user = await _context.User.FindAsync(id);
      _context.User.Remove(user);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool UserExists(int id)
    {
      return _context.User.Any(e => e.UserId == id);
    }
  }
}
