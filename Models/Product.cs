using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Column("id_product")]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("price")]
        public int Price { get; set; }

        [Column("number_of_wish")]
        public int NumberOfWish { get; set; }

        [Column("number_of_like")]
        public int NumberOfLike { get; set; }


        [Column("number_of_viewers")]
        public List<NumberOfViewe> NumberOfViewers { get; set; }

        [Column("details")]
        public string Details { get; set; }

        [Column("picture")]
        public virtual List<Image> Picture { get; set; }

        [Column("review")]
        public virtual List<Review> Reviews { get; set; }

        [Column("data")]
        public List<ProductInformationData> Data { get; set; }

        [JsonIgnore]
        [ForeignKey("id_group")]
        public Group Group { get; set; }


        [ForeignKey("id_user")]
        public User User { get; set; }







    }
}