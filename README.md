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

Este proyecto consiste en desarrollar un sistema en Discord para automatizar la atención y la comunicación con los usuarios mediante un chatbot conversacional.

El sistema permitirá responder de forma automática, rápida y en cualquier momento a las consultas más frecuentes, guiar a los usuarios en distintos procesos y brindarles información de manera clara y sencilla a través de una interfaz de chat, y tendrá como objetivo reducir los tiempos de espera, disminuir la carga de trabajo de las personas que atienden de forma manual y mejorar la experiencia de quienes utilizan el servicio.

El problema que busca resolver es la dificultad de atender un gran volumen de consultas repetitivas de forma eficiente, ya que la atención tradicional suele ser lenta, limitada a ciertos horarios y dependiente de la disponibilidad de personal. La idea general del proyecto es ofrecer un asistente virtual accesible, fácil de usar y disponible las 24 horas, que pueda entender las preguntas de los usuarios, darles respuestas útiles y, cuando sea necesario, derivarlos a una persona para una atención más específica.

---

# Historias de usuario

HU01 - Consulta de preguntas frecuentes
Como usuario, quiero escribir mi pregunta en un chat y recibir una respuesta inmediata para resolver mis dudas sin tener que esperar a que me atienda una persona.

HU02 - Atención disponible en todo momento
Como usuario, quiero poder usar el chatbot a cualquier hora del día para obtener información o ayuda sin depender de horarios de atención.

HU03 - Guía en procesos y trámites
Como usuario, quiero que el chatbot me guíe paso a paso en un proceso o gestión para completarlo correctamente sin cometer errores.

HU04 - Derivación a una persona
Como usuario, quiero poder ser derivado a un agente humano cuando el chatbot no pueda responder mi consulta para recibir una atención más específica y personalizada.

HU05 - Conversación en lenguaje natural
Como usuario, quiero escribir con mis propias palabras, sin usar comandos ni frases exactas, para que el chatbot entienda lo que necesito de forma natural.

HU06 - Gestión de consultas y estadísticas
Como administrador, quiero ver un registro de las conversaciones y las preguntas más frecuentes para identificar qué información falta y mejorar el servicio.

HU07 - Actualización de respuestas
Como administrador, quiero poder agregar o modificar las preguntas y respuestas del chatbot para mantener la información siempre actualizada.

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
Tamara Flores	______________________________
Luis Orlando Cartagena	Tests unitarios.
Lautaro Pereira Das Neves	______________________________
Reflexiones del equipo:

--- 

# Desafíos encontrados

Uno de los principales desafíos durante el desarrollo fue
______________________________________________________________.

También encontramos dificultades relacionadas con
______________________________________________________________.

Aprendizajes

Durante el proyecto aprendimos _____________________________________
______________________________________________________________.

Además, pudimos profundizar nuestros conocimientos sobre
______________________________________________________________.

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

Componente	Estado
Historias de usuario	☐ Pendiente / ☐ En progreso / ☐ Terminado
Tarjetas CRC	☐ Pendiente / ☐ En progreso / ☐ Terminado
Diagrama UML	☐ Pendiente / ☐ En progreso / ☐ Terminado
Clases de dominio	☐ Pendiente / ☐ En progreso / ☐ Terminado
Pruebas unitarias	☐ Pendiente / ☐ En progreso / ☐ Terminado
Trello	☐ Pendiente / ☐ En progreso / ☐ Terminado
README	☐ Pendiente / ☐ En progreso / ☐ Terminado

