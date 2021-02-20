using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.SliderService
{
    public interface ISliderService
    {
        void Create(Slider model);
        void Update(Slider model);
        List<Slider> GetAll();
        Slider GetById(int? id);
        void Delete(Slider model);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
