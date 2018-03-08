using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace PicDB
{
    class DataAccessLayer : IDataAccessLayer
    {
        SqlConnection dbc;

        public DataAccessLayer(string userID, string password, string server, string database)
        {
            string connectionstring = "user id="+ userID + ";password=" + password + ";server=" + server + ";Trusted_Connection=yes;database="+ database + ";connection timeout=30; MultipleActiveResultSets=true";
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
            SqlDataReader dr = c.ExecuteReader();
            dr.Close();
        }

        public void DeletePicture(int ID)
        {
            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "DELETE FROM PICTURES WHERE ID = @id";
            SqlParameter id = new SqlParameter("@id", SqlDbType.Int, 0);
            id.Value = ID;
            c.Parameters.Add(id);
            c.Prepare();
            SqlDataReader dr = c.ExecuteReader();
            dr.Close();
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
                dr.Close();
                return cm;
            }
            else
            {
                dr.Close();
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
            dr.Close();
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
                dr.Close();
                return pm;
            }
            else
            {
                dr.Close();
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
            dr.Close();
            return pml;
        }

        public IPictureModel GetPicture(int ID)
        {
            PictureModel pm = new PictureModel("test.jpg");

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "SELECT ID, FileName, Make, FNumber, ExposureTime, ISOValue, Flash, ExposureProgram, Keywords, ByLine, CopyrightNotice, Headline, Caption, fk_Cameras_ID, fk_Photographers_ID FROM Pictures WHERE ID = @id";
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
                pm.Photographer = GetPhotographer(dr.GetInt32(15));
                dr.Close();
                return pm;
            }
            else
            {
                dr.Close();
                throw new ElementWithIdDoesNotExistException();
            }
        }

        public IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, BIF.SWE2.Interfaces.Models.IIPTCModel iptcParts, IEXIFModel exifParts)
        {
            List<IPictureModel> pml = new List<IPictureModel>();

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "SELECT ID, FileName, Make, FNumber, ExposureTime, ISOValue, Flash, ExposureProgram, Keywords, ByLine, CopyrightNotice, Headline, Caption, fk_Cameras_ID, fk_Photographers_ID FROM Pictures";
            SqlDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                PictureModel pm = new PictureModel("test.jpg");
                pm.ID = dr.GetInt32(0);
                pm.FileName = dr.GetString(1);
                if(namePart != null) if (!pm.FileName.ToLower().Contains(namePart.ToLower())) continue;
                pm.EXIF.Make = dr.GetString(2);
                pm.EXIF.FNumber = dr.GetDecimal(3);
                pm.EXIF.ExposureTime = dr.GetDecimal(4);
                pm.EXIF.ISOValue = dr.GetDecimal(5);
                pm.EXIF.Flash = dr.GetBoolean(6);
                pm.EXIF.ExposureProgram = (ExposurePrograms)dr.GetInt32(7);
                pm.IPTC.Keywords = dr.GetString(8);
                pm.IPTC.ByLine = dr.GetString(9);
                pm.IPTC.CopyrightNotice = dr.GetString(10);
                pm.IPTC.Headline = dr.GetString(11);
                pm.IPTC.Caption = dr.GetString(12);
                try
                {
                    pm.Camera = GetCamera(dr.GetInt32(13));
                }
                catch (SqlNullValueException) { }

                try
                {
                    pm.Photographer = GetPhotographer(dr.GetInt32(14));
                }
                catch (SqlNullValueException) { }
                pml.Add(pm);
            }
            dr.Close();
            return pml;
        }

        public void Save(IPictureModel picture)
        {
            PictureModel p = (PictureModel)picture;

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "INSERT INTO Pictures (FileName, Make, FNumber, ExposureTime, ISOValue, Flash, ExposureProgram, Keywords, ByLine, CopyrightNotice, Headline, Caption, fk_Cameras_ID, fk_Photographers_ID) VALUES (@FileName, @Make, @FNumber, @ExposureTime, @ISOValue, @Flash, @ExposureProgram, @Keywords, @ByLine, @CopyrightNotice, @Headline, @Caption, @fk_Cameras_ID, @fk_Photographers_ID)";

            SqlParameter filename = new SqlParameter("@FileName", SqlDbType.Text, p.FileName.Length);
            filename.Value = p.FileName;
            c.Parameters.Add(filename);

            SqlParameter Make = new SqlParameter("@Make", SqlDbType.Text, string.IsNullOrEmpty(p.EXIF.Make) ? 1 : p.EXIF.Make.Length);
            Make.Value = p.EXIF.Make;
            c.Parameters.Add(Make);

            SqlParameter FNumber = new SqlParameter("@FNumber", SqlDbType.Decimal, 0);
            FNumber.Precision = 18;
            FNumber.Scale = 2;
            FNumber.Value = (decimal) p.EXIF.FNumber;
            c.Parameters.Add(FNumber);

            SqlParameter ExposureTime = new SqlParameter("@ExposureTime", SqlDbType.Decimal, 0);
            ExposureTime.Precision = 18;
            ExposureTime.Scale = 2;
            ExposureTime.Value = (decimal)p.EXIF.ExposureTime;
            c.Parameters.Add(ExposureTime);

            SqlParameter ISOValue = new SqlParameter("@ISOValue", SqlDbType.Decimal, 0);
            ISOValue.Precision = 18;
            ISOValue.Scale = 2;
            ISOValue.Value = (decimal)p.EXIF.ISOValue;
            c.Parameters.Add(ISOValue);

            SqlParameter Flash = new SqlParameter("@Flash", SqlDbType.Bit, 0);
            Flash.Value = p.EXIF.Flash;
            c.Parameters.Add(Flash);

            SqlParameter ExposureProgram = new SqlParameter("@ExposureProgram", SqlDbType.Int, 0);
            ExposureProgram.Value = p.EXIF.ExposureProgram;
            c.Parameters.Add(ExposureProgram);

            SqlParameter Keywords = new SqlParameter("@Keywords", SqlDbType.Text, string.IsNullOrEmpty(p.IPTC.Keywords) ? 1 : p.IPTC.Keywords.Length);
            Keywords.Value = p.IPTC.Keywords;
            c.Parameters.Add(Keywords);

            SqlParameter ByLine = new SqlParameter("@ByLine", SqlDbType.Text, string.IsNullOrEmpty(p.IPTC.ByLine) ? 1 : p.IPTC.ByLine.Length);
            ByLine.Value = p.IPTC.ByLine;
            c.Parameters.Add(ByLine);

            SqlParameter CopyrightNotice = new SqlParameter("@CopyrightNotice", SqlDbType.Text, string.IsNullOrEmpty(p.IPTC.CopyrightNotice) ? 1 : p.IPTC.CopyrightNotice.Length);
            CopyrightNotice.Value = p.IPTC.CopyrightNotice;
            c.Parameters.Add(CopyrightNotice);

            SqlParameter Headline = new SqlParameter("@Headline", SqlDbType.Text, string.IsNullOrEmpty(p.IPTC.Headline) ? 1 : p.IPTC.Headline.Length);
            Headline.Value = p.IPTC.Headline;
            c.Parameters.Add(Headline);

            SqlParameter Caption = new SqlParameter("@Caption", SqlDbType.Text, string.IsNullOrEmpty(p.IPTC.Caption) ? 1 : p.IPTC.Caption.Length);
            Caption.Value = p.IPTC.Caption;
            c.Parameters.Add(Caption);

            SqlParameter fk_Cameras_ID = new SqlParameter("@fk_Cameras_ID", SqlDbType.Int, 0);
            try
            {
                fk_Cameras_ID.Value = p.Camera.ID;
            }
            catch (NullReferenceException)
            {
                fk_Cameras_ID.Value = DBNull.Value;
            }
            c.Parameters.Add(fk_Cameras_ID);

            SqlParameter fk_Photographers_ID = new SqlParameter("@fk_Photographers_ID", SqlDbType.Int, 0);
            try
            {
                fk_Photographers_ID.Value = p.Photographer.ID;
            }
            catch (NullReferenceException)
            {
                fk_Cameras_ID.Value = DBNull.Value;
            }
            c.Parameters.Add(fk_Photographers_ID);

            c.Prepare();
            c.ExecuteReader();
        }

        public void Save(IPhotographerModel photographer)
        {
            throw new NotImplementedException();
        }
    }
}
