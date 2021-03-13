using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mica.Core.Repositories;
using Mica.Models;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Mica.Core.Persistence
{
    public class BankRepository : Repository<Bank>, IBankRepository
    {
        public BankRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}