using System;
using System.Collections.Generic;
using System.Linq;
namespace Project{
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