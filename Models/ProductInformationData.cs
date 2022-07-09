using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("ProductInformationData")]
    public class ProductInformationData
    {
        [Key]
        [Column("id_product_information_data")]
        public int Id { get; set; }

        [Column("id_product_information_save")]
        public int IdInfo { get; set; }

        [Column("data")]
        public string Data { get; set; }


        [ForeignKey("id_product_information")]
        public ProductInformation ProductInformation { get; set; }

        [JsonIgnore]
        [ForeignKey("id_product")]
        public Product Product { get; set; }

    }
}