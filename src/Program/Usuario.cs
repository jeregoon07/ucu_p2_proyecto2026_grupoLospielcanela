using System;
using System.Collections.Generic;

namespace Project
{
    /// <summary>
    /// Almacena y valida los datos de cuenta de un usuario, gestiona sus
    /// preferencias de contenido, mantiene el historial de interacciones
    /// y valoraciones, y administra la lista de contenidos guardados
    /// para más tarde.
    /// Colaboradores: Interaccion, Preferencia, IRecomendable.
    /// </summary>
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
}