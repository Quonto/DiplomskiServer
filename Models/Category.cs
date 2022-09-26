using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [Column("id_category")]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("delete")]
        public bool Delete { get; set; }

        [Column("picture_category")]
        public Image Picture { get; set; }

        [Column("group")]
        public virtual List<Group> Groups { get; set; }

    }
}