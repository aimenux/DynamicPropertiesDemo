using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App
{
    public static class Program
    {
        private static async Task Main()
        {
            var json = await File.ReadAllTextAsync("Files\\file1.json");
            var userProfile1 = JsonConvert.DeserializeObject<Lib.Models.V1.UserProfile>(json);
            Console.WriteLine($"V1 -> {userProfile1["email"]}");
            var userProfile2 = JsonConvert.DeserializeObject<Lib.Models.V2.UserProfile>(json);
            Console.WriteLine($"V2 -> {userProfile2["email"]}");
            var userProfile3 = JsonConvert.DeserializeObject<Lib.Models.V3.UserProfile>(json);
            Console.WriteLine($"V3 -> {userProfile3["email"]}");
            Console.WriteLine("Press any key to exit !");
            Console.ReadKey();
        }
    }
}
