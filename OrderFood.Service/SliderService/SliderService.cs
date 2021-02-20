using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.SliderService
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }
        public void Create(Slider model)
        {
            _sliderRepository.Create(model);
        }

        public void Delete(Slider model)
        {
            _sliderRepository.Delete(model);
        }

        public List<Slider> GetAll()
        {
            return _sliderRepository.GetAll();
        }

        public Slider GetById(int? id)
        {
            return _sliderRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _sliderRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _sliderRepository.SetDeActive(id);
        }

        public void Update(Slider model)
        {
            _sliderRepository.Update(model);
        }
    }
}
