using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using VidLec;

namespace Vlc.DotNet.Forms.Samples
{
    public partial class LectureViewer : Form
    {
        private Dictionary<string, string> videoUrls;
        public LectureViewer(Dictionary<string, string> videoUrls)
        {
            this.videoUrls = videoUrls;
            InitializeComponent();
        }

        private void OnVlcControlNeedLibDirectory(object sender, VlcLibDirectoryNeededEventArgs e)
        {
            e.VlcLibDirectory = new DirectoryInfo(Path.Combine(AppConfig.Constants.AppDataFolder, AppConfig.Constants.LibVlcDir));

            if (!e.VlcLibDirectory.Exists)
            {
                var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                folderBrowserDialog.Description = "Select Vlc libraries folder.";
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
                folderBrowserDialog.ShowNewFolderButton = true;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    e.VlcLibDirectory = new DirectoryInfo(folderBrowserDialog.SelectedPath);
                }
            }
        }

        private void OnButtonPlayClicked(object sender, EventArgs e)
        {
            myVlcControl.Audio.IsMute = false;
            myVlcControl.Audio.Volume = 100;
            Console.WriteLine(videoUrls.Values.ElementAt(0));
            myVlcControl.Play(new Uri(videoUrls.Values.ElementAt(0)));
        }

        private void OnButtonStopClicked(object sender, EventArgs e)
        {
            myVlcControl.Position += 0.1f;
            //myVlcControl.Stop();
        }

        private void OnButtonPauseClicked(object sender, EventArgs e)
        {
            myVlcControl.Rate += 0.1f;
            //myVlcControl.Pause();
        }

        private void OnVlcMediaLengthChanged(object sender, Core.VlcMediaPlayerLengthChangedEventArgs e)
        {
#if !NET20
            myLblMediaLength.InvokeIfRequired(l => l.Text = new DateTime(new TimeSpan((long)e.NewLength).Ticks).ToString("T"));
#else
            ControlExtensions.InvokeIfRequired(myLblMediaLength, l => l.Text = new DateTime(new TimeSpan((long)e.NewLength).Ticks).ToString("T"));
#endif
        }

        private void OnVlcPositionChanged(object sender, Core.VlcMediaPlayerPositionChangedEventArgs e)
        {
            var position = myVlcControl.GetCurrentMedia().Duration.Ticks * e.NewPosition;
#if !NET20
            myLblVlcPosition.InvokeIfRequired(l => l.Text = new DateTime((long)position).ToString("T"));
#else
            ControlExtensions.InvokeIfRequired(myLblVlcPosition, c => c.Text = new DateTime((long)position).ToString("T"));
#endif
        }

        private void OnVlcPaused(object sender, Core.VlcMediaPlayerPausedEventArgs e)
        {
#if !NET20
            myLblState.InvokeIfRequired(l => l.Text = "Paused");
#else
            ControlExtensions.InvokeIfRequired(myLblState, c => c.Text = "Paused");
#endif
        }

        private void OnVlcStopped(object sender, Core.VlcMediaPlayerStoppedEventArgs e)
        {
#if !NET20
            myLblState.InvokeIfRequired(l => l.Text = "Stopped");

            myCbxAspectRatio.InvokeIfRequired(c =>
            {
                c.Text = string.Empty;
                c.Enabled = false;
            });
#else
            ControlExtensions.InvokeIfRequired(myLblState, c => c.Text = "Stopped");

            ControlExtensions.InvokeIfRequired(myCbxAspectRatio, c =>
            {
                c.Text = string.Empty;
                c.Enabled = false;
            });
#endif
            myLblAudioCodec.Text = "Codec: ";
            myLblAudioChannels.Text = "Channels: ";
            myLblAudioRate.Text = "Rate: ";
            myLblVideoCodec.Text = "Codec: ";
            myLblVideoHeight.Text = "Height: ";
            myLblVideoWidth.Text = "Width: ";



        }

