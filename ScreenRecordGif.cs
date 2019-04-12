namespace FabricioRHS
{
	// <copyright file="ScreenRecordGif.cs" company="FabricioRHS">
	// Copyright (c) 2019 All Rights Reserved
	// </copyright>
	// <author>Fabricio Rodrigues</author>
	// <date>2019-04-12</date>
	// <summary>Class to record screen and save in Gif file</summary>

    class ScreenRecordGif
    {
        protected System.Drawing.Rectangle _bounds;
        protected System.Windows.Media.Imaging.GifBitmapEncoder _gifEncoder;
        protected System.Windows.Threading.DispatcherTimer _dispatcherTimer;

        public ScreenRecordGif(System.Drawing.Rectangle bounds)
        {
            _bounds = bounds;
            this.InitLocalData();
        }

        protected void InitLocalData()
        {
            _gifEncoder = new System.Windows.Media.Imaging.GifBitmapEncoder();
            _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            _dispatcherTimer.Tick += EventCapture;
            _dispatcherTimer.Interval = new System.TimeSpan(0, 0, 0, 0, 62);
        }
    
        public void Pause()
        {
            _dispatcherTimer.Stop();
        }

        public void Record()
        {
            if (_dispatcherTimer.IsEnabled) return;
            _dispatcherTimer.Start();
        }

        public void StopSave(string fileNameGif)
        {
            this.Pause();
            this.SaveFile(fileNameGif);
            if (_gifEncoder != null) _gifEncoder.Frames.Clear();
            this.InitLocalData();
        }

        protected void SaveFile( string fileNameGif)
        {
            if (System.IO.File.Exists(fileNameGif)) System.IO.File.Delete(fileNameGif);
            using (System.IO.FileStream fs = new System.IO.FileStream(fileNameGif, System.IO.FileMode.Create))
            {
                _gifEncoder.Save(fs);
            }
        }

        protected void EventCapture(object sender, System.EventArgs e)
        {
            int Width = _bounds.Width;
            int Height = _bounds.Height;
            using (System.Drawing.Bitmap target = new System.Drawing.Bitmap(Width, Height))
            {
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(target))
                {
                    g.CopyFromScreen(new System.Drawing.Point(_bounds.Left, _bounds.Top), System.Drawing.Point.Empty, new System.Drawing.Size(Width, Height), System.Drawing.CopyPixelOperation.SourceCopy);
                }
                using (var ms = new System.IO.MemoryStream())
                {
                    target.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    _gifEncoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(ms, System.Windows.Media.Imaging.BitmapCreateOptions.PreservePixelFormat, System.Windows.Media.Imaging.BitmapCacheOption.OnLoad));
                }
            }

        }
    }
}
