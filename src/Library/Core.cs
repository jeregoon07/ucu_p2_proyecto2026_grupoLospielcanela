using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Project
{
    public interface IRecomendable
    {
        string Id { get; }
        string Nombre { get; }
        IReadOnlyList<string> Atributos { get; }
    }
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

    
    /// Registra el consumo de un ítem por parte del usuario y guarda
    /// la valoración explícita (opcional).
    /// Colaboradores: Usuario, IRecomendable.
    
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

    
    /// Representa los atributos de interés del usuario.
    /// Colaboradores: Usuario, Catalogo.
    
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

        
        /// Valida que los datos básicos de la cuenta sean correctos.
        
        private void ValidarDatos(string id, string nombre)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El id del usuario no puede estar vacío.", nameof(id));

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del usuario no puede estar vacío.", nameof(nombre));
        }

        
        /// Actualiza las preferencias de contenido del usuario.
        
        public void ActualizarPreferencia(Preferencia preferencia)
        {
            _preferencia = preferencia ?? throw new ArgumentNullException(nameof(preferencia));
        }

        public Preferencia ObtenerPreferencia()
        {
            return _preferencia;
        }

        
        /// Registra una nueva interacción (consumo y/o valoración) en el
        /// historial del usuario.
        
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

        
        /// Agrega un ítem del catálogo a la lista de contenidos guardados
        /// para ver más tarde.
        
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