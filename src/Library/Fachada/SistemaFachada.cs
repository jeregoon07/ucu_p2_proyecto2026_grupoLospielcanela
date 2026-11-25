using System;
using System.Collections.Generic;
using System.Linq;
using Library;

namespace Project
{
    public class SistemaFachada
    {
        private readonly Dictionary<string, Usuario> _usuarios = new Dictionary<string, Usuario>();
        private readonly Catalogo _catalogo;
        private readonly MotorRecomendacion _motorRecomendacion;

        public SistemaFachada(Catalogo catalogo, MotorRecomendacion motorRecomendacion)
        {
            _catalogo = catalogo ?? throw new ArgumentNullException(nameof(catalogo));
            _motorRecomendacion = motorRecomendacion ?? throw new ArgumentNullException(nameof(motorRecomendacion));
        }

        public Usuario RegistrarUsuario(string id, string nombre)
        {
            var usuario = new Usuario(id, nombre);
            if (!_usuarios.TryAdd(id, usuario))
                throw new InvalidOperationException($"El usuario '{id}' ya existe.");

            return usuario;
        }

        public void RegistrarInteraccion(string usuarioId, string itemId, double? valoracion = null)
        {
            var usuario = ObtenerUsuario(usuarioId);
            var item = ObtenerItem(itemId);
            usuario.RegistrarInteraccion(new Interaccion(usuario, item, DateTime.Now, valoracion));
        }

        public void ActualizarPreferencias(string usuarioId, Preferencia preferencia)
        {
            ObtenerUsuario(usuarioId).ActualizarPreferencia(preferencia);
        }

        public void ExcluirAtributo(string usuarioId, string atributo)
        {
            var usuario = ObtenerUsuario(usuarioId);
            if (string.IsNullOrWhiteSpace(atributo))
                throw new InvalidOperationException("Indicá un atributo para excluir.");

            usuario.AtributosExcluidos.Add(atributo.Trim().ToLowerInvariant());
        }

        public void CambiarOrden(string usuarioId, string criterio)
        {
            var usuario = ObtenerUsuario(usuarioId);
            criterio = criterio.Trim().ToLowerInvariant();
            if (criterio != "score" && criterio != "nombre")
                throw new InvalidOperationException("El criterio debe ser score o nombre.");

            usuario.CriterioOrden = criterio;
        }

        public List<IRecomendable> ObtenerRecomendaciones(string usuarioId)
        {
            var usuario = ObtenerUsuario(usuarioId);
            var catalogoDisponible = _catalogo;
            if (usuario.AtributosExcluidos.Count > 0)
            {
                catalogoDisponible = new Catalogo();
                foreach (var item in _catalogo.ObtenerTodos())
                {
                    if (!item.Atributos.Any(a => usuario.AtributosExcluidos.Contains(a.Trim())))
                        catalogoDisponible.Agregar(item);
                }
            }

            var recomendaciones = _motorRecomendacion.Recomendar(usuario, catalogoDisponible)
                ?? new List<IRecomendable>();
            if (usuario.CriterioOrden == "nombre")
                return recomendaciones.OrderBy(item => item.Nombre, StringComparer.OrdinalIgnoreCase).ToList();

            return recomendaciones;
        }

        public bool GuardarParaMasTarde(string usuarioId, string itemId)
        {
            var usuario = ObtenerUsuario(usuarioId);
            return usuario.GuardarContenido(ObtenerItem(itemId));
        }

        public List<IRecomendable> ObtenerHistorialConsumidos(string usuarioId)
        {
            return ObtenerUsuario(usuarioId).ObtenerHistorial()
                .Select(interaccion => interaccion.Item)
                .ToList();
        }

        private Usuario ObtenerUsuario(string usuarioId)
        {
            if (!_usuarios.TryGetValue(usuarioId, out var usuario))
                throw new InvalidOperationException("Primero tenés que registrarte con !registrar.");

            return usuario;
        }

        private IRecomendable ObtenerItem(string itemId)
        {
            return _catalogo.BuscarPorId(itemId)
                ?? throw new InvalidOperationException($"El ID '{itemId}' no existe en el catálogo.");
        }

        public IReadOnlyList<IRecomendable> ObtenerCatalogo()
        {
            return _catalogo.ObtenerTodos();
        }

        public void AgregarItemAlCatalogo(IRecomendable item)
        {
            _catalogo.Agregar(item);
        }

        public bool IntentarAgregarItemAlCatalogo(IRecomendable item)
        {
            return _catalogo.IntentarAgregar(item);
        }

        public void EliminarItemDelCatalogo(string itemId)
        {
            if (!_catalogo.Quitar(itemId))
                throw new InvalidOperationException($"El ID '{itemId}' no existe en el catálogo.");
        }

        public IReadOnlyList<IRecomendable> ObtenerGuardados(string usuarioId)
        {
            return ObtenerUsuario(usuarioId).ObtenerGuardadosParaMasTarde();
        }

        public List<IRecomendable> ObtenerRelacionados(string itemId)
        {
            var itemBase = ObtenerItem(itemId);
            return _catalogo.ObtenerTodos()
                .Where(item => item != itemBase && item.Atributos.Intersect(itemBase.Atributos, StringComparer.OrdinalIgnoreCase).Any())
                .ToList();
        }
    }
}
