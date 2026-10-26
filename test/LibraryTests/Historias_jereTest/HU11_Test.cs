using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Security;
using Microsoft.VisualBasic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Project;
namespace Library.Tests
{
    /// <summary>
    /// Clase de prueba unitaria encargada de la Historia de Usuario 11
    /// Indica si un contenido le gustó o no al usuario mediante valores numéricos 
    /// </summary>
    [TestFixture]
    public class HU11Test
    {
        /// <summary>
        /// Prueba unitaria que verifica que el sistema permite verificar una interaccion
        /// con valoración alta indicando que le gusto
        /// </summary>
        [Test]
        public void IndicarSiMeGusto_HU11()
        {
            Catalogo catalogo = new Catalogo();
            MotorRecomendacion motor = new MotorRecomendacion();
            SistemaFachada fachada = new SistemaFachada (catalogo, motor);

            string usuarioId = "Lau44f";
            fachada.RegistrarUsuario(usuarioId, "Lautaro");

            string itemId="item1";
            IRecomendable item = new ItemPrueba(itemId, "de prueba");
            catalogo.Agregar(item);
            bool seRegistroConExito= true;
            try
            {
                fachada.RegistrarInteraccion(usuarioId, itemId, 7.0 );
            }
            catch (Exception)
            {
                seRegistroConExito= false;
            }
            Assert.IsTrue(seRegistroConExito, "El sistema debe permitir registrar que me gusto");
        }
        /// <summary>
        /// Prueba unitaria que verifica que el sistema permita verificar una interacción
        /// con valoración baja indicando que no le gustó al usuario 
        /// </summary>
        [Test]
        public void IndicarNoMeGusto_HU11()
        {
            Catalogo catalogo = new Catalogo();
            MotorRecomendacion motor = new MotorRecomendacion();
            SistemaFachada fachada = new SistemaFachada (catalogo, motor);

            string usuarioId = "Lau44f";
            fachada.RegistrarUsuario(usuarioId, "Lautaro");

            string itemId="item2";
            IRecomendable item = new ItemPrueba(itemId, "otra prueba");
            catalogo.Agregar(item);
            bool seRegistroConExito = true;

            try
            {
                fachada.RegistrarInteraccion(usuarioId, itemId, 1.0);
            }
            catch (Exception)
            {
                seRegistroConExito= false;
            }
            Assert.IsTrue(seRegistroConExito, "El sistema debe permitir registrar que no me gusto");
        }
        /// <summary>
        /// Clase auxiliar de prueba que implementa la interfaz <see cref="IRecomendable"/>
        /// para simular elementos del catálogo
        /// </summary>
        public class ItemPrueba: IRecomendable
        {
            public string Id{get;set;}
            public string Nombre{get;set;}
            public IReadOnlyList<string> Atributos { get; set;} = new List<string>();
            /// <summary>
            /// Inicializa una nueva instancia de la calse <see cref="ItemPrueba"/>
            /// </summary>
            /// <param name="id"></param>
            /// <param name="nombre"></param>
            public ItemPrueba(string id, string nombre)
            {
                Id=id;
                Nombre=nombre;
            }
            
        }
    }
    
}