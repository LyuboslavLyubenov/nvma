using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace nvm_without_admin_rights
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandInterfaceType = typeof(ICustomCommand);
            var commandType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(
                    commandType =>
                        commandInterfaceType.IsAssignableFrom(commandType) &&
                        commandType.Name.Replace("Command", "").ToLower() == args[0].ToLower()
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
