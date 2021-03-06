﻿using OrderFood.Business.Interfaces;
using OrderFood.Data.Data;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Repositories
{
    public class CompanySocialMediaRepository : GenericRepository<CompanySocialMedia>, ICompanySocialMediaRepository
    {
        ApplicationDbContext _db;
        public CompanySocialMediaRepository()
        {
            _db = new ApplicationDbContext();
        }
        public List<CompanySocialMedia> GetAllIncluding()
        {
            return _db.Set<CompanySocialMedia>().Include("Company").Include("Company.Country").Include("Company.City").ToList();
        }

        public void SetActive(int id)
        {
            var active = _db.Set<CompanySocialMedia>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<CompanySocialMedia>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
