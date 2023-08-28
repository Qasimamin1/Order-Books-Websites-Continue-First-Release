using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
    public  class Company
    {
        public int Id { get; set; }


        [Required]
        public string Name { set; get; }

        public String? StreetAdress { set; get; }
        public string? City { set; get; }
        public string? State { set; get; }
        public string? PostalCode { set; get; }

        public string? PhoneNumber { set; get; }    



    }
}
