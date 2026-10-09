//------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Project
{
    /// <summary>
    /// Programa principal.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Punto de entrada al programa principal.
        /// </summary>
        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            // Comentado temporalmente hasta implementar el bot
            /*
            using (var bot = new BotService())
            {
                string token = "TOKEN_AQUI"; 
                await bot.IniciarAsync(token).ConfigureAwait(false);
            }
            */
            await Task.CompletedTask;
        }
    }   
}
