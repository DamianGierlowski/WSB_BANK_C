using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankApp.Data;
using BankApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private static readonly Random Random = new Random();
        
        public AccountController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var applicationDbContext = _context.Account.Include(a => a.User).Where(s =>s.UserId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }
        
        public async Task<IActionResult> History(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }
        
            var account = await _context.Account
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);


            var income = await _context.Transactions.Where(t => t.RecipientId == account.Id).Include(t => t.Source).ToListAsync();
            var expense = await _context.Transactions.Where(t => t.SourceId == account.Id).Include(t=>t.Recipient).ToListAsync();

            
            if (account == null)
            {
                return NotFound();
            }

            ViewData["income"] = income;
            ViewData["expense"] = expense;
            
            
            return View(account);
        }

        public async Task<IActionResult> Create()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            
            string number = Random.Next(100000000, 999999999).ToString();
            string iban = "PL0013164057" + number;
            
            var account = new Account
            {
                Number = iban,
                Balance = 1000,
                UserId = user.Id,
            };
            
            _context.Add(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Account == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Account'  is null.");
            }
            var account = await _context.Account.FindAsync(id);
            if (account != null)
            {
                _context.Account.Remove(account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
          return (_context.Account?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
