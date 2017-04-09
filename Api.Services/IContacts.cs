using Api.DomainObjects;
using System.Collections.Generic;

namespace Api.Services
{
    public interface IContacts
    {
        IEnumerable<Contact> GetAll();
        Contact Get(long Id);
        bool Create(Contact newContact);
        bool Update(Contact contact);
        bool Delete(long Id);
    }
}
