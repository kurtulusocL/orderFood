using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class SendMail : BaseHome
    {
        [Required]
        [EmailAddress]
        public string Sender { get; set; }
        [Required]
        [EmailAddress]
        public string Reciever { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Text { get; set; }
        public SendMail()
        {
            IsConfirmed = true;
        }
    }
}
