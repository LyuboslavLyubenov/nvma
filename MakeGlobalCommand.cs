using System.IO;
using System.Threading.Tasks;

namespace nvma
{
    /// <summary>
    /// Makes nvma usable from any location (not just from nvma folder)
    /// </summary>
    class MakeGlobalCommand : CustomCommand
    {
        public override Task Execute(string[] args)
        {
            return Task.Run(() =>
            {
                if (this.DoesExistInPath(this.ExecutableLocationDirectory))
                {
                    System.Console.WriteLine("Global NVMA is enabled already and should be accessible globally.");
                    return;
                }

                this.AddToPath(this.ExecutableLocationDirectory);
                System.Console.WriteLine("You should be able to use nvma globally now! Please restart your cmd/terminal");
            });
        }

        public override string ToString()
        {
            return "Makes nvma globally available (from any cmd, terminal).";
        }
    }
}