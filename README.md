# ScreenRecordGif v0.1-alpha
Simple Screen Record to Gif - Class in C#  .NET 4+

### A brief introduction
The class allows you to capture a region of the screen and save as animated GIF, without using any additional library.

To use:
```c#
        ScreenRecordGif workerScreenRecordGif;
        private void btnRecord_Click(object sender, RoutedEventArgs e)
        {
            if (workerScreenRecordGif == null)
            {
                workerScreenRecordGif = new ScreenRecordGif(getBoundsGrid());
            }
            workerScreenRecordGif.Record();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            workerScreenRecordGif.Pause();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            workerScreenRecordGif.StopSave("Capture.gif");
            //   Done ! ! !
        }
        
        //getBoundsGrid() - need return a bounds object
```
