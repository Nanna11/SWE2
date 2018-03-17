using BIF.SWE2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB
{
    class DBConnectionFactory
    {
        private static readonly DBConnectionFactory _dbf;
        private static bool _mock = false;

        protected DBConnectionFactory()
        {

        }

        static DBConnectionFactory()
        {
            _dbf = new DBConnectionFactory();
        }

        public static DBConnectionFactory Instance
        {
            get
            {
                return _dbf;
            }
        }

        public bool Mock
        {
            set
            {
                _mock = value;
            }
        }

        public IDataAccessLayer CreateDal(string userID, string password, string server, string database)
        {
            if(_mock == false)
            {
                 return new DataAccessLayer(userID, password, server, database);
            }
            else
            {
                return new MockDataAccessLayer();
            }
        }
    }
}
