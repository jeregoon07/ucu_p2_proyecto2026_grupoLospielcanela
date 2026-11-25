using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Library;

namespace Project
{

    public class DiscordUCUBot : IDisposable
    {

        private DiscordSocketClient _client;

        private SistemaFachada _fachada;

        public DiscordUCUBot()
        {
            var catalogo = new Catalogo();
            var motor = new MotorRecomendacion();
            _fachada = new SistemaFachada(catalogo, motor);
        }
        public async Task IniciarAsync(string token)
        {
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent | GatewayIntents.Guilds
            };
            _client = new DiscordSocketClient(config);
            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"{_client.CurrentUser} está conectado y listo en Discord.");
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(SocketMessage message)
        {
            if (message.Author.Id == _client.CurrentUser.Id) return;
            string contenido = message.Content.Trim();
            if (!contenido.StartsWith("!")) return;
            string usuarioId = message.Author.Id.ToString();
            string nombreUsuario = message.Author.Username;
            string[] partes = contenido.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            string comando = partes[0].ToLowerInvariant();
            string args = partes.Length > 1 ? partes[1].Trim() : string.Empty;
            try
            {
                switch (comando)
                {

                case "!ayuda":
                    await message.Channel.SendMessageAsync(
                    "Comandos disponibles:\n" +
                    "• !unirse - Saluda y prueba la fachada\n" +
                    "• !cargar_demo - Carga 5 canciones de prueba\n" +
                    "• !registrar - Crea tu cuenta en el sistema (HU1)\n" +
                    "• !catalogo - Muestra las canciones disponibles\n" +
                    "• !preferencias pop, rock - Define tus gustos (HU2)\n" +
                    "• !consumir <id> [1-5] - Registra escucha y valoracion (HU10, HU11)\n" +
                    "• !recomendar - Obtiene recomendaciones personalizadas (HU3-HU7)\n" +
                    "• !historial - Muestra tu historial de consumo (HU12)\n" +
                    "• !guardar <id> - Guarda una cancion para mas tarde (HU14)\n" +
                    "• !guardados - Muestra tu lista de guardados (HU14)\n" +
                    "• !agregar <id> | <nombre> | <attr1, attr2> - Agrega una cancion\n" +
                    "• !eliminar <id> - Elimina una cancion del catalogo (HU16)\n" +
                    "• !relacionados <id> - Muestra contenido similar a una cancion (HU15)\n" +
                    "• !excluir <atributo> - Excluye caracteristicas de las recomendaciones (HU8)\n" +
                    "• !ordenar <score|nombre> - Cambia el orden de las recomendaciones (HU9)\n"
                    );
                    break;

                case "!unirse":
                    await message.Channel.SendMessageAsync($"Hola {nombreUsuario}! Te has comunicado con la fachada del proyecto.");
                    break;

                case "!cargar_demo":
                    var demo = new[]
                    {
                        new Cancion("1", "Love in the Dark", new[] { "pop", "soul", "adele", "balada" }),
                        new Cancion("2", "Headlock", new[] { "electronica", "pop", "imogen heap", "indie" }),
                        new Cancion("3", "Degenere", new[] { "urbano", "reggaeton", "myke towers", "latino" }),
                        new Cancion("4", "Natural", new[] { "rock", "pop rock", "imagine dragons" }),
                        new Cancion("5", "Viva la Vida", new[] { "rock", "pop rock", "coldplay", "indie" })
                    };
                    int agregadas = demo.Count(cancion => _fachada.IntentarAgregarItemAlCatalogo(cancion));
                    await message.Channel.SendMessageAsync($"Se agregaron {agregadas} de las 5 canciones de la demo. Usa !catalogo para ver el catálogo.");
                    break;

                case "!registrar":
                    _fachada.RegistrarUsuario(usuarioId, nombreUsuario);
                    await message.Channel.SendMessageAsync($"Bienvenido {nombreUsuario}! Tu cuenta quedo registrada.");
                    break;

                case "!catalogo":
                    var items = _fachada.ObtenerCatalogo();
                    if (!items.Any())
                    {
                        await message.Channel.SendMessageAsync("El catalogo esta vacio. Usa !cargar_demo o !agregar.");
                        break;
                    }
                    var listaCat = items.Select(i => $"• [{i.Id}] {i.Nombre} ({string.Join(", ", i.Atributos)})");
                    await message.Channel.SendMessageAsync("Catalogo disponible:\n" + string.Join("\n", listaCat));
                    break;

                case "!preferencias":
                    var attrs = args.Split(',')
                        .Select(a => a.Trim().ToLowerInvariant())
                        .Where(a => a.Length > 0)
                        .Distinct()
                        .ToArray();
                    if (attrs.Length == 0)
                    {
                        await message.Channel.SendMessageAsync("Uso correcto: !preferencias pop, rock, indie");
                        break;
                    }
                    _fachada.ActualizarPreferencias(usuarioId, new Preferencia(attrs));
                    await message.Channel.SendMessageAsync($"Preferencias actualizadas: {string.Join(", ", attrs)}.");
                    break;

                case "!consumir":
                    var datosConsumo = args.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    if (datosConsumo.Length < 1 || datosConsumo.Length > 2)
                    {
                        await message.Channel.SendMessageAsync("Uso correcto: !consumir <id> [valoración]");
                        break;
                    }
                    string itemId = ValidarId(datosConsumo[0]);
                    int? valoracion = null;
                    if (datosConsumo.Length == 2)
                    {
                        if (!int.TryParse(datosConsumo[1], NumberStyles.None, CultureInfo.InvariantCulture, out int puntaje)
                            || puntaje < 1 || puntaje > 5)
                        {
                            await message.Channel.SendMessageAsync("La valoración debe ser un entero del 1 al 5.");
                            break;
                        }
                        valoracion = puntaje;
                    }
                    _fachada.RegistrarInteraccion(usuarioId, itemId, valoracion);
                    string detalle = valoracion.HasValue ? $" con valoración {valoracion}/5" : "";
                    await message.Channel.SendMessageAsync($"Consumo registrado para el ítem {itemId}{detalle}.");
                    break;

                case "!recomendar":
                    var recs = _fachada.ObtenerRecomendaciones(usuarioId);
                    if (recs == null || !recs.Any())
                    {
                        await message.Channel.SendMessageAsync("No hay recomendaciones disponibles en este momento.");
                        break;
                    }
                    var listaRecs = recs.Select((r, idx) => $"{idx + 1}. [{r.Id}] {r.Nombre} ({string.Join(", ", r.Atributos)})");
                    await message.Channel.SendMessageAsync("Recomendaciones para vos:\n" + string.Join("\n", listaRecs));
                    break;

                case "!historial":
                    var historial = _fachada.ObtenerHistorialConsumidos(usuarioId);
                    if (!historial.Any())
                    {
                        await message.Channel.SendMessageAsync("Todavia no tienes canciones en tu historial.");
                        break;
                    }
                    var listaHist = historial.Select(h => $"• [{h.Id}] {h.Nombre}");
                    await message.Channel.SendMessageAsync("Tu historial de consumo:\n" + string.Join("\n", listaHist));
                    break;

                case "!guardar":
                    string idGuardar = ValidarId(args);
                    bool guardado = _fachada.GuardarParaMasTarde(usuarioId, idGuardar);
                    await message.Channel.SendMessageAsync(guardado
                        ? $"Ítem {idGuardar} guardado para más tarde."
                        : $"El ítem {idGuardar} ya estaba guardado.");
                    break;

                case "!guardados":
                    var guardados = _fachada.ObtenerGuardados(usuarioId);
                    if (!guardados.Any())
                    {
                        await message.Channel.SendMessageAsync("Tu lista de guardados esta vacia.");
                        break;
                    }
                    var listaGuard = guardados.Select(g => $"• [{g.Id}] {g.Nombre} ({string.Join(", ", g.Atributos)})");
                    await message.Channel.SendMessageAsync("Guardados para mas tarde:\n" + string.Join("\n", listaGuard));
                    break;

                case "!agregar":
                    var datosItem = args.Split('|');
                    if (datosItem.Length != 3)
                    {
                        await message.Channel.SendMessageAsync("Uso correcto: !agregar <id> | <nombre> | <atributo1, atributo2>");
                        break;
                    }
                    string nuevoId = ValidarId(datosItem[0]);
                    string nuevoNombre = datosItem[1].Trim();
                    var nuevosAttrs = datosItem[2].Split(',')
                        .Select(a => a.Trim().ToLowerInvariant())
                        .Distinct()
                        .ToArray();
                    if (nuevoNombre.Length == 0 || nuevosAttrs.Any(a => a.Length == 0))
                    {
                        await message.Channel.SendMessageAsync("Ingresá un nombre y completá todos los atributos separados por comas.");
                        break;
                    }
                    _fachada.AgregarItemAlCatalogo(new Cancion(nuevoId, nuevoNombre, nuevosAttrs));
                    await message.Channel.SendMessageAsync($"Canción {nuevoNombre} agregada al catálogo con ID {nuevoId}.");
                    break;

                case "!eliminar":
                    string idEliminar = ValidarId(args);
                    _fachada.EliminarItemDelCatalogo(idEliminar);
                    await message.Channel.SendMessageAsync($"Ítem {idEliminar} eliminado del catálogo. Las entradas anteriores se conservan en el historial y los guardados.");
                    break;

                case "!relacionados":
                    var relacionados = _fachada.ObtenerRelacionados(ValidarId(args));
                    if (!relacionados.Any())
                    {
                        await message.Channel.SendMessageAsync("No se encontraron elementos relacionados a ese ID.");
                        break;
                    }
                    var listaRel = relacionados.Select((r, idx) => $"{idx + 1}. [{r.Id}] {r.Nombre} ({string.Join(", ", r.Atributos)})");
                    await message.Channel.SendMessageAsync("Contenido relacionado:\n" + string.Join("\n", listaRel));
                    break;

                case "!excluir":
                    _fachada.ExcluirAtributo(usuarioId, args);
                    await message.Channel.SendMessageAsync($"Atributo excluido de tus recomendaciones: {args.ToLowerInvariant()}.");
                    break;

                case "!ordenar":
                    _fachada.CambiarOrden(usuarioId, args);
                    await message.Channel.SendMessageAsync($"Criterio de orden actualizado a {args.ToLowerInvariant()}.");
                    break;

                }
            }

            catch (InvalidOperationException ex)
            {
                await message.Channel.SendMessageAsync($"Error: {ex.Message}");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex}");
                await message.Channel.SendMessageAsync("Ocurrio un error al procesar el comando.");
            }
        }

        private static string ValidarId(string texto)
        {
            if (!Catalogo.EsIdPositivo(texto, out int id))
                throw new InvalidOperationException("El ID debe ser un número entero positivo.");

            return id.ToString(CultureInfo.InvariantCulture);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
