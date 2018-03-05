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
        private static IDataAccessLayer _dal = null;
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

        public IDataAccessLayer GetDal(string userID, string password, string server, string database)
        {
            if (_dal != null) return _dal;
            else if(_mock == false)
            {
                _dal = new DataAccessLayer(userID, password, server, database);
                return _dal;
            }
            else
            {
                _dal = new MockDataAccessLayer();
                return _dal;
            }
        }
    }
}
