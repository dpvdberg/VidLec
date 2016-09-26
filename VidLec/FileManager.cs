using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidLec
{
    class FileManager
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private string _catalogDetailsPath = Path.Combine(AppConfig.Constants.AppDataFolder, AppConfig.Constants.CatalogDetailsSubDir);
        private string _videoPath = Path.Combine(AppConfig.Constants.AppDataFolder, AppConfig.Constants.VideoSubDir);

        public FileManager()
        {
            List<string> directories = new List<string> { AppConfig.Constants.AppDataFolder,
                                                          _catalogDetailsPath,
                                                          _videoPath };
            foreach (string dir in directories)
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
        }

        public void ClearPath(string path)
        {
            foreach (string file in Directory.GetFiles(path))
                File.Delete(file);
        }

        public void SaveCatalogDetails(Folder rootFolder)
        {
            string serializationFile = Path.Combine(_catalogDetailsPath, DateTime.Now.ToBinary() + ".bin");

            ClearPath(_catalogDetailsPath);

            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, rootFolder);
            }
        }

        public Folder GetStoredCatalogDetails()
        {
            if (Directory.GetFiles(_catalogDetailsPath).Count() == 0)
                return null;
            if (Directory.GetFiles(_catalogDetailsPath).Count() == 1)
            {
                string fullPath = Directory.GetFiles(_catalogDetailsPath)[0];
                string fileName = Path.GetFileNameWithoutExtension(fullPath);
                TimeSpan diff = TimeSpan.Zero;
                try
                {
                    diff = DateTime.Now - DateTime.FromBinary(Convert.ToInt64(fileName));
                }
                catch (Exception)
                {
                    _logger.Debug("Could not parse datetime of catalog details file");
                }
                if (diff == TimeSpan.Zero || diff.Days > AppConfig.AppChosenVariables.CatalogDetailsRetentionDays)
                {
                    _logger.Debug("Catalog details file not valid");
                    ClearPath(_catalogDetailsPath);
                    return null;
                }
                using (Stream stream = File.Open(fullPath, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (Folder)bformatter.Deserialize(stream);
                }
            }
            if (Directory.GetFiles(_catalogDetailsPath).Count() > 1)
            {
                _logger.Error("Found multiple catalog details files, cleaning up..");
                ClearPath(_catalogDetailsPath);
                return null;
            }
            _logger.Error("Something extraordinary weird happend, please do report this!");
            return null;
        }
    }
}
