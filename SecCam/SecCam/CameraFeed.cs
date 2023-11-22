using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AForge.Video;
using AForge.Video.DirectShow;

namespace SecCam
{
    public class CameraFeed
    {
        FilterInfoCollection _filterInfos;

        public FilterInfoCollection GetAllCameras()
        {
            List<string> cams = new List<string>();
            try
            {
                return new FilterInfoCollection(FilterCategory.VideoInputDevice);
            }
            catch (Exception)
            {
                throw;
            }
        }

 
    }
}
