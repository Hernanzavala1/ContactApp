using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.ContactManager;
using ContactApp.viewModel;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        private readonly ContactContext _context;

        public UsersController(ContactContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            var address = await _context.Addresses
                .FirstOrDefaultAsync(m => m.User == id);
            if (user == null || address == null)
            {
                return NotFound();
            }
            ContactVM contact = new ContactVM();
            contact.firstName = user.firstName;
            contact.lastName = user.lastName;
            contact.streetName = address.Street;
            contact.state = address.State;
            contact.city = address.city;
            contact.zipCode = address.postalCode;
            ViewBag.Id = id;
            return View(contact);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ContactVM createConctact = new ContactVM();
            
            return View(createConctact);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactVM Contact)
        {
            if (ModelState.IsValid)
            {
                User user = new User(); //create an instance of a User from data in view
                user.firstName = Contact.firstName;
                user.lastName = Contact.lastName;
                _context.Add(user);
                _context.SaveChanges();
                Address address = new Address(); // create instance of Address model from view data
                address.Street = Contact.streetName;
                address.city = Contact.city;
                address.State = Contact.state;
                address.postalCode = Contact.zipCode;
                address.User = user.Id; // use the user's id as a foreign key to the address table
                _context.Addresses.Add(address);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(Contact);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            var address = await _context.Addresses.FirstOrDefaultAsync(m => m.User == id);
            if (user == null || address == null)
            {
                return NotFound();
            }
            ContactVM contact = new ContactVM
            {
                id = user.Id,
                firstName = user.firstName,
                lastName = user.lastName,
                streetName = address.Street,
                state = address.State,
                city = address.city,
                zipCode = address.postalCode
            };
            
            return View(contact);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContactVM contact)
        {
            if (id != contact.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Users.FindAsync(id);
                    var address = await _context.Addresses.FirstOrDefaultAsync(m => m.User == id);
                    user.firstName = contact.firstName;
                    user.lastName = contact.lastName;
                    address.Street = contact.streetName;
                    address.city = contact.city;
                    address.State = contact.state;
                    address.postalCode = contact.zipCode;
                    _context.Users.Update(user);
                    _context.Addresses.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(contact.id))
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
            return View(contact);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
