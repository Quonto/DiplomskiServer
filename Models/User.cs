using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Novi.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [Column("id_user")]
        public int ID { get; set; }

        [Column("is_admin")]
        public bool IsAdmin { get; set; }

        [Column("username")]
        [MaxLength(255)]
        public string Username { get; set; }


        [Column("email")]
        public string Email { get; set; }


        [Column("password")]
        public string Password { get; set; }


        [Column("picture")]
        public string Picture { get; set; }

        [Column("user_information")]
        public virtual UserInformation UserInformation { get; set; }


    }
}