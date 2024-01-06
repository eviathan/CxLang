
namespace Cx.Compiler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var interpreter = new Interpreter();
            var sourcePath = args.FirstOrDefault();

            if(!string.IsNullOrWhiteSpace(sourcePath)) 
                interpreter.RunFile(sourcePath);
            else
                interpreter.RunPrompt();
        }
    }
}