using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidLec
{
    class Config
    {
        public bool onlineMode = false;

        public static class Constants
        {
            const int statusWaitResetInterval = 800;

            public const string onlineModeText = "Online mode activated";
            public const string offlineModeText = "Offline mode activated";

            public const string noConnectionText = "No network connection available, entering offline mode..";

            public const string statusWaitText = "Waiting for user input";
            public const string loadingDataText = "Getting data from server..";
            public const string loadedText = "Loaded successfully";
            public const string loadingErrorText = "Error loading folders..";
        }

        public static class AppColors
        {
            public static readonly Color ErrorText = Color.Red;
            public static readonly Color OKText = Color.SeaGreen;
            public static readonly Color BlueText = Color.RoyalBlue;
        }
    }
}
