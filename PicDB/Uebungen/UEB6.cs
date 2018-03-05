using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using PicDB;

namespace Uebungen
{
    public class UEB6 : IUEB6
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

        public IPictureModel GetEmptyPictureModel()
        {
            return new PictureModel();
        }

        public IPhotographerModel GetEmptyPhotographerModel()
        {
            return new PhotorapherModel();
        }
    }
}
