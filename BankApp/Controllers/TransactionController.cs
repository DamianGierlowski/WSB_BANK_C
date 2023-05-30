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
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TransactionController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.Transactions != null ? 
                          View(await _context.Transactions.Include(a => a.Source).Include(a => a.Recipient).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transactions = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transactions == null)
            {
                return NotFound();
            }

            return View(transactions);
        }

        // GET: Transaction/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewData["SourceId"] = new SelectList(_context.Account.Where(a => a.UserId == user.Id), "Id","Number");
            
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SourceId,RecipientNumber,Value")] TransactionsDTO transactionsDto)
        {
            IQueryable<Account> source = _context.Account.Where(a => a.Id == transactionsDto.SourceId);

            IQueryable<Account> recipient = _context.Account.Where(a => a.Number == transactionsDto.RecipientNumber);
            
            
            if (!recipient.Any())
            {
                ModelState.AddModelError("RecipientNumber","Wrong recipient number");
            }
            
            if (source.First().Balance < transactionsDto.Value)
            {
                ModelState.AddModelError("Value","Insufficient funds in the account");
            }
            
            if (ModelState.IsValid)
            {
                var sourceAccount = source.First();
                var recipientAccount = recipient.First();

                var transactions = new Transactions
                {
                    Id = transactionsDto.Id,
                    Value = transactionsDto.Value,
                    SourceId = transactionsDto.SourceId,
                    RecipientId = recipientAccount.Id,
                };

                
                _context.Add(transactions);
                
                sourceAccount.SubBalance(transactionsDto.Value);
                recipientAccount.AddBalance(transactionsDto.Value);
                _context.Update(sourceAccount);
                _context.Update(recipientAccount);
                
                await _context.SaveChangesAsync();
                return RedirectToAction("index", "Account");
            }
            
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewData["SourceId"] = new SelectList(_context.Account.Where(a => a.UserId == user.Id), "Id","Number");

            return View(transactionsDto);
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transactions = await _context.Transactions.FindAsync(id);
            if (transactions == null)
            {
                return NotFound();
            }
            return View(transactions);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SourceId,RecipientId,Value")] Transactions transactions)
        {
            if (id != transactions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionsExists(transactions.Id))
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
            return View(transactions);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transactions = await _context.Transactions.Include(a => a.Source).Include(a => a.Recipient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transactions == null)
            {
                return NotFound();
            }

            return View(transactions);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }
            var transactions = await _context.Transactions.FindAsync(id);
            if (transactions != null)
            {
                var recipient = _context.Account.First(a => a.Id == transactions.RecipientId);
                var source = _context.Account.First(a => a.Id == transactions.SourceId);


                recipient.SubBalance(transactions.Value);
                source.AddBalance(transactions.Value);
                _context.Update(recipient);
                _context.Update(source);

                _context.Transactions.Remove(transactions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionsExists(int id)
        {
          return (_context.Transactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
