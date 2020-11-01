using System.Threading.Tasks;

/// <summary>
/// Interface that all commands inherit
/// </summary>
internal interface ICustomCommand
{
    Task Execute(string[] args);

    string ToString();
}