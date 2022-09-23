using System;

namespace DevPaceTest.DAL.Repositories
{
    public class RepositoryManager
    {
        private DevPaceTestEntities _db;
        private CustomersRepository _customersRepository;
        public RepositoryManager()
        {
            _db = new DevPaceTestEntities();
        }
        public CustomersRepository CustomersRepository
        {
            get
            {
                if (_customersRepository == null)
                    _customersRepository = new CustomersRepository(_db);
                return _customersRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
