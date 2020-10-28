using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoteliNovica.Models
{
    public class LanacHotel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(75)]
        public string Naziv { get; set; }

        [Required]
        [Range(1851,2009)]
        public int GodinaOsnivanja { get; set; }
    }
}