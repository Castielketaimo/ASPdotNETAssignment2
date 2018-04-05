﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ASPdotNETcoreAssignment.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        [Required(ErrorMessage = "First Name Required:")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [Display(Name = "First Name:")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Required:")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [Display(Name = "Last Name:")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Street Address Required")]
        [Display(Name = "Street Address:")]
        [StringLength(100, ErrorMessage = "Less than 100 characters")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City Required")]
        [Display(Name = "City:")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Province Required")]
        [Display(Name = "Province:")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Postal Code Required")]
        [Display(Name = "Postal Code:")]
        //[RegularExpression(@"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$", ErrorMessage = "Please input a valid Canadian postal code")]
        [StringLength(10, ErrorMessage = "Less than 10 characters")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country Required")]
        [Display(Name = "Country :")]
        [StringLength(20, ErrorMessage = "Less than 20 characters")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Mobile Number Required")]
        [Display(Name = "Mobile Number:")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please input a valid phone number")]
        [StringLength(20, ErrorMessage = "Less than 20 characters")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Provide Sailing Experience")]
        [Display(Name = "Sailing Experience:")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string SailingExp { get; set; }
    }
}
