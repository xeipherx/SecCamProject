using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecCam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CameraFeed camFeed = new CameraFeed();
        FilterInfoCollection camList;
        VideoCaptureDevice device;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void testBtn_Click(object sender, RoutedEventArgs e)
        {
            camList = camFeed.GetAllCameras();
            foreach(FilterInfo cam in camList) 
            {
                this.cbCameraSelector.Items.Add(cam.Name);
            }
            if (camList.Count > 0)
            {
                ShowCameraFeed(camList[0]);
            }
        }

        private void btnShowCamera_Click(object sender, RoutedEventArgs e)
        {
            if(device.IsRunning == true)
            {
                try
                {
                    device.SignalToStop();
                }
                catch   (Exception ex) { }  
            }
            else
            {
                try
                {
                    device.Start();
                }
                catch (Exception ex) { }
            }
        }

        private void VideoCaptureDeviceAddFrame(object sender, NewFrameEventArgs e)
        {
            //this.camImg = (Bitmap)e.Frame.Clone();
            try
            {
                BitmapImage bi;
                using (var bitmap = (Bitmap)e.Frame.Clone())
                {
                    bi = new BitmapImage();
                    bi.BeginInit();
                    MemoryStream ms = new MemoryStream();
                    bitmap.Save(ms, ImageFormat.Bmp);
                    bi.StreamSource = ms;
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.EndInit();
                }
                bi.Freeze();
                Dispatcher.BeginInvoke(new ThreadStart(delegate { camImg.Source = bi; }));


            }
            catch (Exception ex)
            {
                //catch your error here
            }
        }

        private void ShowCameraFeed(FilterInfo feed)
        {
            device = new VideoCaptureDevice(feed.MonikerString);
            device.NewFrame += VideoCaptureDeviceAddFrame;
            device.Start();
        }
    }
}