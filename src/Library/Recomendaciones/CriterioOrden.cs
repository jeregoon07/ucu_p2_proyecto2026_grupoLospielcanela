using System.Collections.Generic;
using System.Linq;

namespace Project
{
    public class CriterioOrden
    {
        public List<IRecomendable> OrdenarListaFinal(List<IRecomendable> itemsRecomendados)
        {
            return itemsRecomendados.OrderBy(item => item.Nombre).ToList();
        }
        public List<IRecomendable> OrdenarPorRelevancia(List<IRecomendable> itemsRecomendados, Preferencia preferenciasUsuario)
        {
            return itemsRecomendados.OrderByDescending(item => 
                item.Atributos.Count(attr => preferenciasUsuario.AtributosPreferidos.Contains(attr))
            ).ToList();
        }
    }
}