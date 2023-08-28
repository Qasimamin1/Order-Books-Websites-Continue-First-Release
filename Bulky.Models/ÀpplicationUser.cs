using Bulky.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyWeb
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string ?Name { set; get; }

        public String? StreetAdress { set; get; }
        public string? City { set; get; }
        public string? State { set; get; }
        public string? PostalCode { set; get; }

        public int? CompanyId { set; get; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }



    }
}
