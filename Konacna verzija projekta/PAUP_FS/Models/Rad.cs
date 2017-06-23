using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PAUP_FS.Models
{
    public class Rad
    {
        public int ID { get; set; }

        [DisplayName("Ime Rada")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezan podatak")]
        [StringLength(160, ErrorMessage = "Ovo polje nesmije imati vise od 160 znakova")]
        public string Ime { get; set; }

        [DisplayName("File Rada")]
        [StringLength(160, ErrorMessage = "Ovo polje nesmije imati vise od 160 znakova")]
        public string Path { get; set; }

        [DisplayName("Ime Korisnika")]
        [StringLength(160, ErrorMessage = "Ovo polje nesmije imati vise od 160 znakova")]
        public string Korisnik { get; set; }

        [DisplayName("Zahtjev")]
        public bool Zahtjev { get; set; }

        [DisplayName("Kategorija")]
        [ForeignKey("KategorijaIme")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezan podatak")]
        public int KategorijaID { get; set; }
        public Kategorija KategorijaIme { get; set; }

        [DisplayName("Struka")]
        [ForeignKey("StrukaIme")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezan podatak")]
        public int StrukaID { get; set; }
        public Struka StrukaIme { get; set; }
    }
}