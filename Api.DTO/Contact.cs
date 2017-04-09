using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.DomainObjects
{
    /// <summary>
    /// Contact Information Domain Object.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Gets or Sets Customer Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or Sets First Name
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets Email Address
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage="Email Address you entered is not valid.")]
        public string Email { get; set; }
        public IEnumerable<Phone> PhoneNumber { get; set; }
        public Status CurrentStatus { get; set; }
    }
}
