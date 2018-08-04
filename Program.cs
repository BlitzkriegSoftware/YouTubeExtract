using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace YouTubeExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            if ((args == null) || (args.Length <= 0))
            {
                return;
            }

            var filename = args[0];

            var text = File.ReadAllText(filename);

            var index = text.IndexOf('{');
            if (index > 0) text = text.Substring(index);

            var content = JsonConvert.DeserializeObject<RootObject>(text);

            var list = new List<VideoData>();

            foreach (var item in content.items)
            {
                var v = new VideoData()
                {
                    title = item.snippet.title,
                    description = item.snippet.description,
                    publishedAt = item.snippet.publishedAt,
                    resourceId = item.snippet.resourceId
                };
                list.Add(v);
            }

            filename = Path.ChangeExtension(filename, ".csv");
            if (File.Exists(filename)) File.Delete(filename);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
            {
                file.WriteLine("\"Title\", \"Description\", \"PublishDate\", \"Url\"");
                foreach(var v in list)
                {
                    file.WriteLine("\"{0}\",\"{1}\",\"{2}\",\"{3}\"", v.title, MinDescription(v.description), v.publishedAt, MakeUrl( v.resourceId.videoId));
                }
            }
        }

        static string MinDescription(string s)
        {
            int i = s.IndexOf("Table of Contents: ");
            if(i > 0) s = s.Substring(0, i);
            s = s.Trim();
            return s;
        }

        static string MakeUrl(string s)
        {
            return string.Format("https://www.youtube.com/watch?v={0}", s);
        }

    }
}
