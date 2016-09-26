using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace VidLec
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool hasVlcLib = ExtractLibVlc();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginManager loginManager = new LoginManager(hasVlcLib);
            loginManager.AutoLogin();
        }

        private static string _zipPath = Environment.GetEnvironmentVariable("TEMP") + @"\" + @"MyZip.zip";


        /// <summary>
        /// Extracts the contents of a zip file to the 
        /// Temp Folder
        /// </summary>
        private static bool ExtractLibVlc()
        {
            if (File.Exists(Path.Combine(AppConfig.Constants.AppDataFolder, AppConfig.Constants.LibVlcDir, "libvlc.dll")))
                return true;
            try
            {
                //write the resource zip file to the temp directory
                using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("VidLec.libvlc.zip"))
                {
                    using (FileStream bw = new FileStream(_zipPath, FileMode.Create))
                    {
                        //read until we reach the end of the file
                        while (stream.Position < stream.Length)
                        {
                            //byte array to hold file bytes
                            byte[] bits = new byte[stream.Length];
                            //read in the bytes
                            stream.Read(bits, 0, (int)stream.Length);
                            //write out the bytes
                            bw.Write(bits, 0, (int)stream.Length);
                        }
                    }
                    stream.Close();
                }

                if (File.Exists(_zipPath))
                {
                    Directory.CreateDirectory(Path.Combine(AppConfig.Constants.AppDataFolder,
                        AppConfig.Constants.LibVlcDir));
                    System.IO.Compression.ZipFile.ExtractToDirectory(_zipPath, Path.Combine(AppConfig.Constants.AppDataFolder, AppConfig.Constants.LibVlcDir));
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
