using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mica.Core.Repositories;
using Mica.Models;

namespace Mica.Core.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Accounts = new AccountRepository(_context);
            Banks = new BankRepository(_context);
            Transactions = new TransactionRepository(_context);
        }

        public IAccountRepository Accounts { get; private set; }
        public IBankRepository Banks { get; private set; }
        public ITransactionRepository Transactions { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}