using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc_Assignment_1.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Mvc_Assignment_1.Repository
{
    public class ContactRepository : IContactRepository
    {
        ContactContext db;
        DbSet dbset;

        public ContactRepository()
        {
            db = new ContactContext();
        }

        public async Task CreateAsync(Contact contacts)
        {
            db.contact.Add(contacts);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(long Id)
        {
            var contacts = await db.contact.FindAsync(Id);
            if (contacts != null)
            {
                db.contact.Remove(contacts);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await db.contact.ToListAsync();
        }
    }
}