using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("Place")]
    public class Place
    {
        [Key]
        [Column("id_place")]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("delete")]
        public bool Delete { get; set; }
    }
}
