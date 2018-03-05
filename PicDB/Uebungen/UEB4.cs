﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB;

namespace Uebungen
{
    public class UEB4 : IUEB4
    {
        string path;

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
            new BusinessLayer(picturePath);
        }

        public IEXIFModel GetEmptyEXIFModel()
        {
            return new EXIFModel();
        }

        public IEXIFViewModel GetEXIFViewModel(IEXIFModel mdl)
        {
            return new EXIFViewModel(mdl);
        }

        public IIPTCModel GetEmptyIPTCModel()
        {
            return new IPTCModel();
        }

        public IIPTCViewModel GetIPTCViewModel(BIF.SWE2.Interfaces.Models.IIPTCModel mdl)
        {
            return new IPTCViewModel(mdl);
        }

        public ICameraModel GetCameraModel(string producer, string make)
        {
            ICameraModel c = new CameraModel();
            c.Producer = producer;
            c.Make = make;
            return c;

        }

        public ICameraViewModel GetCameraViewModel(ICameraModel mdl)
        {
            return new CameraViewModel(mdl);
        }
    }
}
