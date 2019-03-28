using System;
using System.Collections.Generic;
using DesktopAppWithVideo.LibVlcSharpShared;
using Gtk;
using LibVLCSharp.GTK;
using LibVLCSharp.Shared;

namespace DesktopAppWithVideo
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Core.Initialize();

            Application.Init();
            using (var libvlc = new LibVLC())
            {
                MainWindow win = new MainWindow();
                win.Fullscreen();

                var size = win.GetDisplaySize();
                HBox hbox = new HBox();
                hbox.HeightRequest = size.Height;
                // Creates the video view, and adds it to the window
                var mp = new MediaPlayer(libvlc);
                VideoView videoView = new VideoView { MediaPlayer = mp };
                hbox.PackStart(videoView);
                hbox.Children[0].WidthRequest = (int)(size.Width * 0.8);
                hbox.Children[0].HeightRequest = size.Height;

                win.Add(hbox);

                win.ShowAll();

                string basePath = Environment.CurrentDirectory + "/../../Media/";
                var playList = new List<string>()
                {
                    basePath + "SampleVideo_1280x720_1mb.mp4",
                    basePath + "SampleVideo_1280x720_2mb.mp4"
                };

                MediaList mediList = new MediaList(libvlc);
                foreach (var movie in playList)
                {
                    var media = new Media(libvlc, movie, FromType.FromPath);
                    mediList.AddMedia(media);
                }

                var mlp = new MediaListPlayer(libvlc);

                mlp.SetMediaList(mediList);
                mlp.SetMediaPlayer(mp);
                mlp.Play();

                win.DeleteEvent += (sender, a) =>
                {
                    mp.Stop();
                    videoView.Dispose();
                    Application.Quit();
                    a.RetVal = true;
                };

                Application.Run();
            }
        }
    }
}
