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

El modelo del dominio fue desarrollado a partir de las historias de
usuario identificadas para el proyecto.

Las principales clases identificadas para el dominio son:

- Usuario: representa a la persona que interactúa con el chatbot para hacer consultas u obtener ayuda.
- Administrador: representa a la persona encargada de gestionar el contenido del chatbot y revisar las estadísticas de uso.
- Conversación: representa cada sesión de diálogo entre un usuario y el chatbot, e incluye su fecha, su estado y los mensajes intercambiados.
- Mensaje: representa cada texto enviado, ya sea por el usuario o por el chatbot, dentro de una conversación.
- PreguntaFrecuente: representa una pregunta con su respuesta asociada, que forma parte de la base de conocimiento del chatbot.
- Derivación: representa el traspaso de una conversación a un agente humano cuando el chatbot no puede resolver la consulta.

Estas clases permiten representar a las personas que participan en el sistema, el desarrollo de las conversaciones y la información que el chatbot utiliza para responder. De esta forma, el modelo cubre las funcionalidades principales del proyecto: responder consultas, guiar a los usuarios, derivarlos a una persona cuando sea necesario y mantener actualizada la información del sistema.

---

# Diagrama de clases UML


[Ver diagrama de clases](docs/diagrama-clases.png)


---

# Tarjetas CRC

Las tarjetas CRC utilizadas para el modelado del dominio se encuentran
en:

[Ver tarjetas CRC](docs/tarjetas-crc.md)

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
link del trello

Tablero de Trello del proyecto

Organización del tablero

El tablero de Trello se organiza en las siguientes columnas:

TODO: tareas pendientes de realizar.
WIP: tareas que se encuentran actualmente en progreso.
DONE: tareas finalizadas.

Cada tarea incluye:

Responsable.
Fecha prevista de finalización.
Distribución de tareas

Integrante	Tarea / responsabilidad
Jeremías González	______________________________
Tamara Flores	______________________________
Luis Orlando Cartagena	______________________________
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

