//    Author: Andrew Kramer
// File name: encryptWord.cs
//      Date: 4/30/2016

// description goes here
// preconditions: constructor must be passed a Random object (ASSUMPTION!)
// postconditions:

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  public class encryptWord
  {
    private int shiftValue; // amount by which encrypted words are shifted
    private int maxGuesses; // max number of guesses allowed
    private List<int> guessHistory; // record of guessed shift values
    private bool isOn;
    private Random r;
    private const int defaultGuesses = 10;
    private const string validChars = "abcdefghijklmnopqrstuvwxyz /t/n";

    public encryptWord(Random r)
    {
      this.r = r;
      shiftValue = this.r.Next(0, 26);
      Console.WriteLine("Shift value = " + shiftValue);
      maxGuesses = defaultGuesses;
      guessHistory = new List<int>();
      isOn = true;
    }

    public encryptWord(Random r, int maxGuesses)
    {
      this.r = r;
      shiftValue = this.r.Next(0, 26);
      this.maxGuesses = maxGuesses;
      guessHistory = new List<int>();
      isOn = true;
    }

    // ASSUMES only alphabetic characters should be encoded
    // throws an ArgumentException
    // throws an exception if the object is off
    public string encrypt(string word)
    {
      if (!isOn)
        throw new Exception("Object is off, reset to try again.");
      if (word.Length < 4)
        throw new Exception("Given string must be 4 chars long or more.")
      string result = "";
      for (int i = 0; i < word.Length; i++)
      {
        int nextChar = (int)word[i];
        if (word[i] >= 65 && word[i] <= 122) // only shift if char is alphabetic
        {
          nextChar += shiftValue;
        }
        // if current char is upper case
        if (word[i] <= 90 && nextChar > 90)
        {
          nextChar = ((nextChar - 65) % 26) + 65;
        }
        // if current char is lower case
        if (word[i] >= 97 && nextChar > 122)
        {
          nextChar = ((nextChar - 97) % 26) + 97;
        }
        result += (char)nextChar;
      }
      return result;
    }
    
    // throws an exception if the object is off
    public void guess(int guess)
    {
      if (!isOn)
        throw new Exception("Object is off, reset to try again.");
      if (guess == shiftValue)
      {
        Console.WriteLine("Your guess was correct!");
        printStats();
        isOn = false;
        Console.WriteLine("Reset to try again");
      }
      else
      {
        guessHistory.Add(guess);
        if (guess < shiftValue)
          Console.WriteLine("Sorry, your guess was too low.");
        else
          Console.WriteLine("Sorry, your guess was too high.");
        if (guessHistory.Count() == maxGuesses)
        {
          Console.WriteLine("You have reached the maximum number of guesses.");
          Console.WriteLine("Reset to try again.");
          isOn = false;
          printStats();
        }
      }
    }

    public void reset()
    {
      Console.WriteLine("Shift value is reset and guess"
                        + "history has been cleared.");
      shiftValue = r.Next(0, 26);
      guessHistory.Clear();
      isOn = true;
    }

    public bool getIsOn()
    {
      return isOn;
    }

    // ASSUMES stats are only printed after a correct guess
    private void printStats()
    {
      int min = 0, max = 0;
      double mean = 0.0;
      foreach (int n in guessHistory)
      {
        if (n < min) min = n;
        if (n > max) max = n;
        mean += (double)n;
      }
      mean /= (double)guessHistory.Count();
      Console.WriteLine("Guess statistics:");
      Console.WriteLine("Minimum  -  " + min);
      Console.WriteLine("Maximum  -  " + max);
      Console.WriteLine("Mean     -  " + String.Format("{0:0.00}", mean));
    }
  }

  public class Program
  {
    public static void Main()
    {
      Random r = new Random();
      encryptWord encrypt = new encryptWord(r, 2);
      string word, result;
      while (encrypt.getIsOn())
      {
        Console.Write("Enter a word to encrypt: ");
        word = Console.ReadLine();
        result = encrypt.encrypt(word);
        Console.WriteLine("Encrypted word:  " + result);
        Console.Write("\nGuess shift value? (enter -1 for no guess)  ");
        int shift;
        if (!int.TryParse(Console.ReadLine(), out shift))
        {
          Console.WriteLine("Your guess was not a number.");
          continue;
        }
        Console.WriteLine();
        if (shift >= 0)
        {
          try
          {
            encrypt.guess(shift);
          }
          catch (Exception exception)
          {
            Console.WriteLine(exception.Message);
            continue;
          }
        }
      }
      Console.WriteLine("Press enter to close:");
      Console.ReadLine();
    }
  }
}
