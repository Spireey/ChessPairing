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
            public List<string> History { get; }
            public int Elo { get; }

            
            
            public Player(string name, int elo)
            {
                Name = name;
                Score = 0;
                History = new List<string>();
                Elo = elo;
            }

            public void ShowPlayerHistory()
            {
                Console.WriteLine($"History of {Name}");
                foreach (var match in History)
                    Console.WriteLine($"\t{match}");
                Console.WriteLine("----------------------");
            }   
        }
        
        
        
        public List<Player> Players;
        public List<(Player, Player)> CurrentMatches;
        
        private Random _rd;
        
        public int NbRound;
        public int CurrentRound;

        public bool RoundRunning;

        public PairingManager(int nbRound)
        {
            NbRound = nbRound;
            CurrentRound = 1;
            Players = new List<Player>();
            CurrentMatches = new List<(Player,Player)>();
            _rd = new Random();
            RoundRunning = false;
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void CreateNewMatches()
        {
            Console.WriteLine("\tCreating new rounds");
            if (RoundRunning)
            {
                Console.WriteLine("Round still running");
                return;
            }
            RoundRunning = true;
            Player? playerNotPaired = null;
            Player playerToPair;
            
            for (float i = CurrentRound-1; i >= -.5f ; i -= .5f)
            {
                Console.WriteLine($"i = {i} ---------------------");
                // Take all the players with i points
                List<Player> playersWithSameScore = Players.FindAll(player => player.Score == i);
                Console.WriteLine($"Found {playersWithSameScore.Count} with same score");
                
                // If there is no player with this score, skip
                if (playersWithSameScore.Count == 0)
                {
                    // If, at the end, there is only one player left, exempt him of his game
                    if (i == -.5f && playerNotPaired != null)
                    {
                        Console.WriteLine($"Exempting {playerNotPaired.Name} of his game");
                        CurrentMatches.Add((playerNotPaired!, new Player("EXEMPT",0)));
                        
                        //Entering Score
                        EnterScore(CurrentMatches.Count, "1-0");
                    }

                    continue;
                }
                
                // If a player was not paired before, pair him first
                if (playerNotPaired != null)
                {
                    playerToPair = playersWithSameScore[_rd.Next(playersWithSameScore.Count)];
                    playersWithSameScore.Remove(playerToPair);
                    CurrentMatches.Add((playerNotPaired, playerToPair));
                    playerNotPaired = null;
                }
                
                // Skip if no players remaining
                if (playersWithSameScore.Count == 0)
                    continue;
                
                // Create the matches
                // If there is an odd number of players, keep one to next step
                if (playersWithSameScore.Count % 2 == 1)
                {
                    playerNotPaired = playersWithSameScore[_rd.Next(playersWithSameScore.Count)];
                    Console.WriteLine($"Keeping {playerNotPaired.Name} for next step");
                    playersWithSameScore.Remove(playerNotPaired);
                }
                
                // Now we have a even number of players
                int nbOfRoundToCreate = playersWithSameScore.Count / 2;
                for (int j = 0; j < nbOfRoundToCreate; j++)
                {
                    playerToPair = playersWithSameScore[_rd.Next(1, playersWithSameScore.Count)];
                    CurrentMatches.Add((playersWithSameScore[0], playerToPair));
                    Console.WriteLine($"Pairing {playersWithSameScore[0].Name} with {playerToPair.Name}");
                    playersWithSameScore.RemoveAt(0);
                    playersWithSameScore.Remove(playerToPair);
                }
            }
            
            Console.WriteLine();
        }

        public void EnterScore(int idMatch, string result)
        {
            (Player, Player) currentMatch = CurrentMatches[idMatch-1];
            switch (result)
            {
                // Adding scores
                case "1/2-1/2":
                    currentMatch.Item1.Score += 0.5f;
                    currentMatch.Item2.Score += 0.5f;
                    break;
                case "1-0":
                    currentMatch.Item1.Score += 1f;
                    break;
                case "0-1":
                    currentMatch.Item2.Score += 1f;
                    break;
            }
            
            // Adding the result to both player's history
            currentMatch.Item1.History.Add($"{currentMatch.Item1.Name} - {currentMatch.Item2.Name} {result}");
            currentMatch.Item2.History.Add($"{currentMatch.Item1.Name} - {currentMatch.Item2.Name} {result}");
            
            // Removing game of current matches
            CurrentMatches.RemoveAt(idMatch-1);
            
            // Ending Round if no more matches
            if (CurrentMatches.Count == 0)
            {
                RoundRunning = false;
                Console.WriteLine($"END OF THE ROUND {CurrentRound}\n");
                CurrentRound++;
            }
        }
        
        // DEBUGGING FUNCTIONS
        public void ShowCurrentMatches()
        {
            Console.WriteLine("Current matches :");
            for (int i = 0; i < CurrentMatches.Count; i++)
            {
                var match = CurrentMatches[i];
                Console.WriteLine($"\t{i+1}. {match.Item1.Name} - {match.Item2.Name}");
            }
            Console.WriteLine();
        }

        public void ShowAllPlayerHistory()
        {
            foreach (var player in Players)
                player.ShowPlayerHistory();
        }

        public void ShowScoreBoard()
        {
            Console.WriteLine("Scores:");
            for (float i = CurrentRound-1; i >= 0 ; i-=.5f)
            {
                var temp = Players.FindAll(player => player.Score == i);
                foreach (var player in temp)
                    Console.WriteLine($"\t{player.Name} : {player.Score}");
            }
            Console.WriteLine();
        }
    }
    
}

