using eTickets.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cinema Logo"), Required(ErrorMessage = "Logo Is Required")]
        public string Logo { get; set; }
        [Display(Name = "Cinema Name"), Required(ErrorMessage = "Cinema Name Is Required")]
        public string Name { get; set; }
        [Display(Name = "Description"), Required(ErrorMessage = "Description Is Required")]
        public string Description { get; set; }

        public List<Movie> Movies{ get; set; }
    }
}
