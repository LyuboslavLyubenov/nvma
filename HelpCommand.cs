
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace nvm_without_admin_rights
{
    /// <summary>
    /// Command that returns all commands
    /// </summary>
    class HelpCommand : CustomCommand
    {
        public override Task Execute(string[] args)
        {
            return Task.Run(() =>
            {
                var commandInterfaceType = typeof(CustomCommand);
                var availableCommands = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(
                        commandType =>
                            commandInterfaceType.IsAssignableFrom(commandType) && !commandType.IsAbstract
                    ).Select(command => command.Name.Replace("Command", "").ToLower() + ": " + ((ICustomCommand)Activator.CreateInstance(command)).ToString());
                Console.WriteLine(string.Join('\n', availableCommands));
            });
        }

        public override string ToString()
        {
            return "Returns all possible commands and their usages";
        }
    }
}