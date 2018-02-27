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
        }

        public IDataAccessLayer GetDataAccessLayer()
        {
            return new MockDataAccessLayer();
        }

        public ISearchViewModel GetSearchViewModel()
        {
            return new SearchViewModel();
        }
    }
}
