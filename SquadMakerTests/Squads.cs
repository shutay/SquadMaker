using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SquadMaker.Model;

namespace SquadMakerTests
{
    [TestClass]
    public class Squads
    {
        [TestMethod]
        public void TestSquadGeneration()
        {
            // Generate mock player list
            List<PlayerData> playerList = new List<PlayerData>();
            playerList.Add(new PlayerData() { ID = "1", FirstName = "Alex", LastName = "Carney",
                Skills = new List<PlayerSkill>() { new PlayerSkill { Type = "Shooting", Rating = 90 }, new PlayerSkill { Type = "Skating", Rating = 98 }, new PlayerSkill { Type = "Checking", Rating = 92 } } });
            playerList.Add(new PlayerData() { ID = "2", FirstName = "Bob", LastName = "Smith",
                Skills = new List<PlayerSkill>() { new PlayerSkill { Type = "Shooting", Rating = 80 }, new PlayerSkill { Type = "Skating", Rating = 60 }, new PlayerSkill { Type = "Checking", Rating = 50 } } });
            playerList.Add(new PlayerData() { ID = "3", FirstName = "Roy", LastName = "Talbot",
                Skills = new List<PlayerSkill>() { new PlayerSkill { Type = "Shooting", Rating = 60 }, new PlayerSkill { Type = "Skating", Rating = 85 }, new PlayerSkill { Type = "Checking", Rating = 20 } } });
            playerList.Add(new PlayerData() { ID = "4", FirstName = "Jill", LastName = "White",
                Skills = new List<PlayerSkill>() { new PlayerSkill { Type = "Shooting", Rating = 70 }, new PlayerSkill { Type = "Skating", Rating = 90 }, new PlayerSkill { Type = "Checking", Rating = 60 } } });
            playerList.Add(new PlayerData() { ID = "5", FirstName = "Jennifer", LastName = "Wu",
                Skills = new List<PlayerSkill>() { new PlayerSkill { Type = "Shooting", Rating = 94 }, new PlayerSkill { Type = "Skating", Rating = 55 }, new PlayerSkill { Type = "Checking", Rating = 100 } } });

            List<PlayerData> remainingPlayers;
            List<PlayerData>[] squads = SquadMaker.BLL.Squads.GenerateSquads(2, playerList, out remainingPlayers);

            Assert.AreEqual(2, squads.Count(), "The number of squads created should be correct");
            foreach(List<PlayerData> players in squads)
            {
                Assert.AreEqual(2, players.Count(), "The number of players per squad should be correct");
            }
            Assert.AreEqual(1, remainingPlayers.Count(), "The number of players remaining in the waiting list should be correct");
        }

        [TestMethod]
        public void TestSquadGenerationWithInvalidInputs()
        {
            List<PlayerData> playerList = new List<PlayerData>();
            List<PlayerData> remainingPlayers;
            List<PlayerData>[] squads = SquadMaker.BLL.Squads.GenerateSquads(1, playerList, out remainingPlayers);
            Assert.IsNull(squads, "If player list is empty, no squads should be returned");
            Assert.AreEqual(0, remainingPlayers.Count(), "If player list is empty, there should be no remaining players");

            playerList.Add(new PlayerData() { ID = "1", FirstName = "Alex", LastName = "Carney",
                Skills = new List<PlayerSkill>() { new PlayerSkill { Type = "Shooting", Rating = 90 }, new PlayerSkill { Type = "Skating", Rating = 98 }, new PlayerSkill { Type = "Checking", Rating = 92 } } });

            squads = SquadMaker.BLL.Squads.GenerateSquads(-1, playerList, out remainingPlayers);
            Assert.IsNull(squads, "If number of squads is invalid, no squads should be returned");
            Assert.AreEqual(playerList.Count(), remainingPlayers.Count(), "If no squads are created, the number of remaining players should be the number of players in the playerlist");

            squads = SquadMaker.BLL.Squads.GenerateSquads(0, playerList, out remainingPlayers);
            Assert.IsNull(squads, "If number of squads is 0, no squads should be returned");
            Assert.AreEqual(playerList.Count(), remainingPlayers.Count(), "If no squads are created, the number of remaining players should be the number of players in the playerlist");

            squads = SquadMaker.BLL.Squads.GenerateSquads(2, playerList, out remainingPlayers);
            Assert.IsNull(squads, "If number of squads is greater than the number of players, no squads should be returned");
            Assert.AreEqual(playerList.Count(), remainingPlayers.Count(), "If no squads are created, the number of remaining players should be the number of players in the playerlist");
        }
    }
}
