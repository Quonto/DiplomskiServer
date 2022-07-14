using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("NumberOfWish")]
    public class NumberOfWish
    {
        [Key]
        [Column("id_wish")]
        public int Id { get; set; }

        [Column("id_user")]
        public int IdUser { get; set; }

        [JsonIgnore]
        [ForeignKey("id_product")]
        public Product Product { get; set; }

    }

}