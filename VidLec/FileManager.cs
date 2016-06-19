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
        private Logger logger = LogManager.GetCurrentClassLogger();
        private string catalogDetailsPath = Path.Combine(AppConfig.Constants.appDataFolder, AppConfig.Constants.catalogDetailsSubDir);
        private string videoPath = Path.Combine(AppConfig.Constants.appDataFolder, AppConfig.Constants.videoSubDir);

        public FileManager()
        {
            List<string> directories = new List<string> { AppConfig.Constants.appDataFolder,
                                                          catalogDetailsPath,
                                                          videoPath
                                                        };
            foreach (string dir in directories)
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
        }

        public void clearPath(string path)
        {
            foreach (string file in Directory.GetFiles(path))
                File.Delete(file);
        }

        public void saveCatalogDetails(Folder rootFolder)
        {
            string serializationFile = Path.Combine(catalogDetailsPath, DateTime.Now.ToBinary() + ".bin");

            clearPath(catalogDetailsPath);

            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, rootFolder);
            }
        }

        public Folder getStoredCatalogDetails()
        {
            if (Directory.GetFiles(catalogDetailsPath).Count() == 0)
                return null;
            else if (Directory.GetFiles(catalogDetailsPath).Count() == 1)
            {
                string fullPath = Directory.GetFiles(catalogDetailsPath)[0];
                string fileName = Path.GetFileNameWithoutExtension(fullPath);
                DateTime now = DateTime.Now;
                TimeSpan diff = TimeSpan.Zero;
                try
                {
                    diff = DateTime.Now - DateTime.FromBinary(Convert.ToInt64(fileName));
                }
                catch (Exception)
                {
                    logger.Debug("Could not parse datetime of catalog details file");
                }
                if (diff == TimeSpan.Zero || diff.Days > AppConfig.AppChosenVariables.catalogDetailsRetentionDays)
                {
                    logger.Debug("Catalog details file not valid");
                    clearPath(catalogDetailsPath);
                    return null;
                }
                else
                {
                    using (Stream stream = File.Open(fullPath, FileMode.Open))
                    {
                        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        return (Folder)bformatter.Deserialize(stream);
                    }
                }
            }
            else if (Directory.GetFiles(catalogDetailsPath).Count() > 1)
            {
                logger.Error("Found multiple catalog details files, cleaning up..");
                clearPath(catalogDetailsPath);
                return null;
            }
            else
            {
                logger.Error("Something extraordinary weird happend, please do report this!");
                return null;
            }
        }
    }
}
