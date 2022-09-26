using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("Group")]
    public class Group
    {
        [Key]
        [Column("id_group")]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }



        [Column("picture_group")]
        public Image Picture { get; set; }

        [Column("products")]
        public virtual List<Product> Products { get; set; }



        [Column("information")]
        public virtual List<ProductInformation> ProductInformation { get; set; }

        [JsonIgnore]
        [ForeignKey("id_category")]
        public Category Category { get; set; }

        [Column("delete")]
        public virtual bool Delete { get; set; }

    }
}