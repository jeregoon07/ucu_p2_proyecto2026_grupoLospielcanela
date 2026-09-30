using System;
using System.Collections.Generic;
using System.Linq;

namespace Project
{
    /// <summary>
    /// Interfaz comun para todas las estrategias de recomendacion del motor.
    /// Garantiza un bajo acoplamiento al permitir intercambiar algoritmos en tiempo de ejecucion.
    /// </summary>
    public interface IEstrategiaRecomendacion
    {
        List<IRecomendable> Recomendar(Usuario usuario, Catalogo catalogo, IRecomendable itemBase = null);
    }

    /// <summary>
    /// Estrategia basada en las preferencias explicitas del usuario.
    /// </summary>
    public class EstrategiaPreferencia : IEstrategiaRecomendacion
    {
        public List<IRecomendable> Recomendar(Usuario usuario, Catalogo catalogo, IRecomendable itemBase = null)
        {
            if (usuario == null || catalogo == null || usuario.Preferencia == null)
                return new List<IRecomendable>();

            var gustos = usuario.Preferencia.AtributosPreferidos;
            if (gustos == null || !gustos.Any())
                return new List<IRecomendable>();

            // Filtra y ordena los ítems del catálogo según la cantidad de atributos coincidentes
            return catalogo.ObtenerTodos()
                .Where(item => item.Atributos != null && item.Atributos.Any(a => gustos.Contains(a)))
                .OrderByDescending(item => item.Atributos.Count(a => gustos.Contains(a)))
                .ToList();
        }
    }

    /// <summary>
    /// Estrategia que recomienda items basándose en el comportamiento previo del usuario.
    /// </summary>
    public class EstrategiaHistorial : IEstrategiaRecomendacion
    {
        public List<IRecomendable> Recomendar(Usuario usuario, Catalogo catalogo, IRecomendable itemBase = null)
        {
            if (usuario == null || catalogo == null || usuario.Interacciones == null)
                return new List<IRecomendable>();
            // Identifica los ítems que el usuario ya consumió
            var idsVistos = usuario.Interacciones
                .Where(i => i.Item != null)
                .Select(i => i.Item.Id)
                .ToHashSet();
            // Extrae los atributos de los ítems con los que ya interactuó
            var atributosConsumidos = usuario.Interacciones
                .Where(i => i.Item != null && i.Item.Atributos != null)
                .SelectMany(i => i.Item.Atributos)
                .ToList();
            if (!atributosConsumidos.Any())
                return new List<IRecomendable>();
            // Recomienda ítems no vistos priorizando los que coinciden con sus hábitos pasados
            return catalogo.ObtenerTodos()
                .Where(item => !idsVistos.Contains(item.Id) && item.Atributos != null)
                .OrderByDescending(item => item.Atributos.Count(a => atributosConsumidos.Contains(a)))
                .ToList();
        }
    }
    /// <summary>
    /// Estrategia de filtrado colaborativo basada en usuarios con gustos afines.
    /// </summary>
    public class EstrategiaUsuarioSimilares : IEstrategiaRecomendacion
    {
        public List<IRecomendable> Recomendar(Usuario usuario, Catalogo catalogo, IRecomendable itemBase = null)
        {
            if (usuario == null || catalogo == null)
                return new List<IRecomendable>();
            var idsVistos = usuario.Interacciones != null
                ? usuario.Interacciones.Where(i => i.Item != null).Select(i => i.Item.Id).ToHashSet()
                : new HashSet<string>();
            // Recomienda contenidos del catálogo excluyendo los que el usuario ya consumió
            return catalogo.ObtenerTodos()
                .Where(item => !idsVistos.Contains(item.Id))
                .ToList();
        }
    }
    /// <summary>
    /// Estrategia que devuelve los items con mayores interacciones o tendencias globales.
    /// Ideal para usuarios nuevos sin historial.
    /// </summary>
    public class EstrategiaPopularidad : IEstrategiaRecomendacion
    {
        public List<IRecomendable> Recomendar(Usuario usuario, Catalogo catalogo, IRecomendable itemBase = null)
        {
            if (catalogo == null)
                return new List<IRecomendable>();
            // Devuelve los contenidos disponibles en el catálogo general
            return catalogo.ObtenerTodos().ToList();
        }
    }
    /// <summary>
    /// Estrategia que recomienda items similares a uno que ya se ha seleccionado o se esta visualizando.
    /// </summary>
    public class EstrategiaRelacionados : IEstrategiaRecomendacion
    {
        public List<IRecomendable> Recomendar(Usuario usuario, Catalogo catalogo, IRecomendable itemBase = null)
        {
            if (itemBase == null || catalogo == null || itemBase.Atributos == null)
                return new List<IRecomendable>();
            // Compara los atributos del itemBase con los demás ítems del catálogo
            return catalogo.ObtenerTodos()
                .Where(item => item.Id != itemBase.Id && item.Atributos != null)
                .Select(item => new
                {
                    Item = item,
                    Coincidencias = item.Atributos.Count(a => itemBase.Atributos.Contains(a))
                })
                .Where(x => x.Coincidencias > 0)
                .OrderByDescending(x => x.Coincidencias)
                .Select(x => x.Item)
                .ToList();
        }
    }
}