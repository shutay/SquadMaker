﻿using System;
using System.Collections.Generic;
using System.Linq;
using SquadMaker.Model;

namespace SquadMaker.BLL
{
    public class Squads
    {
        /// <summary>
        /// Gererates the number of squads asked for
        /// </summary>
        public static List<PlayerData>[] GenerateSquads(int numSquads, List<PlayerData> allPlayers, out List<PlayerData> remainingPlayers)
        {
            remainingPlayers = new List<PlayerData>(allPlayers);
            if (numSquads <= 0 || allPlayers.Count() < numSquads)
            {
                return null;
            }
            
            int numPlayersPerSquad = allPlayers.Count() / numSquads;
            double shootingAvg = allPlayers.Select(p => p.ShootingRating).Average();
            double skatingAvg = allPlayers.Select(p => p.SkatingRating).Average();
            double checkingAvg = allPlayers.Select(p => p.CheckingRating).Average();
            
            // initialize all squads
            List<PlayerData>[] squads = new List<PlayerData>[numSquads];
            for (int i = 0; i < squads.Count(); i++)
            {
                squads[i] = new List<PlayerData>();
            }

            // loop though until all squads have enough players
            while (!squads.All(s => s.Count == numPlayersPerSquad))
            {
                for (int i = 0; i < squads.Count(); i++)
                {
                    if (squads[i].Count() == numPlayersPerSquad) { continue; }

                    // If the squad only has one space left, add the single best person you can, then continue
                    if (squads[i].Count() == numPlayersPerSquad - 1)
                    {
                        PlayerData bestBalancingPlayer = GetBestBalancingPlayer(squads[i], remainingPlayers, shootingAvg, skatingAvg, checkingAvg);
                        squads[i].Add(bestBalancingPlayer);
                        remainingPlayers.Remove(bestBalancingPlayer);
                        continue;
                    }

                    // If the squad has more than one space left, add a single player to the squad. Choose the person least closest to average (to get rid of outliers first)
                    // Then, check the squad average against the worst person in the remaining players
                    // While the worst person's average is better than the squad average, continue adding players to balance the squad until the squad is full or the squad average is better than that person's average
                    PlayerData player = GetFurthestPlayerByDistance(remainingPlayers, shootingAvg, skatingAvg, checkingAvg).Key;
                    squads[i].Add(player);
                    remainingPlayers.Remove(player);
                   
                    KeyValuePair<PlayerData, double> furthestPlayer = GetFurthestPlayerByDistance(remainingPlayers, shootingAvg, skatingAvg, checkingAvg);
                    double currSquadDistance = (Math.Abs(squads[i].Sum(s => s.ShootingRating) - (shootingAvg * squads[i].Count))
                        + Math.Abs(squads[i].Sum(s => s.SkatingRating) - (skatingAvg * squads[i].Count))
                        + Math.Abs(squads[i].Sum(s => s.CheckingRating) - (checkingAvg * squads[i].Count)));

                    while (furthestPlayer.Value < currSquadDistance && squads[i].Count != numPlayersPerSquad)
                    {
                        PlayerData balancingPlayer = GetBestBalancingPlayer(squads[i], remainingPlayers, shootingAvg, skatingAvg, checkingAvg);
                        squads[i].Add(balancingPlayer);
                        remainingPlayers.Remove(balancingPlayer);

                        furthestPlayer = GetFurthestPlayerByDistance(remainingPlayers, shootingAvg, skatingAvg, checkingAvg);
                        currSquadDistance = (Math.Abs(squads[i].Sum(s => s.ShootingRating) - (shootingAvg * squads[i].Count))
                            + Math.Abs(squads[i].Sum(s => s.SkatingRating) - (skatingAvg * squads[i].Count))
                            + Math.Abs(squads[i].Sum(s => s.CheckingRating) - (checkingAvg * squads[i].Count)));
                    }
                }
            }
            return squads;
        }

        /// <summary>
        /// Loops through the player list to find the player whose total skills are furthest from average
        /// </summary>
        protected static KeyValuePair<PlayerData, double> GetFurthestPlayerByDistance(List<PlayerData> players, double shootingAvg, double skatingAvg, double checkingAvg)
        {
            double furthestDistance = 0;
            PlayerData furthestPlayer = null;

            foreach (PlayerData player in players)
            {
                double playerDistance = Math.Abs(shootingAvg - player.ShootingRating) + Math.Abs(skatingAvg - player.SkatingRating) + Math.Abs(checkingAvg - player.CheckingRating);
                if (playerDistance > furthestDistance)
                {
                    furthestPlayer = player;
                    furthestDistance = playerDistance;
                }
            }
            return new KeyValuePair<PlayerData, double>(furthestPlayer, furthestDistance);
        }

        /// <summary>
        /// Of all the remaining players, loops thorugh the playerlist to find the best player that will make the current squad numbers closest to average
        /// </summary>
        protected static PlayerData GetBestBalancingPlayer(List<PlayerData> squad, List<PlayerData> remainingPlayers, double shootingAvg, double skatingAvg, double checkingAvg)
        {
            double idealPlayerShooting = ((squad.Count + 1) * shootingAvg) - squad.Sum(s => s.ShootingRating);
            double idealPlayerSkating = ((squad.Count + 1) * skatingAvg) - squad.Sum(s => s.SkatingRating);
            double idealPlayerChecking = ((squad.Count + 1) * checkingAvg) - squad.Sum(s => s.CheckingRating);

            double closestPlayerDiff = 0;
            PlayerData closestPlayer = null;
            foreach (PlayerData player in remainingPlayers)
            {
                double diffToIdeal = Math.Abs(idealPlayerShooting - player.ShootingRating) +
                    Math.Abs(idealPlayerSkating - player.SkatingRating) + Math.Abs(idealPlayerChecking - player.CheckingRating);
                if (diffToIdeal < closestPlayerDiff || closestPlayer == null)
                {
                    closestPlayer = player;
                    closestPlayerDiff = diffToIdeal;
                }
            }
            return closestPlayer;
        }
    }
}