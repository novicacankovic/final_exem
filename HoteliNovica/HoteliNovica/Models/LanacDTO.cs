using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoteliNovica.Models
{
    public class LanacDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int GodinaOsnivanja { get; set; }
        public decimal Prosek { get; set; }
        public int UkupanBrojSoba { get; set; }
    }
}