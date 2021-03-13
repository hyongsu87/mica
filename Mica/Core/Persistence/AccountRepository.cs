using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mica.Models;
using Mica.Core.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Mica.Core.Persistence
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(IdentityDbContext context)
            : base(context)
        {
        }
    }
}