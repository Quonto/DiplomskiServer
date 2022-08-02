using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("PlaceProductUser")]
    public class PlaceProductUser
    {
        [Key]
        [Column("id_places_product_user")]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [JsonIgnore]
        [ForeignKey("id_user_information")]
        public UserInformation UserInformation { get; set; }

        [JsonIgnore]
        [ForeignKey("id_product")]
        public Product Product { get; set; }

    }
}