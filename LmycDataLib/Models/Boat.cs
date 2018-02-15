
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmycDataLib.Models
{
    public class Boat
    {
        [key]
        [Required]
        [ScaffoldColumn(false)]
        public string BoatId { get; set; }

        [Required(ErrorMessage = "Please Enter Boat Name")]
        [StringLength(50, ErrorMessage = "BoatName cannot be longer than 50 characters.")]
        [DisplayName("Boat Name")]
        public string BoatName { get; set; }

        [DisplayName("Image Url")]
        [Required(ErrorMessage = "Please Enter Image Url")]
        public string Picture { get; set; }

        [DisplayName("Length In Feet")]
        [Required(ErrorMessage = "Please Enter Length In Feet")]
        public int LengthInFeet { get; set; }

        [Required(ErrorMessage = "Please Enter Make of the boat")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Please Enter Year of the boat being made")]
        [Range(1900, 2017, ErrorMessage = "Year Should Be Between 1900 and 2017")]
        public int Year { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RecordCreationDate { get; set; }

        [ScaffoldColumn(false)]
        //[ForeignKey("User")]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}
