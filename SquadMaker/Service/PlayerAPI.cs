using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Caching;
using SquadMaker.Model;

namespace SquadMaker.Service
{
    public class PlayerAPI
    {
        public static PlayerList GetAllPlayers()
        {
            if (HttpRuntime.Cache["PlayerList"] != null)
                return HttpRuntime.Cache["PlayerList"] as PlayerList;
            
            string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testjson.txt");
            string json = File.ReadAllText(testFile);
            PlayerList players = Deserialize<PlayerList>(json);

            HttpRuntime.Cache.Insert("PlayerList", players, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
            return players;
        }

        private static T Deserialize<T>(string json) where T : class
        {
            byte[] b = Encoding.UTF8.GetBytes(json);
            MemoryStream m = new MemoryStream(b);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            return ser.ReadObject(m) as T;
        }
    }
}