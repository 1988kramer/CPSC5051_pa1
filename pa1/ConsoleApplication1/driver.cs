using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  class driver
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
        try
        {
          result = encrypt.encrypt(word);
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception.Message);
          continue;
        }
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
