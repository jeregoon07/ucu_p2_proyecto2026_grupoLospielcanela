using System;
using System.Collections.Generic;
using System.Linq;

namespace Project
{
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

            ValidarValoracion(valoracion);
            Valoracion = valoracion;
        }

        /// <summary>
        /// Valida que la valoración, si se proporciona, sea un número entero entre 1 y 5.
        /// </summary>
        private void ValidarValoracion(double? valoracion)
        {
            if (!valoracion.HasValue)
                return;

            if (double.IsNaN(valoracion.Value) ||
                double.IsInfinity(valoracion.Value) ||
                valoracion.Value < 1 ||
                valoracion.Value > 5 ||
                valoracion.Value % 1 != 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(valoracion),
                    valoracion.Value,
                    "La valoración debe ser un número entero entre 1 y 5.");
            }
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

    public class Usuario
    {
        public string Id { get; private set; }
        public string Nombre { get; private set; }
        public Preferencia Preferencia => _preferencia;
        public IReadOnlyList<Interaccion> Interacciones => _interacciones.AsReadOnly();
        private Preferencia _preferencia;
        private readonly List<Interaccion> _interacciones;
        private readonly List<IRecomendable> _guardadosParaMasTarde;

        public Usuario(string id, string nombre)
        {
            ValidarDatos(id, nombre);

            Id = id;
            Nombre = nombre;
            _interacciones = new List<Interaccion>();
            _guardadosParaMasTarde = new List<IRecomendable>();
        }

        /// <summary>
        /// Valida que los datos básicos de la cuenta sean correctos.
        /// </summary>
        private void ValidarDatos(string id, string nombre)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El id del usuario no puede estar vacío.", nameof(id));

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del usuario no puede estar vacío.", nameof(nombre));
        }

        /// <summary>
        /// Actualiza las preferencias de contenido del usuario.
        /// </summary>
        public void ActualizarPreferencia(Preferencia preferencia)
        {
            _preferencia = preferencia ?? throw new ArgumentNullException(nameof(preferencia));
        }

        public Preferencia ObtenerPreferencia()
        {
            return _preferencia;
        }

        /// <summary>
        /// Registra una nueva interacción (consumo y/o valoración) en el
        /// historial del usuario.
        /// </summary>
        public void RegistrarInteraccion(Interaccion interaccion)
        {
            if (interaccion == null)
                throw new ArgumentNullException(nameof(interaccion));

            _interacciones.Add(interaccion);
        }

        public IReadOnlyList<Interaccion> ObtenerHistorial()
        {
            return _interacciones.AsReadOnly();
        }

        /// <summary>
        /// Agrega un ítem del catálogo a la lista de contenidos guardados
        /// para ver más tarde.
        /// </summary>
        public void GuardarContenido(IRecomendable item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!_guardadosParaMasTarde.Contains(item))
                _guardadosParaMasTarde.Add(item);
        }

        public IReadOnlyList<IRecomendable> ObtenerGuardadosParaMasTarde()
        {
            return _guardadosParaMasTarde.AsReadOnly();
        }
    }

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
}