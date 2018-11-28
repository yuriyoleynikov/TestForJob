using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestForJob.Models.ViewModels
{
    public class TaskViewModel
    {
        [Display(Name = "The Task Id")]
        public int Id { get; set; }

        [Display(Name = "The Task Name")]
        public string Name { get; set; }

        [Display(Name = "The Task Description")]
        public string Description { get; set; }
    }
}
