using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Project
{
    public class Cancion : IRecomendable
    {
        public string ID{get; private set;}
        public string Titulo{get; private set;}
        public string Artista{get; private set;}
        public string Genero{get; private set;}
        public string Categoria{get;private set;}
        public Cancion(string id, string titulo, string artista, string genero)
        {
            ID=id;
            Titulo=titulo;
            Artista=artista;
            Genero=genero;
            Categoria="Musica";
        }
        public bool TieneAtributo(string clave, string valor)
        {
            
            switch(clave.ToLower())
            {
                case "artista":
                return Artista.Equals(valor, StringComparison.OrdinalIgnoreCase);
                case "genero":
                return Genero.Equals(valor, StringComparison.OrdinalIgnoreCase);
                case "categoria":
                return Categoria.Equals(valor, StringComparison.OrdinalIgnoreCase);
                default:
                return false;
            }
        }
        public List<string> ObtenerAtributoClave()
        {
            return new List<string>{"artista", "genero", "categoriá"};
        }

    }
}

