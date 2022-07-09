using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("Review")]
    public class Review
    {
        [Key]
        [Column("id_review")]
        public int Id { get; set; }

        [Column("mark")]
        public int Mark { get; set; }


        [Column("coment")]
        public string Coment { get; set; }


        [JsonIgnore]
        [ForeignKey("id_product")]
        public Product Product { get; set; }

    }

}