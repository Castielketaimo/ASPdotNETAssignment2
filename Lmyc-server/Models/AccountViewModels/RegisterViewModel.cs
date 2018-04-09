using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc_server.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

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

        [Display(Name = "Postal Code :")]
        [RegularExpression(@"[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ]\s?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]", ErrorMessage = "Invalid Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country Required")]
        [Display(Name = "Country :")]
        [StringLength(20, ErrorMessage = "Less than 20 characters")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Mobile Number: ")]
        [RegularExpression(@"\D*([2-9]\d{2})(\D*)([2-9]\d{2})(\D*)(\d{4})\D*", ErrorMessage = "Invalid Mobile Number")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Provide Sailing Experience")]
        [Display(Name = "Sailing Experience:")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string SailingExperience { get; set; }
    }
}
