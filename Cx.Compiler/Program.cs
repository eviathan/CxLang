using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cx.Compiler.Exceptions;

namespace Cx.Compiler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var sourcePath = args.FirstOrDefault();

            if(!string.IsNullOrWhiteSpace(sourcePath)) 
                RunFile(sourcePath);
            else
                RunPrompt();
        }

        private static void RunPrompt()
        {
            Console.Clear();
            Console.WriteLine("Welcome to C𝄪 v0.0.1");
            Console.WriteLine("Type \"help\" for more information or \"quit\" to exit.");

            do
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("> ");
                    Console.ForegroundColor = ConsoleColor.White;

                    var line = Console.ReadLine();

                    if (line == "quit")
                        break;
                    else if(line == "clear")
                        Console.Clear();
                    else if(line == "help")
                        OpenDocumentation();
                    else if(string.IsNullOrWhiteSpace(line))
                        continue;

                    Lexer.Run(line);
                }
                catch(CodeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Fatal Exception:");
                    Console.WriteLine(ex.Message);
                }
            } 
            while(true);
        }

        private static void RunFile(string? sourcePath)
        {
            throw new NotImplementedException();
        }

        private static void OpenDocumentation()
        {
            Console.Clear();
            Console.WriteLine(new string('-', 80));
            Console.WriteLine("Command          Description");
            Console.WriteLine($"{new string('-', 80)}{Environment.NewLine}");

            Console.WriteLine("clear            Clears the current terminal");
            Console.WriteLine("quit             Exit current level of application");

            Console.WriteLine($"{Environment.NewLine}{new string('-', 80)}{Environment.NewLine}");
            Console.WriteLine("Type \"quit\" to exit help");
            Console.WriteLine($"{Environment.NewLine}");

            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("> ");
                Console.ForegroundColor = ConsoleColor.White;

                var line = Console.ReadLine();

                if (line == "quit")
                {
                    Console.Clear();
                    break;
                }
                else if(string.IsNullOrWhiteSpace(line))
                    continue;
            } 
            while(true);
        }
    }
}