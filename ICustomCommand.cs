using System.Threading.Tasks;

internal interface ICustomCommand
{
    Task Execute(string[] args);

    string ToString();
}