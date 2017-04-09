
using Api.DomainObjects;
using Api.Services;
using Api.Web.Extensions;
using log4net;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Web.Controllers
{
    /// <summary>
    /// Contacts API Controller
    /// </summary>
    public class ContactsController : BaseController
    {
        private readonly IContacts contactsService = default(IContacts);
        private readonly ILog loggerService = default(ILog);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contactsInstance">Instance of type <see cref="IContacts"/></param>
        /// <param name="loggerInstance">Instance of type <see cref="ILog"/></param>
        public ContactsController(IContacts contactsInstance, ILog loggerInstance)
        {
            this.contactsService = contactsInstance;
            this.loggerService = loggerInstance;
        }
        /// <summary>
        /// Gets All Contacts Available
        /// </summary>
        /// <returns>Collection of <see cref="Contact"/></returns>
        [HttpGet]
        [ActionName("contacts")]
        public IEnumerable<Contact> Contacts()
        {
            IEnumerable<Contact> results = this.contactsService.GetAll();
            return results;
        }

        /// <summary>
        /// Gets Customer for a given Customer Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int Id)
        {
            Contact result = this.contactsService.Get(Id);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new Customer
        /// </summary>
        /// <param name="model"><see cref="Contact"/></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Create(Contact model)
        {
            IHttpActionResult response;
            if (ModelState.IsValid)
            {
                bool status = this.contactsService.Create(model);
                if (status)
                {
                    return new CustomMessageResult("Created Contact.",HttpStatusCode.Created);
                }
                response = new CustomMessageResult("Sorry, Contact can not be added. Please try later !");
                return response;

            }

            HttpResponseMessage responseMsg = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            response = ResponseMessage(responseMsg);
            return response;  // implemnted Custom Messdage with additional deatils bac.


        }

        /// <summary>
        /// Updates a Contact
        /// </summary>
        /// <param name="model"><see cref="Contact"/></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Update(Contact model)
        {
            IHttpActionResult response;
            if (ModelState.IsValid)
            {
                bool status = this.contactsService.Update(model);
                if (status)
                {
                    return Ok();
                }
                response = new CustomMessageResult("Sorry, Contact can not be Updated. Please try later !");
                return response;

            }

            HttpResponseMessage responseMsg = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            response = ResponseMessage(responseMsg);
            return response;  // implemnted Custom Messdage with additional deatils bac.

        }

        /// <summary>
        /// Deletes the specified Contact
        /// </summary>
        /// <param name="Id">Contact Id</param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(long Id)
        {
            IHttpActionResult response;
            bool status = this.contactsService.Delete(Id);
            if (status)
            {
                return Ok();
            }
            response = new CustomMessageResult("Sorry, Contact can not be Updated. Please try later !");
            return response;
        }
    }
}
