using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Project
{

    public class Catalogo
    {
        private readonly Dictionary<string, IRecomendable> _items = new Dictionary<string, IRecomendable>();

        public void Agregar(IRecomendable item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (!IntentarAgregar(item))
                throw new InvalidOperationException($"El ID '{item.Id}' ya existe en el catálogo.");
        }

        internal bool IntentarAgregar(IRecomendable item)
        {
            return _items.TryAdd(NormalizarId(item.Id), item);
        }

        public bool Quitar(string id)
        {
            return _items.Remove(NormalizarId(id));
        }

        public IRecomendable BuscarPorId(string id)
        {
            _items.TryGetValue(NormalizarId(id), out var item);
            return item;
        }

        internal static string NormalizarId(string id)
        {
            return EsIdPositivo(id, out int numero)
                ? numero.ToString(CultureInfo.InvariantCulture)
                : id;
        }

        public static bool EsIdPositivo(string id, out int numero)
        {
            return int.TryParse(id, NumberStyles.Integer, CultureInfo.InvariantCulture, out numero)
                && numero > 0;
        }

        public IReadOnlyList<IRecomendable> ObtenerTodos()
        {
            return _items.Values.ToList().AsReadOnly();
        }
    }

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
            if (valoracion is double puntaje &&
                (!(puntaje >= 1 && puntaje <= 5) || puntaje != Math.Truncate(puntaje)))
                throw new ArgumentOutOfRangeException(nameof(valoracion), "La valoración debe ser un entero del 1 al 5.");

            Valoracion = valoracion;
        }
    }

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
        internal HashSet<string> AtributosExcluidos { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        internal string CriterioOrden { get; set; } = "score";
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

        private void ValidarDatos(string id, string nombre)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El id del usuario no puede estar vacío.", nameof(id));

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del usuario no puede estar vacío.", nameof(nombre));
        }

        public void ActualizarPreferencia(Preferencia preferencia)
        {
            _preferencia = preferencia ?? throw new ArgumentNullException(nameof(preferencia));
        }

        public Preferencia ObtenerPreferencia()
        {
            return _preferencia;
        }

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

        public bool GuardarContenido(IRecomendable item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (_guardadosParaMasTarde.Contains(item))
                return false;

            _guardadosParaMasTarde.Add(item);
            return true;
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
            Id = Catalogo.NormalizarId(id);
            Nombre = nombre;
            Atributos = (atributos ?? Enumerable.Empty<string>()).ToList().AsReadOnly();
        }
    }
}
