using Charon.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Data.DataAccess
{
    public abstract class BaseAccessor : IDisposable
    {
        #region Constructor

        public BaseAccessor(string user)
        {
            _user = user;
            _context = new CharonContext();
            _context.Configuration.LazyLoadingEnabled = false;
            _context.Configuration.ProxyCreationEnabled = false;
            //true is the default, but I've added an explicit assignment anyway
            _context.Configuration.AutoDetectChangesEnabled = true;
        }

        #endregion

        #region Private Members

        private string _user;
        private CharonContext _context;
        private DbContextTransaction _transaction = null;

        #endregion

        #region Internal Properties

        internal DbContextTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        /// <summary>
        /// Provides access to the DbContext
        /// </summary>
        internal CharonContext DataContext
        {
            get { return _context; }
            set { _context = value; }
        }

        /// <summary>
        /// Provides access to the DbContext
        /// </summary>
        internal string User
        {
            get { return _user; }
            set { _user = value; }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Create a new Tx.  If there is an active Tx, it is disposed of (not committed).
        /// </summary>
        public void BeginTransaction()
        {
            if (_transaction != null && _transaction.UnderlyingTransaction != null && _transaction.UnderlyingTransaction.Connection != null)
            {
                _transaction.Dispose();
            }
            _transaction = DataContext.Database.BeginTransaction();
        }

        /// <summary>
        /// Commit current Tx
        /// </summary>
        public void CommitTransaction()
        {
            if (_transaction != null && _transaction.UnderlyingTransaction != null && _transaction.UnderlyingTransaction.Connection != null)
            {
                _transaction.Commit();
            }
        }

        /// <summary>
        /// Rolls back current Tx
        /// </summary>
        public void RollbackTransaction()
        {
            if (_transaction != null && _transaction.UnderlyingTransaction != null && _transaction.UnderlyingTransaction.Connection != null)
            {
                _transaction.Rollback();
            }
        }

        #endregion

        #region Implements IDispose

        /// <summary>
        /// Ensure the Data Context is cleaned up
        /// </summary>
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        #endregion        
    }
}
