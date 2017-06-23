using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PAUP_FS.Models
{
    public class Struka
    {
        public int ID { get; set; }

        [DisplayName("Struka Rada")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezan podatak")]
        [StringLength(160, ErrorMessage = "Ovo polje nesmije imati vise od 160 znakova")]
        public string ImeStruke { get; set; }
    }
}