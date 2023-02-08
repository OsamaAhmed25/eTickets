using eTickets.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture"), Required(ErrorMessage = "Profile Picture Is Required")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name"), Required(ErrorMessage = "Full Name Is Required")]
        public string FullName { get; set; }
        [Display(Name = "Biography"), Required(ErrorMessage = "Biography Is Required")]
        public string Bio { get; set; }
        //Relationships
        public List<Movie> Movies{ get; set; }
    }
}
