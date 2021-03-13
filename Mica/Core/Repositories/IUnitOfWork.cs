using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mica.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IBankRepository Banks { get; }
        ITransactionRepository Transactions { get; }
        int Complete();
    }
}
