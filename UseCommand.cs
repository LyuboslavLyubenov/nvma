using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;

namespace nvma
{
    /// <summary>
    /// Changes nodejs version to selected
    /// </summary>
    class UseCommand : CustomCommand
    {
        public async override Task Execute(string[] args)
        {
            System.Console.WriteLine("Changing your version to " + args[1]);

            try
            {
                if (!Directory.Exists($"node-{args[1]}-win-x64"))
                {
                    await DownloadAndSaveNodeVersion(args);
                }

                this.RemoveAllFromPath();
                var path = Path.Combine(this.ExecutableLocationDirectory, $"node-{args[1]}-win-x64");
                this.AddToPath(path);

                System.Console.WriteLine($"Your version is successfully changed to {args[1]}. Dont forget to restart your terminal/cmd in order to use this version.");
            }
            catch (HttpRequestException httpError) 
            {
                System.Console.WriteLine($"It looks like there was some problem with changing your version to {args[1]}.\n" + 
                $"Make sure that you are connected to the internet and are typing correct version. \n Error: {httpError}");
            }
            catch (System.Exception error)
            {
                System.Console.WriteLine($"It looks like there was some problem with changing your version to {args[1]}.\n Error: {error}");
            }
        }

        private async Task DownloadAndSaveNodeVersion(string[] args)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://nodejs.org/dist/{args[1].TrimEnd('/')}/node-{args[1]}-win-x64.zip");
            var bytes = await response.Content.ReadAsByteArrayAsync();

            var newDirectoryLocation = Path.Combine(this.ExecutableLocationDirectory, $"node-{args[1]}-win-x64");
            Directory.CreateDirectory(newDirectoryLocation);

            var nodejsZipLocation = Path.Combine(newDirectoryLocation, $"{args[1]}.zip");
            await File.WriteAllBytesAsync(nodejsZipLocation, bytes);
            ZipFile.ExtractToDirectory(Path.Combine(newDirectoryLocation, $"{args[1]}.zip"), this.ExecutableLocationDirectory, true);
        }

        public override string ToString()
        {
            return "Changes the version of your node instance. \n" +
                "\tUsage: nvma.exe use v14.9.0  \n" +
                "\tIf above if executed it will download nodejs version 14.9.0 and set it to your default";
        }
    }
}
