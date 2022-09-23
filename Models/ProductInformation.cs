using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("ProductInformation")]
    public class ProductInformation
    {
        [Key]
        [Column("id_product_information")]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("delete")]
        public bool Delete { get; set; }

        [JsonIgnore]
        [ForeignKey("id_group")]
        public Group Groups { get; set; }

    }

}