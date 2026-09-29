> ## Tarjeta CRC: Usuario

**Responsabilidades:**
- Enviar mensajes o consultas al chatbot
- Iniciar una conversación
- Recibir las respuestas generadas por el chatbot

**Colaboradores:**
- Conversación
- Mensaje




---

> ## Tarjeta CRC: Administrador

**Responsabilidades:**
- Agregar y modificar preguntas frecuentes
- Consultar estadísticas de las conversaciones
- Gestionar la información del chatbot

**Colaboradores:**
- PreguntaFrecuente
- Conversación

---

> ## Tarjeta CRC: Conversación

**Responsabilidades:**
- Registrar los mensajes intercambiados entre el usuario y el chatbot
- Mantener el estado de la conversación (activa, finalizada, derivada)
- Solicitar una derivación cuando el chatbot no puede responder lo que se le solicita/pregunta.

**Colaboradores:**
- Usuario
- Mensaje
- Derivación

---

> ## Tarjeta CRC: Mensaje

**Responsabilidades:**
- Almacenar el contenido enviado por el usuario o el chatbot
- Registrar la fecha y hora del envío
- Asociarse a una conversación específica

**Colaboradores:**
- Conversación
- PreguntaFrecuente

---

> ## Tarjeta CRC: PreguntaFrecuente

**Responsabilidades:**
- Almacenar una pregunta y su respuesta asociada
- Proveer la respuesta correspondiente cuando se detecta una coincidencia con el mensaje del usuario

**Colaboradores:**
- Mensaje
- Administrador

---

> ## Tarjeta CRC: Derivación

**Responsabilidades:**
- Registrar el traspaso de una conversación a un agente humano
- Indicar el motivo de la derivación
- Notificar al agente correspondiente

**Colaboradores:**
- Conversación
- Usuario