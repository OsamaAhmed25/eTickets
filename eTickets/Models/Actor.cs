using eTickets.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture"),Required(ErrorMessage ="Profile Picture Is Required")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name"),Required(ErrorMessage = "Full Name Is Required"),StringLength(50,MinimumLength =3,ErrorMessage ="Full Name muste be between 3 & 500 Chars")]
        public string FullName { get; set; }

        [Display(Name = "Biography"), Required(ErrorMessage = "Biography Is Required")]
        public string Bio { get; set; }
        //Relationships
        public List<Actor_Movie> Actors_Movies{ get; set; }
    }
}
