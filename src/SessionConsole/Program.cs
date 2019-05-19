using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuxaforSharp;

namespace SessionConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = new WindowsSession())
            {
                PrintMenu();
                string input;

                do
                {
                    Console.Write("> ");
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "focus":
                        case "f":
                            session.SetIsFocussed(true);
                            break;

                        case "unfocus":
                        case "uf":
                            session.SetIsFocussed(false);
                            break;

                        case "standup":
                        case "s":
                            session.Standup();
                            break;

                        default:
                            Console.WriteLine("I don't understand");
                            break;
                    }
                }
                while (input != "q");
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("Options are: \"focus\" (f), \"unfocus\" (uf), \"standup\" (s) and \"q\"");
            Console.WriteLine();
        }
    }
}
