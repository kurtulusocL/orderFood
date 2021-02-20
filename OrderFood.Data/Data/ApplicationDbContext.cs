using Microsoft.AspNet.Identity.EntityFramework;
using OrderFood.Data.Identity;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {

        }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<CancelRequest> CancelRequests { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Complain> Complains { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyContact> CompanyContacts { get; set; }
        public DbSet<CompanySocialMedia> CompanySocialMedias { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<MessageForUser> MessageForUsers { get; set; }
        public DbSet<MessageFromCompany> MessageFromCompanies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProfilePhoto> ProfilePhotos { get; set; }
        public DbSet<SendMail> SendMails { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<ToMake> ToMakes { get; set; }
        public DbSet<VideoAd> VideoAds { get; set; }
    }
}
