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
    private const int defaultGuesses = 10;
    private const string validChars = "abcdefghijklmnopqrstuvwxyz /t/n";

    public encryptWord(Random r)
    {
      shiftValue = r.Next(0, 26);
      maxGuesses = defaultGuesses;
      guessHistory = new List<int>();
      isOn = true;
    }

    public encryptWord(Random r, int maxGuesses)
    {
      shiftValue = r.Next(0, 26);
      this.maxGuesses = maxGuesses;
      guessHistory = new List<int>();
      isOn = true;
    }

    // ASSUMES only alphabetic and whitespace characters are valid
    // throws an ArgumentException
    // throws an exception if the object is off
    public string encrypt(string word)
    {
      if (!isOn)
        throw new Exception("Object is off, reset to try again.");
      string result = "";
      for (int i = 0; i < word.Length; i++)
      {
        if (!validChars.Contains(Char.ToLower(word[i])))
          throw new ArgumentException("String contains a non-alphabetic " +
                                      "or non whitespace character.");
        int nextChar = (int)word[i] + shiftValue;
        // if current char is upper case
        if ((word[i] >= 65 || word[i] <= 90) && nextChar > 90)
        {
          nextChar = ((nextChar - 65) % 26) + 65;
        }
        // if current char is lower case
        if ((word[i] >= 97 || word[i] <= 122) && nextChar > 122)
        {
          nextChar = ((nextChar - 97) % 26) + 97;
        }
        result += nextChar;
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
        Console.WriteLine("Sorry, your guess was incorrect.");
        if (guessHistory.Count() == maxGuesses)
        {
          Console.WriteLine("You have reached the maximum number of guesses.");
          Console.WriteLine("Reset to try again.");
          isOn = false;
          printStats();
        }
      }
    }

    // ASSUMES stats are only printed after a correct guess
    private void printStats()
    {
      int min = 0, max = 0;
      double mean = 0.0;
      foreach (int n in guessHistory)
      {
        // left off here
        // finish later
      }
    }
  }
}
