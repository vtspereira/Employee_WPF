using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funcionários.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, StringLength(70, MinimumLength = 3)]
        public string Name { get; set; }

        [Required, StringLength(70, MinimumLength = 3), EmailAddress]
        public string Email { get; set; }

        [Required]
        public decimal Wage { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string ContractType { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
