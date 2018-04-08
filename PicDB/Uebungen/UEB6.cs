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

        public void HelloWorld()
        {
        }

        public IBusinessLayer GetBusinessLayer()
        {
            return new BusinessLayer();
        }

        public void TestSetup(string picturePath)
        {
            Directory.CreateDirectory(picturePath);
            DBConnectionFactory dbf = DBConnectionFactory.Instance;
            dbf.Mock = true;
            try
            {
                GlobalInformation gi = GlobalInformation.InitializeInstance(picturePath);
            }
            catch (Exception) { }
        }

        public IPictureModel GetEmptyPictureModel()
        {
            return new PictureModel("test.jpg");
        }

        public IPhotographerModel GetEmptyPhotographerModel()
        {
            return new PhotographerModel();
        }
    }
}
