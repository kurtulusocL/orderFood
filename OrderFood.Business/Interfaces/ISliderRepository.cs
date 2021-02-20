using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Interfaces
{
    public interface ISliderRepository:IGenericRepository<Slider>
    {
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
