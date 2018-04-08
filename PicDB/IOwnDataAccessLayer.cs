using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces;

namespace PicDB
{
    public interface IOwnDataAccessLayer : IDataAccessLayer
    {
        /// <summary>
        /// Saves all changes to the database.
        /// </summary>
        /// <param name="camera"></param>
        void Save(ICameraModel camera);
        /// <summary>
        /// Deletes a Camera from the database.
        /// </summary>
        /// <param name="ID"></param>
        void DeleteCamera(int ID);
    }
}
