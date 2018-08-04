using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeExtract
{
    public class VideoData
    {
        public DateTime publishedAt { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public ResourceId resourceId { get; set; }

    }
}
