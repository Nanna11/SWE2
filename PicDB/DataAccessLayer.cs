using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace PicDB
{
    public class DataAccessLayer : IOwnDataAccessLayer
    {
        SqlConnection dbc;
        Dictionary<int, PhotographerModel> _Photographers = new Dictionary<int, PhotographerModel>();
        Dictionary<int,CameraModel> _Cameras = new Dictionary<int, CameraModel>();
        Dictionary<int, PictureModel> _Pictures = new Dictionary<int, PictureModel>();

        public DataAccessLayer(string userID, string password, string server, string database)
        {
            string connectionstring = "user id="+ userID + ";password=" + password + ";server=" + server + ";Trusted_Connection=yes;database="+ database + ";connection timeout=5; MultipleActiveResultSets=true";
            dbc = new SqlConnection(connectionstring);
            dbc.Open();
            if (dbc.State != ConnectionState.Open) throw new DBConnectionNotAvailableException();
        }

        public void DeletePhotographer(int ID)
        {

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "UPDATE PICTURES SET fk_Photographers_ID = NULL WHERE fk_Photographers_ID = @id; DELETE FROM PHOTOGRAPHERS WHERE ID = @id";
            SqlParameter id = new SqlParameter("@id", SqlDbType.Int, 0);
            id.Value = ID;
            c.Parameters.Add(id);
            c.Prepare();
            SqlDataReader dr = c.ExecuteReader();
            dr.Close();
            _Photographers.Remove(ID);
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
            _Pictures.Remove(ID);
        }

        public ICameraModel GetCamera(int ID)
        {
            if (_Cameras.ContainsKey(ID))
            {
                return _Cameras[ID];
            }
            else
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
                    cm.ID = dr.GetInt32(0);
                    try
                    {
                        cm.Producer = dr.GetString(1);
                    }
                    catch (SqlNullValueException) { }
                    try
                    {
                        cm.Make = dr.GetString(2);
                    }
                    catch (SqlNullValueException) { }
                    try
                    {
                        cm.BoughtOn = dr.GetDateTime(3);
                    }
                    catch (SqlNullValueException) { }
                    try
                    {
                        cm.Notes = dr.GetString(4);
                    }
                    catch (SqlNullValueException) { }
                    try
                    {
                        cm.ISOLimitGood = dr.GetDecimal(5);
                    }
                    catch (SqlNullValueException) { }
                    try
                    {
                        cm.ISOLimitAcceptable = dr.GetDecimal(6);
                    }
                    catch (SqlNullValueException) { }
                    dr.Close();
                    _Cameras.Add(cm.ID, (CameraModel)cm);
                    return cm;
                }
                else
                {
                    dr.Close();
                    throw new ElementWithIdDoesNotExistException();
                }
            }
            
        }

        public IEnumerable<ICameraModel> GetCameras()
        {
            Dictionary<int, CameraModel> newCameras = new Dictionary<int, CameraModel>();
            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "SELECT ID, Producer, Make, BoughtOn, Notes, ISOLimitGood, ISOLimitAcceptable FROM Cameras";
            SqlDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                int ID = dr.GetInt32(0);
                if (_Cameras.ContainsKey(ID)) newCameras.Add(ID, _Cameras[ID]);
                else newCameras.Add(ID, new CameraModel());
                ICameraModel cm = newCameras[ID];
                cm.ID = ID;
                try
                {
                    cm.Producer = dr.GetString(1);
                }
                catch (SqlNullValueException) { }
                try
                {
                    cm.Make = dr.GetString(2);
                }
                catch (SqlNullValueException) { }
                try
                {
                    cm.BoughtOn = dr.GetDateTime(3);
                }
                catch (SqlNullValueException) { }
                try
                {
                    cm.Notes = dr.GetString(4);
                }
                catch (SqlNullValueException) { }
                try
                {
                    cm.ISOLimitGood = dr.GetDecimal(5);
                }
                catch (SqlNullValueException) { }
                try
                {
                    cm.ISOLimitAcceptable = dr.GetDecimal(6);
                }
                catch (SqlNullValueException) { }
            }
            dr.Close();
            _Cameras = newCameras;
            return _Cameras.Values.ToList();
        }

        public IPhotographerModel GetPhotographer(int ID)
        {
            if (_Photographers.ContainsKey(ID))
            {
                return _Photographers[ID];
            }
            else
            {
                IPhotographerModel pm = new PhotographerModel();

                SqlCommand c = new SqlCommand(null, dbc);
                c.CommandText = "SELECT ID, FirstName, LastName, Birthdate, Notes FROM Photographers WHERE ID = @id";
                SqlParameter id = new SqlParameter("@id", SqlDbType.Int, 0);
                id.Value = ID;
                c.Parameters.Add(id);
                c.Prepare();
                SqlDataReader dr = c.ExecuteReader();
                if (dr.Read())
                {
                    pm.ID = dr.GetInt32(0);
                    try
                    {
                        pm.FirstName = dr.GetString(1);
                    }
                    catch (SqlNullValueException) { }
                    pm.LastName = dr.GetString(2);
                    try
                    {
                        pm.BirthDay = dr.GetDateTime(3);
                    }
                    catch (SqlNullValueException) { }
                    try
                    {
                        pm.Notes = dr.GetString(4);
                    }
                    catch (SqlNullValueException) { }
                    dr.Close();
                    _Photographers.Add(pm.ID, (PhotographerModel)pm);
                    return pm;
                }
                else
                {
                    dr.Close();
                    throw new ElementWithIdDoesNotExistException();
                }
            }
        }

        public IEnumerable<IPhotographerModel> GetPhotographers()
        {
            Dictionary<int, PhotographerModel> newPhotographers = new Dictionary<int, PhotographerModel>();

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "SELECT ID, FirstName, LastName, Birthdate, Notes FROM Photographers";
            SqlDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                int ID = dr.GetInt32(0);
                if (_Photographers.ContainsKey(ID)) newPhotographers.Add(ID, _Photographers[ID]);
                else newPhotographers.Add(ID, new PhotographerModel());
                IPhotographerModel pm = newPhotographers[ID];
                pm.ID = ID;
                try{
                    pm.FirstName = dr.GetString(1);
                }
                catch (SqlNullValueException) { }
                pm.LastName = dr.GetString(2);
                try
                {
                    pm.BirthDay = dr.GetDateTime(3);
                }
                catch (SqlNullValueException) { }
                try
                {
                    pm.Notes = dr.GetString(4);
                }
                catch (SqlNullValueException) { }
            }
            dr.Close();
            _Photographers = newPhotographers;
            return _Photographers.Values.ToList();
        }

        public IPictureModel GetPicture(int ID)
        {
            if (_Pictures.ContainsKey(ID))
            {
                return _Pictures[ID];
            }
            else
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
                    _Pictures.Add(pm.ID, pm);
                    return pm;
                }
                else
                {
                    dr.Close();
                    throw new ElementWithIdDoesNotExistException();
                }
            }
        }

        public IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, BIF.SWE2.Interfaces.Models.IIPTCModel iptcParts, IEXIFModel exifParts)
        {
            if (String.IsNullOrEmpty(namePart)) return UnfilteredPictures();
            else return FilteredPictures(namePart);
        }

        IEnumerable<IPictureModel> UnfilteredPictures()
        {
            Dictionary<int, PictureModel> newPictures = new Dictionary<int, PictureModel>();

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "SELECT ID, FileName, Make, FNumber, ExposureTime, ISOValue, Flash, ExposureProgram, Keywords, ByLine, CopyrightNotice, Headline, Caption, fk_Cameras_ID, fk_Photographers_ID FROM Pictures";
            SqlDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                int ID = dr.GetInt32(0);
                string Filename = dr.GetString(1);
                if (_Pictures.ContainsKey(ID)) newPictures.Add(ID, _Pictures[ID]);
                else newPictures.Add(ID, new PictureModel(Filename));
                PictureModel pm = newPictures[ID];
                pm.ID = ID;
                pm.FileName = Filename;
                pm.EXIF.Make = dr.GetString(2);
                pm.EXIF.FNumber = dr.GetDecimal(3);
                pm.EXIF.ExposureTime = dr.GetDecimal(4);
                pm.EXIF.ISOValue = dr.GetDecimal(5);
                pm.EXIF.Flash = dr.GetBoolean(6);
                pm.EXIF.ExposureProgram = (ExposurePrograms) dr.GetInt32(7);
                pm.IPTC.Keywords = dr.GetString(8);
                pm.IPTC.ByLine = dr.GetString(9);
                pm.IPTC.CopyrightNotice = dr.GetString(10);
                pm.IPTC.Headline = dr.GetString(11);
                pm.IPTC.Caption = dr.GetString(12);
                try
                {
                    pm.Camera = GetCamera(dr.GetInt32(13));
                }
                catch (SqlNullValueException)
                {
                    pm.Camera = null;
                }

                try
                {
                    pm.Photographer = GetPhotographer(dr.GetInt32(14));
                }
                catch (SqlNullValueException)
                {
                    pm.Photographer = null;
                }
            }
            dr.Close();
            _Pictures = newPictures;
            return _Pictures.Values.ToList();
        }

        IEnumerable<IPictureModel> FilteredPictures(string namePart)
        {
            Dictionary<int, PictureModel> filteredPictures = new Dictionary<int, PictureModel>();

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "SELECT ID, FileName, Make, FNumber, ExposureTime, ISOValue, Flash, ExposureProgram, Keywords, ByLine, CopyrightNotice, Headline, Caption, fk_Cameras_ID, fk_Photographers_ID FROM Pictures WHERE CHARINDEX(LOWER(@namePart), LOWER(cast(FileName as varchar(max)))) > 0";
            SqlParameter filename = new SqlParameter("@namePart", SqlDbType.VarChar, namePart.Length);
            filename.Value = namePart;
            c.Parameters.Add(filename);
            SqlDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                int ID = dr.GetInt32(0);
                string Filename = dr.GetString(1);
                if (_Pictures.ContainsKey(ID)) filteredPictures.Add(ID, _Pictures[ID]);
                else filteredPictures.Add(ID, new PictureModel(Filename));
                PictureModel pm = filteredPictures[ID];
                pm.ID = ID;
                pm.FileName = Filename;
                if (namePart != null) if (!pm.FileName.ToLower().Contains(namePart.ToLower())) continue;
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
                catch (SqlNullValueException)
                {
                    pm.Camera = null;
                }

                try
                {
                    pm.Photographer = GetPhotographer(dr.GetInt32(14));
                }
                catch (SqlNullValueException)
                {
                    pm.Photographer = null;
                }
            }
            dr.Close();
            return filteredPictures.Values.ToList();
        }

        public void Save(IPictureModel picture)
        {
            if (_Pictures.ContainsKey(picture.ID)) UpdatePicture(picture);
            else InsertPicture(picture);            
        }

        private void UpdatePicture(IPictureModel picture)
        {
            _Pictures[picture.ID] = (PictureModel)picture;
            PictureModel p = (PictureModel)picture;

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "UPDATE Pictures  SET FileName = @FileName, Make = @Make, FNumber = @FNumber, ExposureTime = @ExposureTime, ISOValue = @ISOValue, Flash = @Flash, ExposureProgram = @ExposureProgram, Keywords = @Keywords, ByLine = @ByLine, CopyrightNotice = @CopyrightNotice, Headline = @Headline, Caption = @Caption, fk_Cameras_ID = @fk_Cameras_ID, fk_Photographers_ID = @fk_Photographers_ID WHERE ID = @ID";

            SqlParameter filename = new SqlParameter("@FileName", SqlDbType.Text, p.FileName.Length);
            filename.Value = p.FileName;
            c.Parameters.Add(filename);

            SqlParameter Make = new SqlParameter("@Make", SqlDbType.Text, string.IsNullOrEmpty(p.EXIF.Make) ? 1 : p.EXIF.Make.Length);
            Make.Value = p.EXIF.Make;
            c.Parameters.Add(Make);

            SqlParameter FNumber = new SqlParameter("@FNumber", SqlDbType.Decimal, 0);
            FNumber.Precision = 18;
            FNumber.Scale = 2;
            FNumber.Value = (decimal)p.EXIF.FNumber;
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

            SqlParameter ID = new SqlParameter("@ID", SqlDbType.Int, 0);
            ID.Value = p.ID;
            c.Parameters.Add(ID);

            SqlParameter fk_Photographers_ID = new SqlParameter("@fk_Photographers_ID", SqlDbType.Int, 0);
            try
            {
                fk_Photographers_ID.Value = p.Photographer.ID;
            }
            catch (NullReferenceException)
            {
                fk_Photographers_ID.Value = DBNull.Value;
            }
            c.Parameters.Add(fk_Photographers_ID);

            c.Prepare();
            try
            {
                c.ExecuteReader();
            }
            catch (SqlException e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        private void InsertPicture(IPictureModel picture)
        {
            PictureModel p = (PictureModel)picture;

            SqlCommand c = new SqlCommand(null, dbc);
            c.CommandText = "INSERT INTO Pictures (FileName, Make, FNumber, ExposureTime, ISOValue, Flash, ExposureProgram, Keywords, ByLine, CopyrightNotice, Headline, Caption, fk_Cameras_ID, fk_Photographers_ID) VALUES (@FileName, @Make, @FNumber, @ExposureTime, @ISOValue, @Flash, @ExposureProgram, @Keywords, @ByLine, @CopyrightNotice, @Headline, @Caption, @fk_Cameras_ID, @fk_Photographers_ID);";

            SqlParameter filename = new SqlParameter("@FileName", SqlDbType.Text, p.FileName.Length);
            filename.Value = p.FileName;
            c.Parameters.Add(filename);

            SqlParameter Make = new SqlParameter("@Make", SqlDbType.Text, string.IsNullOrEmpty(p.EXIF.Make) ? 1 : p.EXIF.Make.Length);
            Make.Value = p.EXIF.Make;
            c.Parameters.Add(Make);

            SqlParameter FNumber = new SqlParameter("@FNumber", SqlDbType.Decimal, 0);
            FNumber.Precision = 18;
            FNumber.Scale = 2;
            FNumber.Value = (decimal)p.EXIF.FNumber;
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
                fk_Photographers_ID.Value = DBNull.Value;
            }
            c.Parameters.Add(fk_Photographers_ID);

            c.Prepare();
            try
            {
                c.ExecuteNonQuery();
                c.CommandText = "SELECT ID FROM Pictures WHERE FileName LIKE @File;";
                SqlParameter fileName = new SqlParameter("@File", SqlDbType.Text, p.FileName.Length);
                fileName.Value = p.FileName;
                c.Parameters.Add(fileName);
                SqlDataReader dr = c.ExecuteReader();
                if (dr.Read())
                {
                    picture.ID = dr.GetInt32(0);
                    _Pictures.Add(picture.ID, (PictureModel)picture);
                }
            }
            catch (SqlException e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public void Save(IPhotographerModel photographer)
        {
            throw new NotImplementedException();
        }

        public void Save(ICameraModel camera)
        {
            throw new NotImplementedException();
        }

        public void DeleteCamera(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
