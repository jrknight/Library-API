﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("tblItem")]
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PhotoUrl { get; set; }
        [Required]
        public string OwnerEmail { get; set; }
        [Required]
        public string OwnerName { get; set; }
        [Required]
        public string CurrentHolderEmail { get; set; }
        [Required]
        public string HolderName { get; set; }


        
        public ICollection<ItemRequest> ItemRequests { get; set; }
        public ICollection<ItemRecord> ItemRecords { get; set; }
    }
}
