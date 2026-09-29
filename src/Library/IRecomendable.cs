using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Project
{
    public interface IRecomendable
    {
        string ID{get;}
        string Titulo{get;}
        string Categoria{get;}
        string Genero{get;}
        bool TieneAtributo(string clave, string valor);
        List<string> ObtenerAtributoClave();

    }
} 