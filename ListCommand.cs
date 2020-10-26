using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace nvm_without_admin_rights
{
    class ListCommand : CustomCommand
    {
        public override async Task Execute(string[] args)
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://nodejs.org/dist/");
            var source = await response.Content.ReadAsStringAsync();
            var regex = new Regex("href=\"([A-Za-z0-9.-/]+)\"");
            var matches = regex.Matches(source);
            var versions = matches.Select(match => match.Groups[1].Value).Where(match => match.StartsWith("v"));

            Console.WriteLine(string.Join('\n', versions));
        }

        public override string ToString()
        {
            return "Gets all node js versions";
        }
    }
}