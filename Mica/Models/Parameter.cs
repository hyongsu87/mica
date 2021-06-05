using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mica.Models
{
    public abstract class Parameter : Model
    {
        [Required]
        [Index(IsUnique = true)]
        [StringLength(500)]
        public string Name { get; set; }
    }
}