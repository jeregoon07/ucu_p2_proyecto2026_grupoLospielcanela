using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Project
{
    public class EstrategiaRelacionado : IRecomendadorStrategy
    {
        private readonly IRecomendable _itemBase;
        public EstrategiaRelacionado(IRecomendable itemBase)
        {
            _itemBase= itemBase ?? throw new ArgumentNullException(nameof(itemBase));
        }
        public IReadOnlyList<IRecomendable> Recomendar(Usuario usuario, Catalogo catalogo)
        {
            if (catalogo==null)
            {
                throw new ArgumentNullException(nameof(catalogo));
            }
            var recomendados = new List<IRecomendable>();
            var candidatos = catalogo.ObtenerTodos();
            foreach (var candidato in candidatos)
            {
                if (candidato.Id.Equals(_itemBase.Id, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                bool tieneCoincidencia = candidato.Atributos
                .Any(attrCandidato => _itemBase.Atributos
                .Any(attrBase=> attrBase.Equals(attrCandidato, StringComparison.OrdinalIgnoreCase)));
                if (tieneCoincidencia)
                {
                    recomendados.Add(candidato);
                }
            }
            return recomendados.AsReadOnly();
        }
    }
}