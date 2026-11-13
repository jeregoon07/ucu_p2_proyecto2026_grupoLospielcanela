using System;
using System.Threading.Tasks;
using Library; 
using Microsoft.Extensions.Configuration;

namespace Project
{
    public class Program
    {
        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }
        private static async Task MainAsync()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();
            string token = configuration["DiscordToken"];
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("No se encontró el DiscordToken en los secretos de usuario.");
                return;
            }
            // Aquí instanciamos la clase que acabamos de crear con el nombre que querías
            using (var bot = new DiscordUCUBot())
            {
                await bot.IniciarAsync(token).ConfigureAwait(false);
            }
            
            await Task.CompletedTask;
        }
    }
}