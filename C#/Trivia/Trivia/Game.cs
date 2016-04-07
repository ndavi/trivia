﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {


        List<Player> players = new List<Player>();


        bool[] inPenaltyBox = new bool[6];

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(createRockQuestion(i));
            }
        }

        public String createRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(string playerName)
        {
            Player player = new Player(playerName);
            players.Add(player);
            inPenaltyBox[howManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public void roll(int roll)
        {
            Console.WriteLine(players[currentPlayer].Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (inPenaltyBox[currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(players[currentPlayer].Name + " is getting out of the penalty box");
                    this.players[currentPlayer].Places += roll;
                    if (this.players[currentPlayer].Places > 11) this.players[currentPlayer].Places = this.players[currentPlayer].Places - 12;

                    Console.WriteLine(players[currentPlayer].Name
                            + "'s new location is "
                            + this.players[currentPlayer].Places);
                    Console.WriteLine("The category is " + currentCategory());
                    askQuestion();
                }
                else
                {
                    Console.WriteLine(players[currentPlayer].Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                this.players[currentPlayer].Places += roll;
                if (this.players[currentPlayer].Places > 11) this.players[currentPlayer].Places = this.players[currentPlayer].Places - 12;

                Console.WriteLine(players[currentPlayer].Name
                        + "'s new location is "
                        + this.players[currentPlayer].Places);
                Console.WriteLine("The category is " + currentCategory());
                askQuestion();
            }

        }

        private void askQuestion()
        {
            if (currentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (currentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (currentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (currentCategory() == "Rock")
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }


        private String currentCategory()
        {
            if (this.players[currentPlayer].Places == 0) return "Pop";
            if (this.players[currentPlayer].Places == 4) return "Pop";
            if (this.players[currentPlayer].Places == 8) return "Pop";
            if (this.players[currentPlayer].Places == 1) return "Science";
            if (this.players[currentPlayer].Places == 5) return "Science";
            if (this.players[currentPlayer].Places == 9) return "Science";
            if (this.players[currentPlayer].Places == 2) return "Sports";
            if (this.players[currentPlayer].Places == 6) return "Sports";
            if (this.players[currentPlayer].Places == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    this.players[currentPlayer].WinOnePurse();
                    Console.WriteLine(players[currentPlayer].Name
                                      + " now has "
                                      + this.players[currentPlayer].Purses
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                this.players[currentPlayer].WinOnePurse();
                Console.WriteLine(players[currentPlayer].Name
                        + " now has "
                        + this.players[currentPlayer].Purses
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer].Name + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }


        private bool didPlayerWin()
        {
            return this.players[currentPlayer].Purses != 6;
        }
    }

}
