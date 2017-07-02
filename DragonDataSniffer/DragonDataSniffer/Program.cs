using System;

namespace DragonDataSniffer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (!Parser.Load())
            {
                Log.WriteLine(LogLevel.Error, "Tunnel could not be started. Errors occured.");
                Console.ReadLine();
            }
        }
    }
}