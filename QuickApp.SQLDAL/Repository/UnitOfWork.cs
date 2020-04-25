
using QuickApp.SQLDAL.Models;
using System;

namespace QuickApp.SQLDAL.Repository
{
    public class UnitOfWork : IDisposable
    {
        readonly QuickappContext _context =new QuickappContext();

        //IEmailRepository _email;
        //IProductRepository _products;
        //IOrdersRepository _orders;



        public UnitOfWork()
        {
           
        }


        private Repository<Email> _emailRepository;
        public Repository<Email> EmailRepository
        {
            get
            {

                if (this._emailRepository == null)
                {
                    this._emailRepository = new Repository<Email>(_context);
                }
                return _emailRepository;
            }
        }

        #region Save/Dispose
        public void Save()
        {
            // context.Configuration.ValidateOnSaveEnabled = false;
            _context.SaveChanges();
        }

        public void Save(bool removeValidation)
        {
            if (removeValidation)
            {
                // context.Configuration.ValidateOnSaveEnabled = false;
                _context.SaveChanges();
                // context.Configuration.ValidateOnSaveEnabled = true;
            }
            else
            {
                _context.SaveChanges();
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion


        //public ICustomerRepository Customers
        //{
        //    get
        //    {
        //        if (_customers == null)
        //            _customers = new CustomerRepository(_context);

        //        return _customers;
        //    }
        //}



        //public IProductRepository Products
        //{
        //    get
        //    {
        //        if (_products == null)
        //            _products = new ProductRepository(_context);

        //        return _products;
        //    }
        //}



        //public IOrdersRepository Orders
        //{
        //    get
        //    {
        //        if (_orders == null)
        //            _orders = new OrdersRepository(_context);

        //        return _orders;
        //    }
        //}




       
    }
}
