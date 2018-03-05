using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB;

namespace Uebungen
{
    public class UEB3 : IUEB3
    {
        string path;

        public void HelloWorld()
        {
        }

        public IBusinessLayer GetBusinessLayer()
        {
            return new MockBusinessLayer(path);
        }

        public void TestSetup(string picturePath)
        {
            path = picturePath;
            Directory.CreateDirectory(picturePath);
            DBConnectionFactory dbf = DBConnectionFactory.Instance;
            dbf.Mock = true;
        }

        public IDataAccessLayer GetDataAccessLayer()
        {
            return DBConnectionFactory.Instance.GetDal("PicDB", "PicDB", "localhost", "PicDB");
        }

        public ISearchViewModel GetSearchViewModel()
        {
            return new SearchViewModel();
        }
    }
}