        private void OnVlcPlaying(object sender, Core.VlcMediaPlayerPlayingEventArgs e)
        {
#if !NET20
            myLblState.InvokeIfRequired(l => l.Text = "Playing");

            myLblAudioCodec.InvokeIfRequired(l => l.Text = "Codec: ");
            myLblAudioChannels.InvokeIfRequired(l => l.Text = "Channels: ");
            myLblAudioRate.InvokeIfRequired(l => l.Text = "Rate: ");
            myLblVideoCodec.InvokeIfRequired(l => l.Text = "Codec: ");
            myLblVideoHeight.InvokeIfRequired(l => l.Text = "Height: ");
            myLblVideoWidth.InvokeIfRequired(l => l.Text = "Width: ");

            var mediaInformations = myVlcControl.GetCurrentMedia().TracksInformations;
            foreach (var mediaInformation in mediaInformations)
            {
                if (mediaInformation.Type == Core.Interops.Signatures.MediaTrackTypes.Audio)
                {
                    myLblAudioCodec.InvokeIfRequired(l => l.Text += mediaInformation.CodecName);
                    myLblAudioChannels.InvokeIfRequired(l => l.Text += mediaInformation.Audio.Channels);
                    myLblAudioRate.InvokeIfRequired(l => l.Text += mediaInformation.Audio.Rate);
                }
                else if (mediaInformation.Type == Core.Interops.Signatures.MediaTrackTypes.Video)
                {
                    myLblVideoCodec.InvokeIfRequired(l => l.Text += mediaInformation.CodecName);
                    myLblVideoHeight.InvokeIfRequired(l => l.Text += mediaInformation.Video.Height);
                    myLblVideoWidth.InvokeIfRequired(l => l.Text += mediaInformation.Video.Width);
                }
            }

            myCbxAspectRatio.InvokeIfRequired(c =>
            {
                c.Text = myVlcControl.Video.AspectRatio;
                c.Enabled = true;
            });

#else
            ControlExtensions.InvokeIfRequired(myLblState, c => c.Text = "Playing");

            ControlExtensions.InvokeIfRequired(myLblAudioCodec, c => c.Text = "Codec: ");
            ControlExtensions.InvokeIfRequired(myLblAudioChannels, c => c.Text = "Channels: ");
            ControlExtensions.InvokeIfRequired(myLblAudioRate, c => c.Text = "Rate: ");
            ControlExtensions.InvokeIfRequired(myLblVideoCodec, c => c.Text = "Codec: ");
            ControlExtensions.InvokeIfRequired(myLblVideoHeight, c => c.Text = "Height: ");
            ControlExtensions.InvokeIfRequired(myLblVideoWidth, c => c.Text = "Width: ");

            var mediaInformations = myVlcControl.GetCurrentMedia().TracksInformations;
            foreach (var mediaInformation in mediaInformations)
            {
                if (mediaInformation.Type == Core.Interops.Signatures.MediaTrackTypes.Audio)
                {
                    ControlExtensions.InvokeIfRequired(myLblAudioCodec, c => c.Text += mediaInformation.CodecName);
                    ControlExtensions.InvokeIfRequired(myLblAudioChannels, c => c.Text += mediaInformation.Audio.Channels);
                    ControlExtensions.InvokeIfRequired(myLblAudioRate, c => c.Text += mediaInformation.Audio.Rate);
                }
                else if (mediaInformation.Type == Core.Interops.Signatures.MediaTrackTypes.Video)
                {
                    ControlExtensions.InvokeIfRequired(myLblVideoCodec, c => c.Text += mediaInformation.CodecName);
                    ControlExtensions.InvokeIfRequired(myLblVideoHeight, c => c.Text += mediaInformation.Video.Height);
                    ControlExtensions.InvokeIfRequired(myLblVideoWidth, c => c.Text += mediaInformation.Video.Width);
                }
            }

            ControlExtensions.InvokeIfRequired(myCbxAspectRatio, c =>
            {
                c.Text = myVlcControl.Video.AspectRatio;
                c.Enabled = true;
            });
#endif
        }

        private void myCbxAspectRatio_TextChanged(object sender, EventArgs e)
        {
            myVlcControl.Video.AspectRatio = myCbxAspectRatio.Text;
        }

        private void Sample_SizeChanged(object sender, EventArgs e)
        {
            ResizeVlcControl();
        }

        void ResizeVlcControl()
        {
            if (!string.IsNullOrEmpty(myCbxAspectRatio.Text))
            {
                var ratio = myCbxAspectRatio.Text.Split(':');
                int width, height;
                if (ratio.Length == 2 && int.TryParse(ratio[0], out width) && int.TryParse(ratio[1], out height))
                {
                    myVlcControl.Width = myVlcControl.Height*width/height;
                }
            }
        }

    }
}
