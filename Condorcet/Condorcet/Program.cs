/*
 * COMMENT Program.cs
 * COMMENT Class containing presentation and buisness logic for condorcet election
 * COMMENT
 * COMMENT Version 1.0
 * COMMENT Harjot Singh,
 *         Karanbir Singh,
 *         Simranjot Singh, 
 *         Nwakwue Godson, 
 *         Oluwafemi Albert Hughes 
 *          
 *         2015.04.17: Created
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condorcet
{
    class Program
    {
        static Ballet ballet = new Ballet();  
 
        //This is method to get the preferences cadidates per voter
        static public string getPreferencedCandidatePerVoter(int a, int b, string[] voters)
        {

            int indexOfX = Array.IndexOf(voters, a.ToString());
            int indexOfY = Array.IndexOf(voters, b.ToString());

            if (indexOfX < indexOfY)
                return a.ToString();
            else
                return b.ToString();

        }

        //This is method to get total preferenced candidate
        static public string[] getPreferencedCandidate(string[] voters)
        {
            string[] winner = new string[10];
            int w1 = 0;
            for (int a = 0; a < voters.Length; a++)
            {
                for (int b = a; b < voters.Length; b++)
                {
                    if (b == a)
                        continue;
                    else
                    {
                        winner[w1] = getPreferencedCandidatePerVoter(a, b, voters);
                        w1++;
                    }
                }
            }

            return winner;
        }

        //This is method to get number of votes per candidate
        static public List<int[]> getNumberOfVotesPerCandidate(List<string[]> winner)
        {
            List<int[]> occurenceOfVotes = new List<int[]>();

            for (int i = 0; i < winner.Count; i++)
            {

                if (winner[i].Length > 0)
                {
                    int[] o = new int[10];
                    

                    for (int j = 0; j < winner[i].Count(); j++)
                    {
                        int count = 0;
                        if (winner[i][j] != null)
                        {
                            for (int k = 0; k < winner[i].Count(); k++)
                            {
                                if (winner[i][k] != null && winner[i][j] != null)
                                {

                                    if (winner[i][k] == j.ToString())
                                    {
                                        count++;
                                    }
                                    else continue;
                                }
                                
                            }
                            
                        }
                        o[j] = count;

                    }
                    occurenceOfVotes.Add(o);                 
                }
                
            } 

            return occurenceOfVotes;
        }

        //This is method to find duplicate in votes
        public static bool ContainsDuplicates(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[i] == a[j]) return true;
                }
            }
            return false;
        }

        //This is method to get total votes
        static public bool getVotes(string[] words)
        {

            bool isCondorcetWinner = false;
            
            List<string[]> voted = new List<string[]>();  

            List<string[]> candidates = new List<string[]>();


            for (int i = 0; i < Convert.ToInt32(words[1]); i++)
            {
                string[] vote1 = new string[3];

                Console.WriteLine("Enter the votes for Candidate Number {0}", i + 1);
                Console.WriteLine("Each voter enter votes followed by space");

                string candidateVotes = Console.ReadLine();

                string[] votes = candidateVotes.Split(' ');

                if (votes.Length > Convert.ToInt32(words[0]))
                {
                    Console.WriteLine("Not more than {0} candidates can vote", Convert.ToInt32(words[1]));
                    getVotes(words);
                }
                int w = 0;
                foreach (string vote in votes)
                {
                    if (vote.Length > 1)
                    {
                        Console.WriteLine("Wrong Input");
                        getVotes(words);
                    }
                    foreach (char c in vote)
                    {
                        if (c < '0' || c > '9')
                        {
                            Console.WriteLine("Input Only Integers");
                            getVotes(words);
                        }
                        else
                        {

                        }
                        vote1[w] = vote;
                    }
                    w++;
                }
                
                voted.Add(vote1);
            }
            ballet.Ballets = voted;

            List<string[]> winningCandidatesList = new List<string[]>();
            List<int[]> totalWinningCandidatesPerVoting = new List<int[]>();

           

            for (int i = 0; i < ballet.Ballets.Count();i++ )
            {
                winningCandidatesList.Add(getPreferencedCandidate(ballet.Ballets[i]));
                totalWinningCandidatesPerVoting = getNumberOfVotesPerCandidate(winningCandidatesList);       
            }

            int[] numberOfVotes = new int[Convert.ToInt32(words[0])];
            int[] index = new int[Convert.ToInt32(words[0])];
            for (int l = 0; l < totalWinningCandidatesPerVoting.Count(); l++)
            {
                int[] votesPerCandidate = new int[Convert.ToInt32(words[0])];

                for (int j = 0; j < totalWinningCandidatesPerVoting.Count(); j++)
                {
                    if (totalWinningCandidatesPerVoting[j][l] > 0)
                    {
                        votesPerCandidate[j] = totalWinningCandidatesPerVoting[j][l];
                    }
                    
                }
                int indexOfCandidate = votesPerCandidate.Sum();
                numberOfVotes[l] = indexOfCandidate;
            }

            int maxValue = numberOfVotes.Max();

            bool hasDuplicates = false;
            hasDuplicates = ContainsDuplicates(numberOfVotes);
            int maxIndex = numberOfVotes.ToList().IndexOf(maxValue);
            


            if (!hasDuplicates)
            {
                Console.WriteLine("Winner of Condorcet Election is Candidate {0} ", maxIndex);
            }
            else if (hasDuplicates)
            {
                Console.WriteLine("No Condorcet Winner");

            }
                return isCondorcetWinner;
        }

        //This is method to check the Condorcet Election Winner
        static public bool checkCondorcetWinner(string userInput)
        {
            bool isCondorcetWinner = false;

            string[] words = userInput.Split(' ');
            foreach (string word in words)
            {
                if (word.Length > 1)
                {
                    Console.WriteLine("Wrong Input");
                    checkCondorcetWinner(userInput);
                }
                foreach (char c in word)
                {
                    if (c < '0' || c > '9')
                    {
                        Console.WriteLine("Input Only Integers ");
                        checkCondorcetWinner(userInput);
                    }
                    else
                    {

                    }
                }               
            }

            if(Convert.ToInt32(words[0]) > 500)
            {
                Console.WriteLine("Cannot have more than 500 ballets");
                checkCondorcetWinner(userInput);
            }

            if (Convert.ToInt32(words[1]) > 2500)
            {
                Console.WriteLine("Cannot have more than 2500 candidates");
                checkCondorcetWinner(userInput);
            }

            isCondorcetWinner = getVotes(words);

            return isCondorcetWinner;
        }

        //This is method to start election
        static public void startElection()
        {
            Console.WriteLine("Enter the number of ballets followed by space and then the number of candidates");
            String userInput = Console.ReadLine();
            checkCondorcetWinner(userInput);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Condorcet Winners");

            startElection();

            Console.ReadKey();
        }

    }
}
