# PokéExplorer: Recolecta y Descubre ([Demo](https://sanquih.itch.io/pokexplorerrecolectadescubre))
Explora, recolecta y descubre Pokémon en un mundo interactivo en 3D.

## 📌 Descripción del Proyecto
Este es un videojuego de exploración desarrollado en Unity, donde el jugador debe recolectar ítems distribuidos en un escenario 3D. Cada ítem representa un Pokémon y, al recolectarlo, se obtiene información en tiempo real desde la PokéAPI. La información de los Pokémon recolectados se guarda en un inventario persistente, el cual el jugador puede consultar en cualquier momento.

## 🚀 Características Principales
✅ Exploración en un entorno 3D con un personaje jugable.  
✅ Recolección de ítems que contienen información sobre Pokémon.  
✅ Interfaz de usuario (UI Toolkit) con un inventario interactivo.  
✅ Consulta de detalles de los Pokémon atrapados.  
✅ Sistema de guardado para mantener el progreso del jugador.  
✅ Música y efectos de sonido que mejoran la experiencia de juego.  

## 🛠️ Implementación y Desarrollo
### 1️⃣ Creación del Escenario 3D
- Se optó por un diseño low poly para optimizar el rendimiento.
- El escenario es cuadrado y tiene 4 límites, cada uno construido con grupos de GameObjects que varían el paisaje.
- Se añadieron montañas detrás de los límites para mejorar la estética y optimizar el rendimiento.
- Se colocaron obstáculos dentro del escenario para hacer la exploración más dinámica.
#### 🛠️ Recursos utilizados:
- [Low Poly Simple Nature Pack]([https://pages.github.com/](https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-simple-nature-pack-162153)).
  
### 2️⃣ Implementación del Player
- Se usó un modelo 3D low poly con Rigidbody y Collider para la interacción física.
- Se agregó un Animator con estados básicos (idle y running).
- La movilidad se limitó a tres teclas (A, W, D) para facilitar el desarrollo.
#### 🛠️ Recurso utilizado:
- [Free Low Poly Human RPG Character](https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/free-low-poly-human-rpg-character-219979)
  
### 3️⃣ Implementación de los Ítems
- Se utilizaron modelos de huevos como representación de los ítems.
- Se definieron 5 prefabs distintos para dar variedad visual.
- Cada ítem tiene:
  - Trigger en el Mesh Collider para detectar colisiones con el jugador.
  - Particle System para hacerlos más visibles.
  - Script ItemController que maneja el estado del ítem y contiene el ID del Pokémon que será consultado en la API.
- Se creo el Script ItemsManager que gestiona los ítems globalmente y se comunica con el SaveManager.
#### 🛠️ Recurso utilizado:
- [Low Poly Bird Nests](https://assetstore.unity.com/packages/3d/props/low-poly-bird-nests-229812)
  
### 4️⃣ Implementación de la UI con UI Toolkit
- Se creó una interfaz principal con un contador de ítems recolectados.
- Se implementó un sistema de escalado de labels (label-little, label-normal, label-title) para ajustar los textos según el tamaño de la pantalla.
- Se desarrolló una interfaz de inventario con un ScrollView dinámico que muestra los ítems recolectados.
  
### 5️⃣ Consumo de la PokéAPI
- Se creó el script APIManager para manejar las peticiones a la API.
- Se definió la clase Pokemon para mapear los datos obtenidos.
- Se implementaron eventos Action para actualizar la UI cuando se recibieran los datos asincrónicamente.

### 6️⃣ Visualización de la Información de los Pokémon
- Se creó una interfaz para mostrar la información detallada del Pokémon seleccionado:
  - Sprites del Pokémon.
  - Nombre, tipo, peso y altura.

## 🎮 Mejoras Adicionales
✔️ Animación al mostrar el nombre del Pokémon cuando se recoge.  
✔️ Ayudas iniciales para que el usuario aprenda los controles.  
✔️ Opción para reiniciar el progreso (bug en el ejecutable, pero funcional en Unity).  
✔️ Música de fondo y efectos de sonido en 3D.  
✔️ Muros invisibles para evitar que el jugador salga del escenario.  
✔️ Cámara con seguimiento fluido del jugador.  
✔️ Mensaje final al recolectar todos los ítems.  
#### 🛠️ Recursos utilizados:
- [Nature Sound FX](https://assetstore.unity.com/packages/audio/sound-fx/nature-sound-fx-180413)

## 📷 Screenshots
![1](https://github.com/user-attachments/assets/5e62186c-cc3b-44d2-8566-5f7e679fc6b3)
![2](https://github.com/user-attachments/assets/3188274d-8980-45a8-9ee0-2e439742c606)
![3](https://github.com/user-attachments/assets/04328b8a-1029-4d4e-a93d-30f2c6507be8)
![4](https://github.com/user-attachments/assets/2bfdb998-e30e-41a7-a51f-cf6a524bbe94)

## 📂 Estructura del Proyecto
📁 Assets/  
┣ 📂 Audio/ (Música y efectos de sonido)  
┣ 📂 ExternalAssets/ (Assets de terceros)  
┣ 📂 Prefabs/ (Objetos reutilizables: ítems, jugador, entorno)  
┣ 📂 Scenes/ (Escenarios del juego)  
┣ 📂 Scripts/ (Código fuente)  
┣ 📂 Sprites/ (Icon del mouse en hover)  
┗ 📂 UI/ (Elementos de la interfaz de usuario)  

## 🔗 Créditos y Recursos Utilizados
- **Escenario:** [Low Poly Simple Nature Pack](https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-simple-nature-pack-162153)
- **Personaje:** [Free Low Poly Human RPG Character](https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/free-low-poly-human-rpg-character-219979)
- **Ítems:** [Low Poly Bird Nests](https://assetstore.unity.com/packages/3d/props/low-poly-bird-nests-229812)
- **Sonidos:** [Nature Sound FX](https://assetstore.unity.com/packages/audio/sound-fx/nature-sound-fx-180413)
- **API utilizada:** [PokéAPI](https://pokeapi.co/#google_vignette)
  
## 🖥️ Tecnologías Utilizadas
- Unity
- C#
- UI Toolkit
- PokéAPI
- Git/GitHub
  
## 📢 Notas Finales
Este proyecto fue desarrollado como parte de una prueba técnica para demostrar habilidades en desarrollo de videojuegos, programación en C#, consumo de APIs y diseño de UI en Unity.
