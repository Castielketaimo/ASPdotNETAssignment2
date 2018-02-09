using LMYCWebsite.Models;
using System;
using System.Collections.Generic;
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
        public string BoatId { get; set; }
        [StringLength(50, ErrorMessage = "BoatName cannot be longer than 50 characters.")]
        public string BoatName { get; set; }
        public string Picture { get; set; }
        public int LengthInFeet { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RecordCreationDate { get; set; }

        [ForeignKey("UserId")]
        [Display(Name = "Created By")]
        [Column("CreatedBy")]
        public virtual ApplicationUser User { get; set; }


    }
}
