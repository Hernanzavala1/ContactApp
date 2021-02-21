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
        //Returns just a list of all of the contacts in the database. 
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        //This returns a view model that contains all of the necessary information about a specific contact.
        //The way of finding that specific contact is via id 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Views/Users/notFound.cshtml");
            }
            // retrieve the record of the contact by using their id, after retrieve their address info.
            //The contact id is also a foreign key to the Address table.
            User user = null;
            Address address = null;
            try
            {
                 user = await _context.Users
               .FirstOrDefaultAsync(m => m.Id == id);
                 address = await _context.Addresses
                    .FirstOrDefaultAsync(m => m.User == id);
            }
            catch(Exception E)
            {
                return View("Views/Shared/ErrorPage.cshtml");
            }
           
            if (user == null || address == null)
            {
                return View("Views/Users/notFound.cshtml");
            }
            //Fill up the view model with only the information that will get displayed to the user of the application.
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
        //Pass in the class that will receive the input values of a new contact and verify them before creating a new contact.
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
        //This is the method which takes all of the user input from the view and saves it as a contact in our database.
        public IActionResult Create(ContactVM Contact)
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
        //This method finds the existing contact in the databse and fills up the view model before rendering the view to the user.
        public async Task<IActionResult> Edit(int? id)
        {
            //if the id passed is null then we must return the error page since we wont be able to identified a specific contact
            if (id == null)
            {
                return View("Views/Shared/ErrorPage.cshtml");
            }

            var user = await _context.Users.FindAsync(id); // we look for the contact User information based on id.
            var address = await _context.Addresses.FirstOrDefaultAsync(m => m.User == id); // we find the user's address model to retrieve info.
            if (user == null || address == null)
            {
                return View("Views/Users/notFound.cshtml");
            }
            ContactVM contact = new ContactVM //We pass the contact's information into  the view model that will be used to display all of the information nicely.
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
        //This is the method where an existing contact can be search via their id and edited as the user wishes to. All changes are saved to the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContactVM contact)
        {
            if (id != contact.id)
            {
                return View("Views/Users/notFound.cshtml");
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
                    //The following 2 lines updates the corresponding records in the databases with the updated info.
                    _context.Users.Update(user);
                    _context.Addresses.Update(address);
                    await _context.SaveChangesAsync(); //saves it to the database.
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(contact.id))
                    {
                        return View("Views/Users/notFound.cshtml");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); //redirect back to the home page.
            }
            return View(contact);
        }

        // GET: Users/Delete/5
        //This method finds the specific contact, fills up the view model with the info to be rendered, then it returns the corresponding view.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Views/Users/notFound.cshtml");
            }
            var user = await _context.Users.FindAsync(id);
            var address = await _context.Addresses.FirstOrDefaultAsync(m => m.User == id);
            if (user == null || address == null)
            {
                return View("Views/Users/notFound.cshtml");
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

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //This method is fired right after the user confirms they intend to delete the contact.
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            var address = await _context.Addresses.FirstOrDefaultAsync(m => m.User == id);
            _context.Users.Remove(user); // removes from the User table     
            _context.Addresses.Remove(address); // removes the record from the Address table
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //This metod returns a view where the user can enter information to help them find a specific contact
        public IActionResult createSearch()
        {
            return View("Views/Users/search.cshtml");
        }
        //This method recieves all of the information a user enters into the search request and processes a result and displays an appropiate view.
        [HttpPost, ActionName("searchResult")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  searchResult(string searchBy, string FirstName, string LastName, string city)
        {
            List<User> contactNames = new List<User>();
            if (searchBy == "Name")
            {
                contactNames = _context.Users.Where(x => (x.firstName.StartsWith(FirstName) && x.lastName.StartsWith(LastName)) || (FirstName == null && LastName == null)).ToList();
                if(contactNames.Count == 0)
                {
                    return View("Views/Users/notFound.cshtml");
                }
                return View("Views/Users/searchResult.cshtml",contactNames);
            }
            else
            {
                List<Address> contactsAddresses = _context.Addresses.Where(x => x.city == city || city == null).ToList();
               
                foreach (Address contactAddress in contactsAddresses)
                {
                    contactNames.Add(_context.Users.Find(contactAddress.User));
                }
                if (contactNames.Count == 0)
                {
                    return View("Views/Users/notFound.cshtml");
                }

                return View("Views/Users/searchResult.cshtml", contactNames);
            }

        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
      
    }
}
