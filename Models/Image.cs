using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("Slika")]
    public class Image
    {
        [Key]
        [Column("ID_I")]
        public int ID { get; set; }

        [Column("Name")]
        [MaxLength(255)]
        public string Name { get; set; }


        [Column("Data")]
        public string Data { get; set; }


        [JsonIgnore]
        [ForeignKey("ID")]
        public Product Product { get; set; }
    }

}