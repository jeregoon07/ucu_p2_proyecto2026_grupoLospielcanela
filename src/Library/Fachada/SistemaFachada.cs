using System;
using System.Collections.Generic;
using Discord;
using Discord.WebSocket;
using Library;
using System.Linq;

namespace Project
{
    /// <summary>
    /// Clase para el sistema fachada
    /// </summary>
public class SistemaFachada
{
    /// <summary>
    /// Diccionario para guardar los usuarios
    /// </summary>
private readonly Dictionary<string, Usuario> _usuarios;
    /// <summary>
    /// Lista del catalogo
    /// </summary>
private readonly Catalogo _catalogo;
private readonly MotorRecomendacion _motorRecomendacion;
/// <summary>
/// Constructor del sistema fachada
/// </summary>
/// <param name="catalogo"></param>
/// <param name="motorRecomendacion"></param>
/// <exception cref="ArgumentNullException"></exception>
public SistemaFachada(Catalogo catalogo, MotorRecomendacion motorRecomendacion)
{
            _usuarios = new Dictionary<string, Usuario>();
            _catalogo = catalogo ?? throw new ArgumentNullException(nameof(catalogo));
            _motorRecomendacion = motorRecomendacion ?? throw new ArgumentNullException(nameof(motorRecomendacion));
}
/// <summary>
/// Constructor para registrar usuario
/// </summary>
/// <param name="id"></param>
/// <param name="nombre"></param>
/// <returns></returns>
/// <exception cref="InvalidOperationException"></exception>
public Usuario RegistrarUsuario(string id, string nombre)
{
if (_usuarios.ContainsKey(id))
throw new InvalidOperationException($"El usuario '{id}' ya existe.");
var usuario = new Usuario(id, nombre);
            _usuarios.Add(id, usuario);
return usuario;
}
/// <summary>
/// Metodo para registrar interaccion
/// </summary>
/// <param name="usuarioId"></param>
/// <param name="itemId"></param>
/// <param name="valoracion"></param>
/// <exception cref="InvalidOperationException"></exception>
public void RegistrarInteraccion(string usuarioId, string itemId, double? valoracion = null)
{
var usuario = ObtenerUsuario(usuarioId);
var item = _catalogo.BuscarPorId(itemId);
if (item == null)
throw new InvalidOperationException($"El ítem '{itemId}' no existe en el catálogo.");
var interaccion = new Interaccion(usuario, item, DateTime.Now, valoracion);
            usuario.RegistrarInteraccion(interaccion);
}
/// <summary>
/// Metodo para actualizar las preferencias
/// </summary>
/// <param name="usuarioId"></param>
/// <param name="preferencia"></param>
public void ActualizarPreferencias(string usuarioId, Preferencia preferencia)
{
var usuario = ObtenerUsuario(usuarioId);
            usuario.ActualizarPreferencia(preferencia);
}
/// <summary>
/// Lista para obtener recomendaciones
/// </summary>
/// <param name="usuarioId"></param>
/// <returns></returns>
public List<IRecomendable> ObtenerRecomendaciones(string usuarioId)
{
var usuario = ObtenerUsuario(usuarioId);
return _motorRecomendacion.Recomendar(usuario, _catalogo);
}
/// <summary>
/// Metodo para guardar un ítem para verlo mas tarde
/// </summary>
/// <param name="usuarioId"></param>
/// <param name="itemId"></param>
/// <exception cref="InvalidOperationException"></exception>
public void GuardarParaMasTarde(string usuarioId, string itemId)
{
var usuario = ObtenerUsuario(usuarioId);
var item = _catalogo.BuscarPorId(itemId);
if (item == null)
throw new InvalidOperationException($"El ítem '{itemId}' no existe en el catálogo.");
            usuario.GuardarContenido(item);
}
/// <summary>
/// Metodo para el historial de ítems consumidos
/// </summary>
/// <param name="usuarioId"></param>
/// <returns></returns>
public List<IRecomendable> ObtenerHistorialConsumidos (string usuarioId)
        {
            Usuario usuario = ObtenerUsuario(usuarioId);
            List<IRecomendable> historialItems = new List<IRecomendable>();
            foreach(Interaccion interaccion in usuario.ObtenerHistorial())
            {
                historialItems.Add(interaccion.Item);
            }
            return historialItems;
        }
    /// <summary>
    /// Constructor para el usuario
    /// </summary>
    /// <param name="usuarioId"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
private Usuario ObtenerUsuario(string usuarioId)
{
if (!_usuarios.TryGetValue(usuarioId, out var usuario))
throw new InvalidOperationException($"El usuario '{usuarioId}' no está registrado.");
return usuario;
}
/// <summary>
/// Devuelve todos los ítems disponibles en el catálogo.
/// </summary>
    public IReadOnlyList<IRecomendable> ObtenerCatalogo()
    {
        return _catalogo.ObtenerTodos();
    }
/// <summary>
/// Agrega un ítem al catálogo (ej. una Canción).
/// </summary>
    public void AgregarItemAlCatalogo(IRecomendable item)
    {
        _catalogo.Agregar(item);
    }
/// <summary>
/// Elimina un ítem del catálogo (HU16).
/// </summary>
    public void EliminarItemDelCatalogo(string itemId)
    {
        _catalogo.Quitar(itemId);
    }
/// <summary>
/// Obtiene los ítems guardados para más tarde de un usuario (HU14).
/// </summary>
    public IReadOnlyList<IRecomendable> ObtenerGuardados(string usuarioId)
        {
            var usuario = ObtenerUsuario(usuarioId);
            return usuario.ObtenerGuardadosParaMasTarde();
        }
// 1. Para contenido relacionado (HU15)
    public List<IRecomendable> ObtenerRelacionados(string itemId)
    {
        var itemBase = _catalogo.BuscarPorId(itemId);
        if (itemBase == null)
            throw new InvalidOperationException($"El ítem '{itemId}' no existe en el catálogo.");
        // Devuelve otros ítems del catálogo que compartan al menos un atributo con el ítem base
        return _catalogo.ObtenerTodos()
            .Where(i => i.Id != itemId && i.Atributos.Any(a => itemBase.Atributos.Contains(a)))
            .ToList();
        }
    }
}


