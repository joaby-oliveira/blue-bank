using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private static List<Account> accounts = new List<Account>
        {
            new Account { Id = 1, CustomerId = 1, AccountNumber = "ABC123", Balance = 1000 },
            new Account { Id = 2, CustomerId = 2, AccountNumber = "XYZ789", Balance = 2000 }
        };

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountById(int id)
        {
            var account = accounts.Find(a => a.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public IActionResult CreateAccount(Account account)
        {
            // Assign a unique id to the new account
            account.Id = accounts.Count + 1;
            accounts.Add(account);
            return CreatedAtAction(nameof(GetAccountById), new { id = account.Id }, account);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, Account updatedAccount)
        {
            var existingAccount = accounts.Find(a => a.Id == id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            existingAccount.CustomerId = updatedAccount.CustomerId;
            existingAccount.AccountNumber = updatedAccount.AccountNumber;
            existingAccount.Balance = updatedAccount.Balance;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            var account = accounts.Find(a => a.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            accounts.Remove(account);
            return NoContent();
        }
    }
}
