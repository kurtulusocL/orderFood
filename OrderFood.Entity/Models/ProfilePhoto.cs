using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class ProfilePhoto : BaseHome
    {
        public string Photo { get; set; }
        public string UserId { get; set; }
        public ProfilePhoto()
        {
            IsConfirmed = true;
        }
    }
}
