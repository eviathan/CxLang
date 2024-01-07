using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cx.Compiler.Exceptions;
using Cx.Compiler.Interfaces;

namespace Cx.Compiler
{
    public class Interpreter
    {
        private List<string> _commandHistory = new List<string>();
        private int _historyIndex = -1;

        private const int TAB_SIZE = 4;

        public void RunPrompt()
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
                    var command = Console.IsInputRedirected
                        ? Console.ReadLine()
                        : ReadLineWithHistory();

                    if (command == "quit")
                        break;
                    else if(command == "clear")
                        Console.Clear();
                    else if(command == "help")
                        OpenDocumentation();
                    else if(string.IsNullOrWhiteSpace(command))
                        continue;

                    _commandHistory.Add(command);
                    _historyIndex = -1;

                    var lexer = new Lexer(command);
                    var tokens = lexer.Tokenize();

                    tokens.ForEach(Console.WriteLine);
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

        public void RunFile(string? sourcePath)
        {
            throw new NotImplementedException();
        }

        private void OpenDocumentation()
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

        // TODO: Rerwrite this mess
        private string ReadLineWithHistory()
        {
            var input = new System.Text.StringBuilder();
            ConsoleKeyInfo keyInfo;
            int cursorIndex = 0;

            void RefreshInputLine()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\r> ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(input);
                Console.Write(new string(' ', Console.WindowWidth - input.Length - 2));
                Console.SetCursorPosition(cursorIndex + 2, Console.CursorTop);
            }

            do
            {
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (_commandHistory.Count > 0 && _historyIndex < _commandHistory.Count - 1)
                        {
                            _historyIndex++;
                            input.Clear();
                            input.Append(_commandHistory[_commandHistory.Count - 1 - _historyIndex]);
                            cursorIndex = input.Length;
                            RefreshInputLine();
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (_historyIndex > 0)
                        {
                            _historyIndex--;
                            input.Clear();
                            input.Append(_commandHistory[_commandHistory.Count - 1 - _historyIndex]);
                            cursorIndex = input.Length;
                            RefreshInputLine();
                        }
                        else if (_historyIndex == 0)
                        {
                            _historyIndex--;
                            input.Clear();
                            cursorIndex = 0;
                            RefreshInputLine();
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (cursorIndex > 0)
                        {
                            cursorIndex--;
                            RefreshInputLine();
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (cursorIndex < input.Length)
                        {
                            cursorIndex++;
                            RefreshInputLine();
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        return input.ToString();
                    case ConsoleKey.Backspace:
                        if (input.Length > 0 && cursorIndex > 0)
                        {
                            input.Remove(cursorIndex - 1, 1);
                            cursorIndex--;
                            RefreshInputLine();
                        }
                        break;
                    default:
                        if (keyInfo.Key == ConsoleKey.Tab)
                        {
                            var tabSpaces = new string(' ', TAB_SIZE);
                            input.Insert(cursorIndex, tabSpaces);
                            cursorIndex += TAB_SIZE;
                            RefreshInputLine();
                        }
                        else if (!char.IsControl(keyInfo.KeyChar))
                        {
                            // Handle other characters
                            input.Insert(cursorIndex, keyInfo.KeyChar);
                            cursorIndex++;
                            RefreshInputLine();
                        }
                        break;
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);

            return input.ToString();
        }
    }
}