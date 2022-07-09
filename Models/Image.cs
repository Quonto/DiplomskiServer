using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("Image")]
    public class Image
    {
        [Key]
        [Column("id_image")]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }


        [Column("data")]
        public string Data { get; set; }


        [JsonIgnore]
        [ForeignKey("id_product")]
        public Product Product { get; set; }

    }

}