using System;
using System.Collections.Generic;

interface Operation
{
    string Perform();
}

class SumOperation : Operation
{
    public string Perform()
    {
        int a = int.Parse(Console.ReadLine());
        int b = int.Parse(Console.ReadLine());
        return (a + b).ToString();
    }
}

class DifOperation : Operation
{
    public string Perform()
    {
        int a = int.Parse(Console.ReadLine());
        int b = int.Parse(Console.ReadLine());
        return (a - b).ToString();
    }
}

internal class Program
{
    static bool runs = true;

    private static void Main(string[] args)
    {
        var commands = new Dictionary<string, Operation>
        {
            { "sum", new SumOperation() },
            { "dif", new DifOperation() }
        };

        while (runs)
        {
            string command = Console.ReadLine();
            if (command == "exit")
            {
                runs = false;
            }
            else if (!string.IsNullOrEmpty(command) && commands.ContainsKey(command))
            {
                Console.WriteLine(commands[command].Perform());
            }
            else
            {
                Console.WriteLine("Unknown command");
            }
        }
    }
}
