using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novi.Models
{
    [Table("Korisnik")]
    public class Korisnik
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Username")]
        [MaxLength(255)]
        public string Username { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("Slika")]
        public string Picture { get; set; }


        public virtual List<Product> Products { get; set; }

    }
}