using BIF.SWE2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB
{
    /// <summary>
    /// returns either mock or real data access layer based on setting
    /// </summary>
    public class DBConnectionFactory
    {
        private static bool _mock = false;

        /// <summary>
        /// sets if mock data access layer should be returned
        /// </summary>
        public static bool Mock
        {
            set
            {
                _mock = value;
            }
        }

        /// <summary>
        /// returns either mock or real data access layer based on setting
        /// </summary>
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
