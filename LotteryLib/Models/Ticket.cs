using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;

namespace LotteryLib
{

    [Serializable]
    public class Ticket
    {
        [Required(ErrorMessage = "This field has not been filled out")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "This field has not been filled out")]
        public String SurName { get; set; }

        [Required(ErrorMessage = "This field has not been filled out")]
        [Phone]
        public String PhoneNumber { get; set; }

        [Required(ErrorMessage = "This field has not been filled out")]
        [EmailAddress]
        public String Email { get; set; }

        [Required(ErrorMessage = "This field has not been filled out")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "This field has not been filled out")]
        public String SerialNumber { get; set; }

    }
}
