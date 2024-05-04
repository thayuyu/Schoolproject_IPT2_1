using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;




namespace ProgramWIthoutUI
{ 
  internal class Program
  {
    static void Main(string[] args)
    {

      while (true)
      {
        string input = "?";
        do
        {
          Console.Clear();
          Intro();
          input = Console.ReadLine().ToLower();
        } while (input != "e" && input != "");
        
        if (input == "e")
          break;

        Console.Clear();
        int ResPlayer = Gameplay();

        Console.Clear();
        int ResBot = BotGameplay();

        Console.Clear();
        Result(ResPlayer, ResBot);

        Console.Clear();
      }

      // here is the game eneded
    }


    // Introduction
    static public void Intro()
    {
      Console.WriteLine("Welcome to our own BlackJack!");
      Console.WriteLine("-----------------------------");
      Console.WriteLine("Here are the Controls:\n[Press 'enter' for a new card]\n[Press 'e' to stop the game]");
      Console.WriteLine("If you want to start a game press 'enter'");
    }

    /// <summary>
    /// Checking if the total is higher/lower than 21 
    /// </summary>
    /// <param name="total"></param>
    /// <param name="player"></param>
    /// <returns>true if over 21</returns>
    static public bool Is21(int total, bool player)
    {
      if (total > 21)
      {
        if (player)
        {
          Console.WriteLine("\nYou went too high!");
          Thread.Sleep(3000);
          return true;

        }
        else if (!player)
        {
          Console.WriteLine("The bot went too high!\n");
        }
      }
      else if (total == 21)
      {
        if (player)
        {
          Console.WriteLine("\n<<You have BlackJack>>\n");
          Thread.Sleep(1500);
          return true;
        }
        else if (!player)
        {
          Console.WriteLine("\n<<The bot has BlackJack>>\n");
        }
      }
      return false;
    }

    // Output if you won
    static public string won()
    {
      return "You won this game";
    }

    // Output if you lost
    static public string lost()
    {
      return "You lost this one, better luck next time!";
    }

    // Output if you draw
    static public string draw()
    {
      return "It's a draw! You and the bot had the same total";
    }

    // Gameplay
    static public int Gameplay()
    {
      int total = 0;
      int cardnum = 0;
      int cardval = 0;
      string input = "?";
      bool player = true;
      int cardcol = 0;
      Random R1 = new Random();
      Random R2 = new Random();
      while (total < 22)
      {
        cardval = Card(R1.Next(1,13), total);
        cardcol = R2.Next(1, 4);

        cardnum++;
        total += cardval;

        Console.WriteLine("Card Number {0}:\t {1}", cardnum, cardval);
        Console.WriteLine("Card Number {0} color:\t {1}",cardnum, cardcol);
        Console.WriteLine("Total: {0}", total);

        if (Is21(total, player) == true) 
        {
          break;
        }

        if (total < 22)
        {
          do
          {
            Console.WriteLine("\nDo you want another card?\n[enter = new card]\n[e = stop and let bot play]");
            
            input = Console.ReadLine().ToLower();
          } while (input != "" && input != "e");

          if (input == "e")
          {
            break;
          }
          else
            continue;
        }
      }

      return total;
    }


  

    // Bot gameplay
    static public int BotGameplay()
    {
      int BotTotal = 0;
      int cardnum = 0;
      int cardval = 0;
      bool player = false;
      int cardcol = 0;
      Random R2 = new Random();

      while (BotTotal < 18)
      {
        cardval = Card(R2.Next(1,13), BotTotal);
        cardcol = R2.Next(1, 4);

        cardnum++;
        BotTotal += cardval;

        Console.WriteLine("Bot Card Number {0}:\t {1}", cardnum, cardval);
        Console.WriteLine("Card Number {0} color:\t {1}", cardnum, cardcol);
        Console.WriteLine("Bot Total: {0}", BotTotal);

        Is21(BotTotal, player);

        Thread.Sleep(1500);
      }
      Console.WriteLine("The bot's round is over\nResults are being calculated...");
      Thread.Sleep(2000);
      return BotTotal;
    }

    /// <summary>
    /// recieving a card through a randomizer 
    /// </summary>
    /// <param name="RecievedCard"></param>
    /// <param name="RecievedTotal"></param>
    /// <returns>real value of a card</returns>
    static public int Card(int RecievedCard, int RecievedTotal)
    {
      int cardvalue = 0;

      if (RecievedCard < 11 && RecievedCard > 1)
      {
        cardvalue = RecievedCard;
      }

      else if (RecievedCard >= 11 && RecievedCard <= 13)
      {
        cardvalue = 10;
      }

      else if (RecievedCard == 1)
      {
        if (RecievedTotal <= 10)
          cardvalue = 11;
        else
          cardvalue = 1;
      }

      return cardvalue;
    }

    /// <summary>
    /// Getting result of a game
    /// </summary>
    /// <param name="PlayerTotal"></param>
    /// <param name="BotTotal"></param>
    /// <retunrs>the result of the game</retunrs>
    static public void Result(int PlayerTotal, int BotTotal)
    {

      if (PlayerTotal == 29)
      {
        Console.WriteLine("Your total: {0}\nBot's total: {1}", PlayerTotal, BotTotal);
        Console.WriteLine("You actually lost but you were so brave that we made you win, congrats");
        Thread.Sleep(1500);
      }
      else if (PlayerTotal < 22 && BotTotal < 22)
      {
        if (PlayerTotal > BotTotal)
        {
          Console.WriteLine("Your total: {0}\nBot's total: {1}", PlayerTotal, BotTotal);
          Console.WriteLine(won());
          Thread.Sleep(1500);
        }
        else if (PlayerTotal == BotTotal)
        {
          Console.WriteLine("Your total: {0}\nBot's total: {1}", PlayerTotal, BotTotal);
          Console.WriteLine(draw());
          Thread.Sleep(1500);
        }
        else if (PlayerTotal < BotTotal)
        {
          Console.WriteLine("Your total: {0}\nBot's total: {1}", PlayerTotal, BotTotal);
          Console.WriteLine(lost());
          Thread.Sleep(1500);
        }
      }
      else if (PlayerTotal > 21 && BotTotal < 22)
      {
        Console.WriteLine("Your total: {0}\nBot's total: {1}", PlayerTotal, BotTotal);
        Console.WriteLine(lost());
        Thread.Sleep(1500);
      }
      else if (BotTotal > 21 && PlayerTotal < 22)
      {
        Console.WriteLine("Your total: {0}\nBot's total: {1}", PlayerTotal, BotTotal);
        Console.WriteLine(won());
        Thread.Sleep(1500);
      }
      else if (PlayerTotal > 21 && BotTotal > 21)
      {
        Console.WriteLine("Your total: {0}\nBot's total: {1}", PlayerTotal, BotTotal);
        Console.WriteLine("How are you both this bad?");
        Thread.Sleep(1500);
      }
    }
  }
}