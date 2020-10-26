using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public abstract class CustomCommand : ICustomCommand {
    public abstract Task Execute(string[] args);

    protected void AddToPath(string value) {
        var name = "PATH";
        var scope = EnvironmentVariableTarget.User;
        var oldValue = Environment.GetEnvironmentVariable(name, scope);
        oldValue += ";" + value;
        Environment.SetEnvironmentVariable(name, oldValue, scope);
    }

    protected void RemoveAllFromPath() {
        var name = "PATH";
        var scope = EnvironmentVariableTarget.User;
        var oldValue = Environment.GetEnvironmentVariable(name, scope);
        var paths = oldValue.Split(';');
        var currentPath = Directory.GetCurrentDirectory();
        var newPaths = paths.Where(path => !path.Contains(currentPath)).ToArray();
        var newValue = string.Join(';', newPaths);
        Environment.SetEnvironmentVariable(name, newValue, scope);
    }

    public override abstract string ToString();
}