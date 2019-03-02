using CostControl.Entity.Models.Base;
using CostControl.Entity.Models.CostControl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostControl.Entity.Models.Security
{
    public class User : SuperEntity<long>
    {
        [Required]
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6,
            ErrorMessage = "Please enter a unique User Name, it must be greater than {2} characters and less than {1} characters.")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6,
            ErrorMessage = "Please enter a unique Password, it must be greater than {2} characters and less than {1} characters.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 6,
            ErrorMessage = "Please enter a unique Email, it must be greater than {2} characters and less than {1} characters.")]
        public string Email { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        //public virtual ICollection<UserClaim> UserClaims { get; set; }

        //public virtual ICollection<UserToken> UserTokens { get; set; }

        //public virtual ICollection<UserLogin> UserLogins { get; set; }

        //public virtual ICollection<Role> Roles { get; set; }
    }
}