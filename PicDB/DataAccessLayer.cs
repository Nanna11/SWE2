using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using System.Data.SqlClient;
using System.Data;

namespace PicDB
{
    class DataAccessLayer : IDataAccessLayer
    {
        SqlConnection dbc;

        public DataAccessLayer(string userID, string password, string server, string database)
        {
            string connectionstring = "user id="+ userID + ";password=" + password + ";server=" + server + ";Trusted_Connection=yes;database="+ database + ";connection timeout=30";
            dbc = new SqlConnection(connectionstring);
            dbc.Open();
        }

        public void DeletePhotographer(int ID)
        {
            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "DELETE FROM PHOTOGRAPHERS WHERE ID = @id";
            SqlParameter id = new SqlParameter("@id", SqlDbType.Int, 0);
            id.Value = ID;
            c.Parameters.Add(id);
            c.Prepare();
            c.ExecuteNonQuery(); ;
        }

        public void DeletePicture(int ID)
        {
            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "DELETE FROM PICTURES WHERE ID = @id";
            SqlParameter id = new SqlParameter("@id", SqlDbType.Int, 0);
            id.Value = ID;
            c.Parameters.Add(id);
            c.Prepare();
            c.ExecuteNonQuery();
        }

        public ICameraModel GetCamera(int ID)
        {
            ICameraModel cm = new CameraModel();

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "SELECT ID, Producer, Make, BoughtOn, Notes, ISOLimitGood, ISOLimitAcceptable FROM Cameras WHERE ID = @id";
            SqlParameter id = new SqlParameter("@id", SqlDbType.Int, 0);
            id.Value = ID;
            c.Parameters.Add(id);
            c.Prepare();
            SqlDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                cm.ID = dr.GetInt32(1);
                cm.Producer = dr.GetString(2);
                cm.Make = dr.GetString(3);
                cm.BoughtOn = dr.GetDateTime(4);
                cm.Notes = dr.GetString(5);
                cm.ISOLimitGood = dr.GetDecimal(6);
                cm.ISOLimitAcceptable = dr.GetDecimal(7);
                return cm;
            }
            else
            {
                throw new ElementWithIdDoesNotExistException();
            }
            
        }

        public IEnumerable<ICameraModel> GetCameras()
        {
            List<ICameraModel> cml = new List<ICameraModel>();

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "SELECT ID, Producer, Make, BoughtOn, Notes, ISOLimitGood, ISOLimitAcceptable FROM Cameras";
            SqlDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                ICameraModel cm = new CameraModel();
                cm.ID = dr.GetInt32(3);
                cm.Producer = dr.GetString(2);
                cm.Make = dr.GetString(3);
                cm.BoughtOn = dr.GetDateTime(4);
                cm.Notes = dr.GetString(5);
                cm.ISOLimitGood = dr.GetDecimal(6);
                cm.ISOLimitAcceptable = dr.GetDecimal(7);
                cml.Add(cm);
            }
            
            return cml;
        }

        public IPhotographerModel GetPhotographer(int ID)
        {
            IPhotographerModel pm = new PhotorapherModel();

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "SELECT ID, FirstName, LastName, Birthdate, Notes FROM Photographers WHERE ID = @id";
            SqlParameter id = new SqlParameter("@id", SqlDbType.Int, 0);
            id.Value = ID;
            c.Parameters.Add(id);
            c.Prepare();
            SqlDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                pm.ID = dr.GetInt32(1);
                pm.FirstName = dr.GetString(2);
                pm.LastName = dr.GetString(3);
                pm.BirthDay = dr.GetDateTime(4);
                pm.Notes = dr.GetString(5);
                return pm;
            }
            else
            {
                throw new ElementWithIdDoesNotExistException();
            }
        }

        public IEnumerable<IPhotographerModel> GetPhotographers()
        {
            List<IPhotographerModel> pml = new List<IPhotographerModel>();

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "SELECT ID, FirstName, LastName, Birthdate, Notes FROM Photographers";
            SqlDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                IPhotographerModel pm = new PhotorapherModel();
                pm.ID = dr.GetInt32(1);
                pm.FirstName = dr.GetString(2);
                pm.LastName = dr.GetString(3);
                pm.BirthDay = dr.GetDateTime(4);
                pm.Notes = dr.GetString(5);
                pml.Add(pm);
            }

            return pml;
        }

        public IPictureModel GetPicture(int ID)
        {
            IPictureModel pm = new PictureModel();

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "SELECT ID, FileName, Make, FNumber, ExposureTime, ISOValue, Flash, ExposureProgram, Keywords, ByLine, CopyrightNotice, Headline, Caption, fk_Cameras_ID FROM Pictures WHERE ID = @id";
            SqlParameter id = new SqlParameter("@id", SqlDbType.Int, 0);
            id.Value = ID;
            c.Parameters.Add(id);
            c.Prepare();
            SqlDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                pm.ID = dr.GetInt32(1);
                pm.FileName = dr.GetString(2);
                pm.EXIF.Make = dr.GetString(3);
                pm.EXIF.FNumber = dr.GetDecimal(4);
                pm.EXIF.ExposureTime = dr.GetDecimal(5);
                pm.EXIF.ISOValue = dr.GetDecimal(6);
                pm.EXIF.Flash = dr.GetBoolean(7);
                pm.EXIF.ExposureProgram = (ExposurePrograms)dr.GetInt32(8);
                pm.IPTC.Keywords = dr.GetString(9);
                pm.IPTC.ByLine = dr.GetString(10);
                pm.IPTC.CopyrightNotice = dr.GetString(11);
                pm.IPTC.Headline = dr.GetString(12);
                pm.IPTC.Caption = dr.GetString(13);
                pm.Camera = GetCamera(dr.GetInt32(14));
                return pm;
            }
            else
            {
                throw new ElementWithIdDoesNotExistException();
            }
        }

        public IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, BIF.SWE2.Interfaces.Models.IIPTCModel iptcParts, IEXIFModel exifParts)
        {
            List<IPictureModel> pml = new List<IPictureModel>();

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "SELECT ID, FileName, Make, FNumber, ExposureTime, ISOValue, Flash, ExposureProgram, Keywords, ByLine, CopyrightNotice, Headline, Caption, fk_Cameras_ID FROM Pictures";
            SqlDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                IPictureModel pm = new PictureModel();
                pm.ID = dr.GetInt32(1);
                pm.FileName = dr.GetString(2);
                if (!pm.FileName.ToLower().Contains(namePart.ToLower())) continue;
                pm.EXIF.Make = dr.GetString(3);
                pm.EXIF.FNumber = dr.GetDecimal(4);
                pm.EXIF.ExposureTime = dr.GetDecimal(5);
                pm.EXIF.ISOValue = dr.GetDecimal(6);
                pm.EXIF.Flash = dr.GetBoolean(7);
                pm.EXIF.ExposureProgram = (ExposurePrograms)dr.GetInt32(8);
                pm.IPTC.Keywords = dr.GetString(9);
                pm.IPTC.ByLine = dr.GetString(10);
                pm.IPTC.CopyrightNotice = dr.GetString(11);
                pm.IPTC.Headline = dr.GetString(12);
                pm.IPTC.Caption = dr.GetString(13);
                pm.Camera = GetCamera(dr.GetInt32(14));
                pml.Add(pm);
            }

            return pml;
        }

        public void Save(IPictureModel picture)
        {
            throw new NotImplementedException();
        }

        public void Save(IPhotographerModel photographer)
        {
            throw new NotImplementedException();
        }
    }
}
