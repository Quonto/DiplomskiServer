using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
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

        [Column("buy")]
        public bool Buy { get; set; }

        [Column("id_user_buy")]
        public int BuyUser { get; set; }

        [Column("add_to_cart")]
        public bool AddToCart { get; set; }

        [Column("is_auction")]
        public bool Auction { get; set; }

        [Column("number_of_wish")]
        public List<NumberOfWish> NumberOfWish { get; set; }

        [Column("number_of_like")]
        public List<NumberOfLike> NumberOfLike { get; set; }


        [Column("number_of_viewers")]
        public List<NumberOfViewe> NumberOfViewers { get; set; }

        [Column("details")]
        public string Details { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("place_product")]
        public PlaceProductUser Place { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "date")]
        public System.DateTime Date { get; set; }

        [Column("picture")]
        public virtual List<Image> Picture { get; set; }

        [Column("review")]
        public virtual List<Review> Reviews { get; set; }

        [Column("data")]
        public List<ProductInformationData> Data { get; set; }

        [ForeignKey("id_group")]
        public int Group { get; set; }

        [ForeignKey("id_user")]
        public User User { get; set; }

    }
}