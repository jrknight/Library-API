using Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("tblItemRequest")]
    public class ItemRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public Item Item { get; set; }
        
        [Required]
        public string UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public User Owner { get; set; }

        public DateTime RequestDate { get; set; }

    }
}
