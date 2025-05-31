using System;
using System.Collections.Generic;

namespace Delegates_and_Exit_Command
{
    class Program
    {
        static bool runs = true;

        static void Main(string[] args)
        {
            InputProvider input = () => Console.ReadLine();
            OnResult result = (s) => Console.WriteLine(s);

            var commands = new Dictionary<string, Operation>
            {
                { "sum", new SumOperation(input, result) },
                { "dif", new DifOperation(input, result) },
                { "exit", new ExitOperation(() => runs = false, result) }
            };

            while (runs)
            {
                Console.Write("Enter command (sum/dif/exit): ");
                string command = Console.ReadLine();

                if (!string.IsNullOrEmpty(command) && commands.ContainsKey(command))
                {
                    commands[command].Perform();
                }
                else
                {
                    Console.WriteLine("Unknown command");
                }
            }
        }
    }

    public delegate string InputProvider();
    public delegate void OnResult(string result);

    interface Operation
    {
        void Perform();
    }

    class SumOperation : Operation
    {
        private InputProvider input;
        private OnResult onResult;

        public SumOperation(InputProvider input, OnResult onResult)
        {
            this.input = input;
            this.onResult = onResult;
        }

        public void Perform()
        {
            int a = int.Parse(input());
            int b = int.Parse(input());
            onResult((a + b).ToString());
        }
    }

    class DifOperation : Operation
    {
        private InputProvider input;
        private OnResult onResult;

        public DifOperation(InputProvider input, OnResult onResult)
        {
            this.input = input;
            this.onResult = onResult;
        }

        public void Perform()
        {
            int a = int.Parse(input());
            int b = int.Parse(input());
            onResult((a - b).ToString());
        }
    }

    class ExitOperation : Operation
    {
        private Action onExit;
        private OnResult onResult;

        public ExitOperation(Action exitAction, OnResult resultAction)
        {
            this.onExit = exitAction;
            this.onResult = resultAction;
        }

        public void Perform()
        {
            onExit();
            onResult("Program end");
        }
    }
}
