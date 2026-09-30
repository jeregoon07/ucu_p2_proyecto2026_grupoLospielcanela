using System;
using System.Collections.Generic;
using System.Linq;

namespace Project
{
    /// <summary>
    /// Define el contrato que debe cumplir cualquier ítem del catálogo.
    /// Expone ID, nombre y atributos genéricos para el motor de recomendación.
    /// Colaboradores: Ninguno.
    /// </summary>
    public interface IRecomendable
    {
        string Id { get; }
        string Nombre { get; }
        IReadOnlyList<string> Atributos { get; }
    }

    /// <summary>
    /// Representa una entidad concreta del dominio de música.
    /// Implementa IRecomendable.
    /// </summary>
    public class Cancion : IRecomendable
    {
        public string Id { get; }
        public string Nombre { get; }
        public IReadOnlyList<string> Atributos { get; }

        public Cancion(string id, string nombre, IEnumerable<string> atributos)
        {
            Id = id;
            Nombre = nombre;
            Atributos = (atributos ?? Enumerable.Empty<string>()).ToList().AsReadOnly();
        }
    }

    /// <summary>
    /// Almacena la colección global de contenidos. Permite altas y bajas
    /// de ítems por el administrador y provee métodos de búsqueda y consulta.
    /// Colaboradores: IRecomendable.
    /// </summary>
    public class Catalogo
    {
        private readonly Dictionary<string, IRecomendable> _items = new Dictionary<string, IRecomendable>();

        public void Agregar(IRecomendable item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _items[item.Id] = item;
        }

        public void Quitar(string id)
        {
            _items.Remove(id);
        }

        public IRecomendable BuscarPorId(string id)
        {
            _items.TryGetValue(id, out var item);
            return item;
        }

        public IReadOnlyList<IRecomendable> ObtenerTodos()
        {
            return _items.Values.ToList().AsReadOnly();
        }
    }

    /// <summary>
    /// Registra el consumo de un ítem por parte del usuario y guarda
    /// la valoración explícita (opcional).
    /// Colaboradores: Usuario, IRecomendable.
    /// </summary>
    public class Interaccion
    {
        public Usuario Usuario { get; }
        public IRecomendable Item { get; }
        public DateTime Fecha { get; }
        public double? Valoracion { get; }

        public Interaccion(Usuario usuario, IRecomendable item, DateTime fecha, double? valoracion = null)
        {
            Usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Fecha = fecha;
            Valoracion = valoracion;
        }
    }

    /// <summary>
    /// Representa los atributos de interés del usuario.
    /// Colaboradores: Usuario, Catalogo.
    /// </summary>
    public class Preferencia
    {
        public IReadOnlyList<string> AtributosPreferidos { get; }

        public Preferencia(IEnumerable<string> atributosPreferidos)
        {
            AtributosPreferidos = (atributosPreferidos ?? Enumerable.Empty<string>()).ToList().AsReadOnly();
        }
    }

    /// <summary>
    /// Recibe e invoca la estrategia configurada, aplica los filtros sobre
    /// los candidatos y ordena el listado final según un criterio.
    /// Versión simplificada: recomienda por coincidencia de atributos con
    /// las preferencias del usuario, si existen; si no, recomienda por
    /// popularidad simple (orden del catálogo).
    /// Colaboradores: Usuario, Catalogo, IRecomendable.
    /// </summary>
    public class MotorRecomendacion
    {
        public List<IRecomendable> Recomendar(Usuario usuario, Catalogo catalogo)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            if (catalogo == null) throw new ArgumentNullException(nameof(catalogo));

            var candidatos = catalogo.ObtenerTodos().AsEnumerable();

            // Filtro: excluir contenidos ya consumidos por el usuario
            var idsConsumidos = usuario.ObtenerHistorial().Select(i => i.Item.Id).ToHashSet();
            candidatos = candidatos.Where(c => !idsConsumidos.Contains(c.Id));

            var preferencia = usuario.ObtenerPreferencia();
            if (preferencia != null && preferencia.AtributosPreferidos.Any())
            {
                candidatos = candidatos
                    .OrderByDescending(c => c.Atributos.Intersect(preferencia.AtributosPreferidos).Count());
            }

            return candidatos.ToList();
        }
    }
}