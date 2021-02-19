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
            if (!Directory.Exists($"node-{args[1]}-win-x64"))
            {
                await DownloadAndSaveNodeVersion(args);
            }

            this.RemoveAllFromPath();
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"node-{args[1]}-win-x64");
            this.AddToPath(path);
        }

        private async Task DownloadAndSaveNodeVersion(string[] args)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://nodejs.org/dist/{args[1]}/node-{args[1]}-win-x64.zip");
            var bytes = await response.Content.ReadAsByteArrayAsync();

            var directoryName = Directory.CreateDirectory($"node-{args[1]}-win-x64").FullName;
            await File.WriteAllBytesAsync($"{directoryName}/{args[1]}.zip", bytes);
            ZipFile.ExtractToDirectory($"{directoryName}/{args[1]}.zip", Directory.GetCurrentDirectory());
        }

        public override string ToString()
        {
            return "Changes the version of your node instance. \n" +
                "   Usage: nvma.exe use v14.9.0  \n" +
                "   If above if executed it will download nodejs version 14.9.0 and set it to your default";
        }
    }
}
