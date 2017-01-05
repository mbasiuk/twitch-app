using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Twitch.Settings;

namespace Twitch.Models
{

    public class Channel
    {
        public string name { get; set; }
        public string status { get; set; }
    }

    public class Stream
    {
        public Channel channel { get; set; }
        public int viewers { get; set; }
        public Preview preview { get; set; }
    }

    public class Preview
    {
        public string small { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
        public string template { get; set; }
    }

    public class StreamCollection
    {
        public List<Stream> streams { get; set; }
    }
}

namespace Twitch.Settings
{
    public class Auth
    {
        public const string ClientIdHeader = "Client-ID";
        public const string ClientId = "7bjbjzp3jzq3zc2n55epfoih9s54zn";
    }

    public class StreamsRefresher
    {
        public const int IntervalInSeconds = 10;
    }
}

namespace Twitch.Models.Loaders
{
    public class Dota2
    {
        const string ApiUrl = "https://api.twitch.tv/kraken/streams?limit=20&offset=0&game=Dota+2&on_site=1";

        System.Windows.Threading.DispatcherTimer timer;

        public StreamCollection Get()
        {
            using (var client = new System.Net.WebClient())
            {
                client.Headers.Add(Auth.ClientIdHeader, Auth.ClientId);
                client.Encoding = System.Text.Encoding.UTF8;
                string response = client.DownloadString(ApiUrl);
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                StreamCollection gameJSON = serializer.Deserialize<StreamCollection>(response);
                return gameJSON;
            }
            throw new NotImplementedException();
        }

        public async Task<StreamCollection> GetAsync()
        {
            using (var client = new System.Net.WebClient())
            {
                client.Headers.Add(Auth.ClientIdHeader, Auth.ClientId);
                client.Encoding = System.Text.Encoding.UTF8;
                string response = await client.DownloadStringTaskAsync(ApiUrl);
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                StreamCollection gameJSON = serializer.Deserialize<StreamCollection>(response);
                return gameJSON;
            }
        }

        internal void UpdateEvery(StreamCollection streamCollection, int intervalInSeconds, ICollectionView view)
        {
            if (timer != null)
            {
                throw new NotImplementedException();
            }

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, intervalInSeconds);
            timer.Tick += (a, b) =>
            {
                System.Diagnostics.Trace.WriteLine("tick start");
                streamCollection.streams.Clear();
                streamCollection.streams.AddRange(Get().streams);
                System.Diagnostics.Trace.WriteLine("tick end");
                view.Refresh();
            };
            timer.Start();
        }
    }
}


