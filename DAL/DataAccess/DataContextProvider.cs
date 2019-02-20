using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class DataContextProvider
    {
        private TestingContext _dataContext;

        public TestingContext Get()
        {
            return _dataContext ?? (_dataContext = new TestingContext());
        }

        private bool _isDisposed;

        ~DataContextProvider()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }

            _isDisposed = true;
        }

        protected void DisposeCore()
        {
            _dataContext?.Dispose();
        }
    }
}
