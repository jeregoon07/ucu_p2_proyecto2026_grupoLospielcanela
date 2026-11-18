using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Library;
using Project;
namespace Project
{
public class DiscordUCUBot : IDisposable
    {
private DiscordSocketClient _client;
private SistemaFachada _fachada;
private bool IdValido(string id)
        {
            return int.TryParse(id, out int numero) && numero > 0;
        }
        private bool CancionExiste(string id)
        {
            return _fachada.ObtenerCatalogo().Any(c => c.Id == id);
        }
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
string comando = partes[0].ToLower();
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
                            "• !ordenar <criterio> - Cambia el orden de las recomendaciones (HU9)\n" 
                        );
break;
case "!unirse":
await message.Channel.SendMessageAsync($"Hola {nombreUsuario}! Te has comunicado con la fachada del proyecto.");
break;
case "!cargar_demo":
                        _fachada.AgregarItemAlCatalogo(new Cancion("1", "Love in the Dark", new[] { "pop", "soul", "adele", "balada" }));
                        _fachada.AgregarItemAlCatalogo(new Cancion("2", "Headlock", new[] { "electronica", "pop", "imogen heap", "indie" }));
                        _fachada.AgregarItemAlCatalogo(new Cancion("3", "Degenere", new[] { "urbano", "reggaeton", "myke towers", "latino" }));
                        _fachada.AgregarItemAlCatalogo(new Cancion("4", "Natural", new[] { "rock", "pop rock", "imagine dragons" }));
                        _fachada.AgregarItemAlCatalogo(new Cancion("5", "Viva la Vida", new[] { "rock", "pop rock", "coldplay", "indie" }));
await message.Channel.SendMessageAsync("Catalogo demo cargado con las 5 canciones (Love in the Dark, Headlock, Degenere, Natural, Viva la Vida). Usa !catalogo para verlas.");
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
if (string.IsNullOrWhiteSpace(args))
                        {
await message.Channel.SendMessageAsync("Uso correcto: !preferencias pop, rock, indie");
break;
                        }
var attrs = args.Split(',').Select(a => a.Trim().ToLower()).Where(a => !string.IsNullOrEmpty(a));
                        _fachada.ActualizarPreferencias(usuarioId, new Preferencia(attrs));
await message.Channel.SendMessageAsync($"Preferencias actualizadas: {string.Join(", ", attrs)}.");
break;
case "!consumir":
if (string.IsNullOrWhiteSpace(args))
                        {
await message.Channel.SendMessageAsync("Uso correcto: !consumir <id> o !consumir <id> <puntaje 1-5>");
break;
                        }
var subArgs = args.Split(' ', StringSplitOptions.RemoveEmptyEntries);
string itemId = subArgs[0];
if (subArgs.Length > 2 || !IdValido(itemId))
                        {
                            await message.Channel.SendMessageAsync("Error: indica un ID positivo y, opcionalmente, una valoración de 1 a 5.");
                            break;
                        }
                        if (!CancionExiste(itemId))
                        {
                            await message.Channel.SendMessageAsync("Error: no existe una canción con ese ID.");
                            break;
                        }
double? valoracion = null;
if (subArgs.Length > 1 && double.TryParse(subArgs[1], out double val))
                        {
                            if (double.IsNaN(val) || double.IsInfinity(val) || val < 1 || val > 5)
                            {
                                await message.Channel.SendMessageAsync("Error: la valoración debe estar entre 1 y 5.");
                                break;
                            }
                            valoracion = val;
                        }
if (subArgs.Length > 1 && !valoracion.HasValue)
                        {
                            await message.Channel.SendMessageAsync("Error: la valoración debe ser un número entre 1 y 5.");
                            break;
                        }
                        _fachada.RegistrarInteraccion(usuarioId, itemId, valoracion);
string txtVal = valoracion.HasValue ? $" con valoracion {valoracion}/5" : "";
await message.Channel.SendMessageAsync($"Interaccion registrada para el item {itemId}{txtVal}.");
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
if (string.IsNullOrWhiteSpace(args))
                        {
await message.Channel.SendMessageAsync("Uso correcto: !guardar <id>");
break;
                        }
                        if (!IdValido(args))
                        {
                            await message.Channel.SendMessageAsync("Error: el ID debe ser un número entero mayor que cero.");
                            break;
                        }
                        if (!CancionExiste(args))
                        {
                            await message.Channel.SendMessageAsync("Error: no existe una canción con ese ID.");
                            break;
                        }
                        _fachada.GuardarParaMasTarde(usuarioId, args);
await message.Channel.SendMessageAsync($"Item {args} guardado en tu lista para mas tarde.");
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
if (datosItem.Length < 3)
                        {
await message.Channel.SendMessageAsync("Uso correcto: !agregar <id> | <nombre> | <atributo1, atributo2>");
break;
                        }
string nuevoId = datosItem[0].Trim();
string nuevoNombre = datosItem[1].Trim();
var nuevosAttrs = datosItem[2].Split(',').Select(a => a.Trim().ToLower());
                        if (!IdValido(nuevoId) || string.IsNullOrWhiteSpace(nuevoNombre) ||
                            datosItem.Length != 3 || !nuevosAttrs.Any(a => !string.IsNullOrWhiteSpace(a)))
                        {
                            await message.Channel.SendMessageAsync("Error: usa un ID entero positivo, un nombre y al menos un atributo.");
                            break;
                        }
                        if (CancionExiste(nuevoId))
                        {
                            await message.Channel.SendMessageAsync("Error: ya existe una canción con ese ID.");
                            break;
                        }
                        _fachada.AgregarItemAlCatalogo(new Cancion(nuevoId, nuevoNombre, nuevosAttrs));
await message.Channel.SendMessageAsync($"Cancion {nuevoNombre} agregada al catalogo con ID {nuevoId}.");
break;
case "!eliminar":
if (string.IsNullOrWhiteSpace(args))
                        {
await message.Channel.SendMessageAsync("Uso correcto: !eliminar <id>");
break;
                        }
                        if (!IdValido(args))
                        {
                            await message.Channel.SendMessageAsync("Error: el ID debe ser un número entero mayor que cero.");
                            break;
                        }
                        if (!CancionExiste(args))
                        {
                            await message.Channel.SendMessageAsync("Error: no existe una canción con ese ID.");
                            break;
                        }
                        _fachada.EliminarItemDelCatalogo(args);
await message.Channel.SendMessageAsync($"Item {args} eliminado del catalogo.");
break;
case "!relacionados":
if (string.IsNullOrWhiteSpace(args))
                        {
await message.Channel.SendMessageAsync("Uso correcto: !relacionados <id>");
break;
                        }
var relacionados = if (!IdValido(args))
                        {
                            await message.Channel.SendMessageAsync("Error: el ID debe ser un número entero mayor que cero.");
                            break;
                        }
                        if (!CancionExiste(args))
                        {
                            await message.Channel.SendMessageAsync("Error: no existe una canción con ese ID.");
                            break;
                        }
                        _fachada.ObtenerRelacionados(args);
if (!relacionados.Any())
                        {
await message.Channel.SendMessageAsync("No se encontraron elementos relacionados a ese ID.");
break;
                        }
var listaRel = relacionados.Select((r, idx) => $"{idx + 1}. [{r.Id}] {r.Nombre} ({string.Join(", ", r.Atributos)})");
await message.Channel.SendMessageAsync("Contenido relacionado:\n" + string.Join("\n", listaRel));
break;
case "!excluir":
if (string.IsNullOrWhiteSpace(args))
                        {
await message.Channel.SendMessageAsync("Uso correcto: !excluir <atributo>");
break;
                        }
await message.Channel.SendMessageAsync($"Filtro de exclusion aplicado para: {args}. Las recomendaciones futuras lo ignoraran.");
break;
case "!ordenar":
if (string.IsNullOrWhiteSpace(args))
                        {
                            await message.Channel.SendMessageAsync("Uso correcto: !ordenar <criterio> (ej: score, nombre)");
break;
                        }
                        await message.Channel.SendMessageAsync($"Criterio de orden actualizado a: {args}.");
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
public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
