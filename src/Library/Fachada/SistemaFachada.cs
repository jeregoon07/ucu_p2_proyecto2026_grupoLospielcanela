using System;
using System.Collections.Generic;

namespace Project
{
    
    public class SistemaFachada
    {
        private readonly Dictionary<string, Usuario> _usuarios;
        private readonly Catalogo _catalogo;
        private readonly MotorRecomendacion _motorRecomendacion;

        public SistemaFachada(Catalogo catalogo, MotorRecomendacion motorRecomendacion)
        {
            _usuarios = new Dictionary<string, Usuario>();
            _catalogo = catalogo ?? throw new ArgumentNullException(nameof(catalogo));
            _motorRecomendacion = motorRecomendacion ?? throw new ArgumentNullException(nameof(motorRecomendacion));
        }

        
        public Usuario RegistrarUsuario(string id, string nombre)
        {
            if (_usuarios.ContainsKey(id))
                throw new InvalidOperationException($"El usuario '{id}' ya existe.");

            var usuario = new Usuario(id, nombre);
            _usuarios.Add(id, usuario);
            return usuario;
        }

        
        public void RegistrarInteraccion(string usuarioId, string itemId, double? valoracion = null)
        {
            var usuario = ObtenerUsuario(usuarioId);
            var item = _catalogo.BuscarPorId(itemId);

            if (item == null)
                throw new InvalidOperationException($"El ítem '{itemId}' no existe en el catálogo.");

            var interaccion = new Interaccion(usuario, item, DateTime.Now, valoracion);
            usuario.RegistrarInteraccion(interaccion);
        }

        
        public void ActualizarPreferencias(string usuarioId, Preferencia preferencia)
        {
            var usuario = ObtenerUsuario(usuarioId);
            usuario.ActualizarPreferencia(preferencia);
        }

        
        public List<IRecomendable> ObtenerRecomendaciones(string usuarioId)
        {
            var usuario = ObtenerUsuario(usuarioId);
            return _motorRecomendacion.Recomendar(usuario, _catalogo);
        }

        
        public void GuardarParaMasTarde(string usuarioId, string itemId)
        {
            var usuario = ObtenerUsuario(usuarioId);
            var item = _catalogo.BuscarPorId(itemId);

            if (item == null)
                throw new InvalidOperationException($"El ítem '{itemId}' no existe en el catálogo.");

            usuario.GuardarContenido(item);
        }

        private Usuario ObtenerUsuario(string usuarioId)
        {
            if (!_usuarios.TryGetValue(usuarioId, out var usuario))
                throw new InvalidOperationException($"El usuario '{usuarioId}' no está registrado.");

            return usuario;
        }
    }
}