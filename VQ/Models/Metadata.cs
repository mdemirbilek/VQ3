using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VQ
{

    public class CustomerMetadata
    {
        [Required]
        [MinLength(2)]
        [StringLength(50)]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [MinLength(2)]
        [StringLength(50)]
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email Address cannot be null!")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
    }


}