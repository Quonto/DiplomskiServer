using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novi.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Naziv")]
        [MaxLength(255)]

        public string Naziv { get; set; }

        [Column("Cena")]
        public int Price { get; set; }

        [Column("Ocena")]
        public int Mark { get; set; }

        [Column("Category")]
        public string Category { get; set; }

        [Column("Slika")]
        public virtual List<Image> Picture { get; set; }

        [Column("Opis")]
        public string Description { get; set; }



    }
}