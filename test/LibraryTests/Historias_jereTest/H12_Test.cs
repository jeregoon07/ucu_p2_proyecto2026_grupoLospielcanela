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
    /// Pruebas unitarias de la historia de usuario 12
    /// Visualizar el listado de contenidos ya consumidos
    /// </summary>
    [TestFixture]
    public class HU12_test
    {
        /// <summary>
        /// Prueba unitaria que verifica que al registrar una interacción con un item existente,
        /// el método devuelva correctamente el contenido con el historial
        /// </summary>
        [Test]
        public void HU12_VisualizarHistorial()
        {
            Catalogo catalogo = new Catalogo();
            MotorRecomendacion motor = new MotorRecomendacion();
            SistemaFachada fachada = new SistemaFachada(catalogo, motor);

            String usuarioId = "Noel4L";
            fachada.RegistrarUsuario(usuarioId, "Noel");

            string itemId = "item1";

            ItemPrueba prouctoPrueba = new ItemPrueba(itemId, "Don't look back in anger");

            catalogo.Agregar(prouctoPrueba);

            fachada.RegistrarInteraccion(usuarioId, itemId, 7.5);

            List<IRecomendable> historial = null;
            bool ejecutoSinErrores= true;
            try
            {
                historial= fachada.ObtenerHistorialConsumidos(usuarioId);
            }
            catch (Exception)
            {
                ejecutoSinErrores= false;
            }
            Assert.IsTrue(ejecutoSinErrores, "El metodo para obtener el historial no deberia lanzar ningún error");
            Assert.IsNotNull(historial, "El historial no debe de ser nulo");
            Assert.IsTrue(historial.Count>0, "El historial debe de contener almenos 1 elemento");

            IRecomendable primerItem = historial[0];
            Assert.AreEqual(itemId, primerItem.Id, "El ID del ítem consumido debe de ser igual...");
            Assert.AreEqual("Don't look back in anger", primerItem.Nombre, "El nombre del item debe de ser visible");
        }
        /// <summary>
        /// Clase auxiliar de prueba que implementa la interfaz <see cref="IRecomendable"/>
        /// para simular elementos dentro del catálogo 
        /// </summary>
        public class ItemPrueba: IRecomendable
        {
            public string Id{get;set;}
            public string Nombre{get;set;}
            /// <summary>
            /// Obtiene o establece los atributos asociados al ítem
            /// </summary>
            public IReadOnlyList<string> Atributos { get; set;} = new List<string>();
            /// <summary>
            /// Inicializa una nueva instancia de la clase <see cref="ItemPrueba"/>
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