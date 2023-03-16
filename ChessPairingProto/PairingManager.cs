using System;
using System.Collections.Generic;

namespace ChessPairingProto
{
    public class PairingManager
    {
        public class Player
        {
            public string Name { get; }
            public float Score { get; set; }
            public string[] History { set; get; }
            public int Elo { get; }

            
            
            public Player(string name, int elo, int nbRound)
            {
                Name = name;
                Score = 0;
                History = new string[nbRound];
                Elo = elo;
            }
        }
        
        int NbRound;
        int CurrentRound;
        
        List<Player> Players;
        List<(int, Player, Player)> CurrentMatches;
        
        private Random _rd;

        public PairingManager(int nbRound)
        {
            NbRound = nbRound;
            Players = new List<Player>();
            CurrentMatches = new List<(int,Player,Player)>();
            _rd = new Random();
        }

        public void AddPlayer(string name, int elo)
        {
            Players.Add(new Player(name, elo, NbRound));
        }

        public void CreateNewMatches()
        {
            int idMatch = 0;
            Player? playerNotPaired = null;
            Player playerToPair;
            
            for (float i = CurrentRound; i >= -.5f ; i -= .5f)
            {
                // Take all the players with i points
                List<Player> playersWithSameScore = Players.FindAll(player => player.Score == i);
                
                // If there is no player with this score, skip
                if (playersWithSameScore.Count == 0)
                {
                    // If, at the end, there is only one player left, exempt him of his game
                    if (CurrentRound == -.5f)
                    {
                        CurrentMatches.Add((idMatch, playerNotPaired!, new Player("EXEMPT",0,NbRound)));
                        EnterScore(idMatch, "1-0");
                    }
                    else
                        continue;
                }
                
                // If a player was not paired before, pair him first
                if (playerNotPaired != null)
                {
                    playerToPair = playersWithSameScore[_rd.Next(playersWithSameScore.Count)];
                    playersWithSameScore.Remove(playerToPair);
                    CurrentMatches.Add((idMatch, playerNotPaired, playerToPair));
                    idMatch++;
                    playerNotPaired = null;
                }
                
                // Skip if no players remaining
                if (playersWithSameScore.Count == 0)
                    continue;
                
                // Creates the matches
                // If there is an odd number of players, keep one to next step
                if (playersWithSameScore.Count % 2 == 1)
                {
                    playerNotPaired = playersWithSameScore[_rd.Next(playersWithSameScore.Count)];
                    playersWithSameScore.Remove(playerNotPaired);
                }
                
                // Now we have a even number of players
                int nbOfRoundToCreate = playersWithSameScore.Count / 2;
                for (int j = 0; j < nbOfRoundToCreate; j++)
                {
                    playerToPair = playersWithSameScore[_rd.Next(1, playersWithSameScore.Count)];
                    CurrentMatches.Add((idMatch, playersWithSameScore[0], playerToPair));
                    playersWithSameScore.RemoveAt(0);
                    playersWithSameScore.Remove(playerToPair);
                }
            }
        }

        public void EnterScore(int idMatch, string result)
        {
            (int, Player, Player) currentMatch = CurrentMatches[idMatch];
            switch ("1/2-1/2")
            {
                
            }
        }
    }
}

