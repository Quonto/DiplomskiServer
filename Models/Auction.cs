using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("Auction")]
    public class Auction
    {
        [Key]
        [Column("id_auction")]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "time_auction")]
        public System.DateTime Time { get; set; }

        [Column("price")]
        public int MinimumPrice { get; set; }

        [ForeignKey("product")]
        public virtual int Product { get; set; }

        [ForeignKey("id_user")]
        public User User { get; set; }

    }
}