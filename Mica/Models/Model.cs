using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Migration commands
//Update-database -TargetMigration:0
//Update-database
//add-migration InitialModel -force

namespace Mica.Models
{
    public abstract class Model
    {
        public int Id { get; set; }
    }
}