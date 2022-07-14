using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("UserInformation")]
    public class UserInformation
    {
        [Key]
        [Column("id_user_information")]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("surename")]
        [MaxLength(255)]
        public string Surename { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("place")]
        public string Place { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "date")]
        public System.DateTime Date { get; set; }

        [Column("data")]
        public string Data { get; set; }


        [JsonIgnore]
        [ForeignKey("id_user")]
        public User User { get; set; }

    }

}