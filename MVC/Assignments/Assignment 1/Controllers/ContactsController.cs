using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Mvc_Assignment_1.Models;
using Mvc_Assignment_1.Repository;

namespace Mvc_Assignment_1.Controllers
{
    public class ContactsController : Controller
    {
        // GET: Contacts
        IContactRepository repository;
        private ContactContext db = new ContactContext();
        public ContactsController()
        {
            repository = new ContactRepository();
        }
        public async Task<ActionResult> Index()
        {
            var contacts = await repository.GetAllAsync();
            return View(contacts);
           
        }
        //create get method
        public ActionResult Create()
        {
            return View();
        }
        //create post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await repository.CreateAsync(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        //delete get method
        public async Task<ActionResult> Delete(long id)
        {
            
            var contact = await repository.GetAllAsync(); 
            var contact1 = (await repository.GetAllAsync()).FirstOrDefault(c => c.Id == id);

            if (contact1 == null)
            {
                return HttpNotFound();
            }

            return View(contact1);
        }
       //delete post method
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteContact(long id)
        {
            await repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> GetAllAsync()
        {
            var contacts = await repository.GetAllAsync();
            return View(contacts);
        }
    }
}