using BIF.SWE2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB
{
    public class DBConnectionFactory
    {
        private static bool _mock = false;

        public static bool Mock
        {
            set
            {
                _mock = value;
            }
        }

        public static IOwnDataAccessLayer CreateDal(string userID, string password, string server, string database)
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
