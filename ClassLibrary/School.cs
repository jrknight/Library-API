using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities
{
    [Table("tblSchool")]
    public class School
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string SchoolName { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Admin { get; set; }



    }
}
