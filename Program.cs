using System;
using System.Linq;
using System.Reflection;

namespace nvma
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandFromInput = args.Length == 0 ? "help" : args[0];

            if (string.IsNullOrWhiteSpace(commandFromInput)) {
                commandFromInput = "help";
            }

            var commandInterfaceType = typeof(ICustomCommand);
            var commandType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(
                    commandType =>
                        commandInterfaceType.IsAssignableFrom(commandType) &&
                        commandType.Name.Replace("Command", "").ToLower() == commandFromInput.ToLower()
                );

            if (commandType.Count() == 0)
            {
                new HelpCommand().Execute(args).Wait();

#if DEBUG
//easier to test
                Console.ReadKey();
#endif
                return;
            }

            var commandInstance = (ICustomCommand)Activator.CreateInstance(commandType.First());
            commandInstance.Execute(args).Wait();

#if DEBUG
//easier to test
            Console.ReadKey();
#endif
        }
    }
}
