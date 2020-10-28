using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoteliNovica.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Naziv   { get; set; }

        [Required]
        [Range(1950,2020)]
        public int GodinaOtvaranja { get; set; }

        [Required]
        [Range(2,int.MaxValue)]
        public int BrojZaposlenih { get; set; }

        [Range(10,999)]
        public int BrojSoba { get; set; }


        public int LanacHotelId { get; set; }
        public virtual LanacHotel LanacHotel { get; set; }

        

    }
}