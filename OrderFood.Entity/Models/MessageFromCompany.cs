using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class MessageFromCompany : BaseHome
    {
        [Required]
        public string NameSurname { get; set; }
        [Required]
        [EmailAddress]
        public string Reciever { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Text { get; set; }       
        public MessageFromCompany()
        {
            IsConfirmed = true;
        }
    }
}
