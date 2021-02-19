using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace nvma
{
    /// <summary>
    /// Asbtract class that must be inherited from all commands.
    /// </summary>
    public abstract class CustomCommand : ICustomCommand
    {
        private string executableLocationDirectory = "";
        protected string ExecutableLocationDirectory
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.executableLocationDirectory))
                {
                    return this.executableLocationDirectory;
                }
                this.executableLocationDirectory = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                this.executableLocationDirectory = Path.GetDirectoryName(this.executableLocationDirectory);
                return this.executableLocationDirectory;
            }
            set
            {

            }
        }

        public abstract Task Execute(string[] args);

        /// <summary>
        /// Adds value to User environment PATH variable
        /// </summary>
        /// <param name="value">value thats going to be added to the PATH variable</param>
        protected void AddToPath(string value)
        {
            var name = "PATH";
            var scope = EnvironmentVariableTarget.User;
            var oldValue = Environment.GetEnvironmentVariable(name, scope);
            oldValue += ";" + value;
            Environment.SetEnvironmentVariable(name, oldValue, scope);
        }
        /// <summary>
        /// Check if some path is stored into user PATH variable
        /// </summary>
        /// <param name="value">value to check agains</param>
        /// <returns>Returns true if it is defined in to the PATH variable, false if it does not</returns>
        protected bool DoesExistInPath(string value)
        {
            var name = "PATH";
            var scope = EnvironmentVariableTarget.User;
            var pathValue = Environment.GetEnvironmentVariable(name, scope);
            return pathValue.Split(';').Contains(value);
        }

        /// <summary>
        /// Removes all values that contain current directory from User Path environment variable
        /// </summary>
        protected void RemoveAllFromPath()
        {
            var name = "PATH";
            var scope = EnvironmentVariableTarget.User;
            var oldValue = Environment.GetEnvironmentVariable(name, scope);
            var paths = oldValue.Split(';');
            var currentPath = this.ExecutableLocationDirectory;
            var newPaths = paths.Where(path => !path.Contains(currentPath)).ToArray();
            var newValue = string.Join(';', newPaths);
            Environment.SetEnvironmentVariable(name, newValue, scope);
        }

        /// <summary>
        /// Implemented by concrete class. Says what is this command used for
        /// </summary>
        /// <returns></returns>
        public override abstract string ToString();
    }
}
