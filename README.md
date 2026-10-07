# Universidad Católica del Uruguay

## Facultad de Ingeniería y Tecnologías

### Programación II

# Proyecto 2026


CHATBOT

## Integrantes

- Jeremías González
- Tamara Flores
- Luis Orlando Cartagena
- Lautaro Pereira Das Neves

---

# Descripción del proyecto

Este proyecto consiste en desarrollar un sistema de recomendaciones de canciones que permita ofrecer contenido musical personalizado a los usuarios según sus preferencias, historial de interacciones y otros criterios.

El sistema permitirá consultar un catálogo de canciones, registrar las interacciones de los usuarios y generar recomendaciones utilizando diferentes estrategias. Estas recomendaciones podrán basarse en las preferencias del usuario, su historial, usuarios con gustos similares, la popularidad de las canciones o contenidos relacionados.

El problema que busca resolver es la dificultad de encontrar contenido musical que se adapte a los gustos de cada usuario dentro de un catálogo amplio. El sistema busca facilitar el descubrimiento de nuevas canciones mediante recomendaciones personalizadas y organizadas según diferentes criterios.

Además, el sistema contará con filtros para evitar recomendar contenido que el usuario ya haya consumido o que no sea adecuado según sus preferencias, y permitirá ordenar las recomendaciones según distintos criterios.

---

# Historias de usuario

HU01 - Consulta de preguntas frecuentes
Como usuario, quiero escribir mi pregunta en un chat y recibir una respuesta inmediata para resolver mis dudas sin tener que esperar a que me atienda una persona.

HU01 - Consultar recomendaciones
Como usuario, quiero recibir recomendaciones de canciones para descubrir contenido que pueda ser de mi interés.

HU02 - Recomendaciones según preferencias
Como usuario, quiero recibir recomendaciones basadas en mis preferencias para encontrar canciones acordes a mis gustos.

HU03 - Recomendaciones según historial
Como usuario, quiero recibir recomendaciones basadas en las canciones que escuché anteriormente para descubrir contenido similar.

HU04 - Recomendaciones según usuarios similares
Como usuario, quiero recibir recomendaciones basadas en los gustos de otros usuarios con preferencias similares a las mías.

HU05 - Recomendaciones por popularidad
Como usuario, quiero recibir recomendaciones de canciones populares para conocer contenido que está siendo escuchado por otros usuarios.

HU06 - Filtrar y ordenar recomendaciones
Como usuario, quiero que las recomendaciones puedan filtrarse y ordenarse para recibir resultados más relevantes y evitar canciones que ya escuché.

HU07 - Gestionar catálogo e interacciones
Como administrador, quiero gestionar el catálogo de canciones y las interacciones de los usuarios para mantener actualizada la información utilizada por el sistema de recomendaciones.

---

# Modelo del dominio

El modelo del dominio fue desarrollado a partir de las historias de usuario y requerimientos identificados para el proyecto de motor de recomendaciones.

Las principales entidades y componentes identificados para el dominio son:

- **SistemaFachada:** punto de entrada unificado que coordina la interacción del sistema (como el Bot o suites de prueba) con la lógica interna de usuarios, catálogo y recomendaciones.
- **MotorRecomendacion:** componente central encargado de aplicar estrategias de recomendación, ejecutar filtros y ordenar los ítems sugeridos.
- **Usuario:** representa a la persona en el sistema, gestionando sus datos de cuenta, historial de interacciones, contenido guardado y preferencias.
- **Cancion:** entidad concreta del catálogo que representa los contenidos musicales e implementa la interfaz `IRecomendable`.
- **Catalogo:** representa la colección global de contenidos (`IRecomendable`), permitiendo la administración, altas/bajas y búsquedas de ítems.
- **Interaccion:** registra los consumos y valoraciones explícitas de los usuarios sobre los contenidos del catálogo.
- **Preferencia:** representa los atributos de interés específicos configurados por un usuario.
- **Estrategias de Recomendación (`IRecomendadorStrategy`):** algoritmos intercambiables en tiempo de ejecución para generar sugerencias según distintas métricas (`EstrategiaPreferencia`, `EstrategiaHistorial`, `EstrategiaUsuarioSimilares`, `EstrategiaPopularidad` y `EstrategiaRelacionados`).
- **FiltroRecomendacion y CriterioOrden:** componentes encargados de excluir contenidos ya consumidos o no deseados, así como de aplicar el criterio final de ordenamiento sobre las sugerencias.

Estas clases permiten desacoplar la lógica interna del sistema de las interfaces de usuario, permitiendo evaluar, puntuar y ofrecer recomendaciones personalizadas basadas en el comportamiento, preferencias e historial del usuario.

---

# Diagrama de clases UML



![Diagrama UML](diagrama%20UML.png)


---

# Tarjetas CRC

Las tarjetas CRC utilizadas para el modelado del dominio se encuentran
en:

[📄 Ver Tarjetas CRC del Proyecto](Tarjetas%20CRC%20Proyecto.pdf)


---

# Código fuente

El código fuente del proyecto se encuentra dentro de la carpeta `src/`.

La estructura principal del proyecto es:

```text
src/
├── Program/
└── Library/
```
---

# Trello
## Tablero de Trello del proyecto
[Enlace del Trello](https://trello.com/invite/b/6aadc6b6105726661e304a73/ATTIc27f239217c77fc450d80ea0d097b4b79849C099/los-piel-canela)

## Organización del tablero

El tablero de Trello se organiza en las siguientes columnas:

TODO: tareas pendientes de realizar.
WIP: tareas que se encuentran actualmente en progreso.
DONE: tareas finalizadas.

Cada tarea incluye:

Responsable.
Fecha prevista de finalización.
Distribución de tareas

Integrantes y sus responsabilidades más importantes.
Jeremías González	______________________________
Tamara Flores	Documentación y esqueleto del programa.
Luis Orlando Cartagena	Tests unitarios.
Lautaro Pereira Das Neves	______________________________
Reflexiones del equipo:

--- 

# Desafíos encontrados

Uno de los principales desafíos durante el desarrollo fue organizar correctamente la estructura del proyecto y distribuir las responsabilidades entre los integrantes del equipo, pero gracias a la estructura formada en Trello y la comunicación se pudo hallar un buen punto medio.


## Aprendizajes

Durante el proyecto aprendimos a trabajar de forma colaborativa utilizando Git, GitHub y Trello, además de organizar mejor las tareas y responsabilidades del equipo.

Además, pudimos profundizar nuestros conocimientos sobre programación orientada a objetos, diseño UML, pruebas unitarias y organización de un proyecto en C# y .NET.

---

# Recursos utilizados

Para resolver los diferentes desafíos utilizamos los siguientes
recursos:

## Otras observaciones

## Tecnologías utilizadas

C#
.NET
Visual Studio Code
Git
GitHub
Trello
UML

---

# Estado del proyecto

