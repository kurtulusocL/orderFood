using Newtonsoft.Json;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderFood.Web.Models
{
    public class Sepet
    {
        public static Sepet AktifSepet
        {

            get
            {
                HttpContext ctx = HttpContext.Current; //online kullanıcı tutar
                if (ctx.Session["AktifSepet"] == null)
                    ctx.Session["AktifSepet"] = new Sepet();

                return (Sepet)ctx.Session["AktifSepet"];
            }
        }

        private List<SepetItem> urunler = new List<SepetItem>();

        public List<SepetItem> Urunler
        {
            get { return urunler; }
            set { urunler = value; }
        }

        public void SepeteEkle(SepetItem spt)
        {
            if (HttpContext.Current.Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)HttpContext.Current.Session["AktifSepet"];

                if (s.Urunler.Any(i => i.Urun.Id == spt.Urun.Id && i.Urun.CompanyId == spt.CompanyId && i.Urun.UserId == spt.UserId))
                {
                    s.Urunler.FirstOrDefault(i => i.Urun.Id == spt.Urun.Id && i.Urun.CompanyId == spt.CompanyId && i.Urun.UserId == spt.UserId).Adet++;
                }
                else
                {
                    s.Urunler.Add(spt);
                }
            }
            else
            {
                Sepet s = new Sepet();
                s.Urunler.Add(spt);
                HttpContext.Current.Session["AktifSepet"] = s;
            }
        }

        public decimal ToplamTutar
        {
            get
            {
                return Urunler.Sum(i => i.ToplamFiyat);
            }
        }

        public void EmptySepet()
        {
            if (HttpContext.Current.Session["AktifSepet"] != null)
            {
                HttpContext.Current.Session.Remove("AktifSepet");
            }
        }
    }
    public class SepetItem
    {
        public int? ProductId { get; set; }
        public int? CompanyId { get; set; }
        public int OrderId { get; set; }        
        public string UserId { get; set; }
        public Company Company { get; set; }
        public Product Urun { get; set; }
        public Order Order { get; set; }
        public int Adet { get; set; }
        public double? Indirim { get; set; }
        public decimal ToplamFiyat
        {
            get
            {
                if (Urun.DicsountPrice != null && Urun.DicsountPrice != Convert.ToDecimal(0.00))
                    return (decimal)(Urun.DicsountPrice * Adet * (decimal)(1 - Indirim));
                else
                    return Urun.SellingPrice * Adet * (decimal)(1 - Indirim);
            }
        }

        public SepetItem()
        {
            if (Indirim == 0)
            {
                Indirim = 0;
            }
        }
    }
}