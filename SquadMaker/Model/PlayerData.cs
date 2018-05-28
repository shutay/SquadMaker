using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SquadMaker.Model
{
    [DataContract]
    public class PlayerList
    {
        [DataMember(Name = "players")]
        public List<PlayerData> Players { get; set; }
    }

    [DataContract]
    public class PlayerData
    {
        [DataMember(Name = "_id")]
        public string ID { get; set; }
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }
        [DataMember(Name = "skills")]
        public List<PlayerSkill> Skills { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }
        public int ShootingRating { get { return Skills.Single(s => s.Type == "Shooting").Rating;} }
        public int SkatingRating { get { return Skills.Single(s => s.Type == "Skating").Rating; } }
        public int CheckingRating { get { return Skills.Single(s => s.Type == "Checking").Rating; } }
    }

    [DataContract]
    public class PlayerSkill
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "rating")]
        public int Rating { get; set; }
    }
}