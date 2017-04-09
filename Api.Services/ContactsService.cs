using Api.DomainObjects;
using System.Collections.Generic;

namespace Api.Services
{
    public class ContactsService : ServiceBase, IContacts
    {
        IEnumerable<Contact> IContacts.GetAll()
        {
            IEnumerable<Contact> results = new List<Contact>();
            var dbContext = GetContactsDbContext();
            results = dbContext.FindAll();
            return results;
        }


        Contact IContacts.Get(long Id)
        {
            var dbContext = GetContactsDbContext();
            Contact contact = dbContext.FindById(Id);
            return contact;
        }

        bool IContacts.Create(Contact newContact)
        {
            bool status = false;
            var dbContext = GetContactsDbContext();

            newContact.Id = dbContext.LongCount();
            dbContext.Insert(newContact);
            dbContext.EnsureIndex(x => x.Email);
            status = true;
            return status;
        }

        bool IContacts.Update(Contact contact)
        {
            bool status = false;
            var dbContext = GetContactsDbContext();

            //Contact contactObj = dbContext.FindById(contact.Id);
            status = dbContext.Update(contact);
            dbContext.EnsureIndex(x => x.Email);
            return status;
        }

        bool IContacts.Delete(long Id)
        {
            bool status = false;
            var dbContext = GetContactsDbContext();
            status = dbContext.Delete(Id);
            return status;
        }
    }
}
