using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SquadMaker.Model
{
    [DataContract]
    public class Players
    {
        [DataMember]
        public List<PlayerData> players { get; set; }
    }

    [DataContract]
    public class PlayerData
    {
        [DataMember(Name = "_id")]
        public string id { get; set; }
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public PlayerSkill[] skills { get; set; }

        public string fullName { get { return firstName + " " + lastName; } }
    }

    [DataContract]
    public class PlayerSkill
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public int rating { get; set; }
    }
}