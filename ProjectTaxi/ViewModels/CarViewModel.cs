using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ProjectTaxi.ViewModels
{
    public class CarViewModel
    {
        [Required]
        [Display(Name = "Name of car: ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Year: ")]
        public int Year { get; set; }

    }
}
