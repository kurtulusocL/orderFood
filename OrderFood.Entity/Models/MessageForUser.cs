using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class MessageForUser : BaseHome
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Text { get; set; }
        public int? Hit { get; set; }

        public string UserId { get; set; }
        public MessageForUser()
        {
            Hit = 0;
            IsConfirmed = true;
        }
    }
}
