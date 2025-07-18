# 🦌 El Super Huemul

Un juego de plataformas 2D desarrollado en Unity donde controlas a un huemul que debe recolectar estrellas mientras evita enemigos y supera obstáculos.

## 🎮 Características del Juego

### Mecánicas Principales
- **Movimiento**: Controla al huemul con WASD o flechas direccionales
- **Salto**: Espacio o W para saltar
- **Recolección**: Recoge estrellas para aumentar tu puntuación
- **Sistema de Vidas**: 3 vidas antes del Game Over
- **Checkpoints**: Puntos de control que guardan tu progreso
- **Límite de Tiempo**: 60 segundos para completar el nivel

### Sistema de Vidas y Checkpoints
- **Vidas Iniciales**: 3 vidas por defecto
- **Respawn**: Al perder una vida, reapareces en el último checkpoint
- **Invulnerabilidad**: 2 segundos de inmunidad tras respawn
- **Checkpoints Automáticos**: Se activan al tocarlos y cambian de color
- **Zonas Peligrosas**: Áreas que quitan vidas al contacto

## 🕹️ Controles

| Acción | Teclado |
|--------|---------|
| Mover Izquierda | A / Flecha Izquierda |
| Mover Derecha | D / Flecha Derecha |
| Saltar | Espacio / W / Flecha Arriba |
| Reiniciar Juego | R |
| Volver al Menú | Escape |

## 🛠️ Instalación y Ejecución

### Requisitos
- Unity 2022.3 LTS o superior
- Sistema operativo: Windows, macOS, o Linux

### Ejecutar el Juego
1. Clona este repositorio
2. Abre el proyecto en Unity
3. Carga la escena `Inicio` desde `Assets/Scenes/`
4. Presiona Play o construye el juego

## 🏗️ Estructura del Proyecto

```
Assets/
├── Scenes/
│   ├── Inicio.unity          # Menú principal
│   ├── Juego.unity           # Escena principal del juego
│   └── GameOver.unity        # Pantalla de Game Over
├── Scripts/
│   ├── GameManager.cs        # Gestión principal del juego
│   ├── PlayerController.cs   # Controles del jugador
│   ├── Checkpoint.cs         # Sistema de checkpoints
│   ├── ZonaPeligrosa.cs      # Zonas que quitan vidas
│   ├── EnemigoController.cs  # Comportamiento de enemigos
│   ├── Estrella.cs           # Recolectables
│   └── MenuInicio.cs         # Menú principal
├── Sprites/                  # Texturas y sprites
└── Settings/                 # Configuraciones del proyecto
```

## 🎯 Scripts Principales

### GameManager.cs
Gestiona el estado global del juego:
- Control de vidas y puntuación
- Gestión de checkpoints
- Sistema de respawn
- Invulnerabilidad temporal
- Temporizador del juego

### PlayerController.cs
Controla el movimiento y acciones del jugador:
- Movimiento con WASD/Flechas
- Detección de suelo para saltos
- Recolección de estrellas
- Detección de colisiones con enemigos

### Checkpoint.cs
Sistema de puntos de control:
- Activación automática al contacto
- Efectos visuales (cambio de color)
- Comunicación con GameManager
- Prevención de reactivación

### ZonaPeligrosa.cs
Zonas que afectan al jugador:
- Zonas mortales (caídas)
- Zonas peligrosas (lava, espinas)
- Respeto a invulnerabilidad temporal

## 🎨 Configuración en Unity

### GameManager
1. Asignar el Transform del jugador en "Jugador"
2. Configurar TextMeshPro para UI:
   - Texto Puntos
   - Texto Tiempo  
   - Texto Vidas
3. Ajustar vidas iniciales y tiempo de invulnerabilidad

### Checkpoints
1. Crear GameObject con:
   - Collider2D (Is Trigger = true)
   - SpriteRenderer
   - Script Checkpoint
2. Configurar colores y efectos visuales
3. Asignar tag "Checkpoint" (opcional)

### Zonas Peligrosas
1. Crear GameObject con:
   - Collider2D (Is Trigger = true)
   - Script ZonaPeligrosa
2. Configurar si es mortal o peligrosa

## 🏷️ Tags Necesarios

- `Player` - Para el jugador
- `Enemigo` - Para los enemigos
- `Estrella` - Para los recolectables
- `Checkpoint` - Para los checkpoints (opcional)

## 🎮 Mecánicas de Juego

### Puntuación
- **Estrellas**: +10 puntos cada una
- **Puntuación Final**: Se guarda automáticamente

### Sistema de Vidas
- **Vidas Iniciales**: 3 (configurable)
- **Pérdida de Vida**: Colisión con enemigos o zonas peligrosas
- **Game Over**: Cuando se acaban todas las vidas

### Checkpoints
- **Activación**: Automática al contacto
- **Visual**: Cambio de color gris → verde
- **Respawn**: El jugador reaparece en el último checkpoint activo

### Invulnerabilidad
- **Duración**: 2 segundos tras respawn
- **Efecto Visual**: Parpadeo del jugador
- **Protección**: Inmunidad temporal a enemigos

## 🔧 Desarrollo

### Para Desarrolladores
El juego está estructurado con patrones de diseño claros:
- **Singleton**: GameManager para acceso global
- **Component Pattern**: Scripts especializados por funcionalidad
- **Observer Pattern**: Comunicación entre sistemas

### Extensibilidad
- Fácil agregar nuevos tipos de enemigos
- Sistema de checkpoints escalable
- UI modular y configurable

## 🐛 Solución de Problemas

### Problemas Comunes
1. **Jugador no respawnea**: Verificar que el GameManager tenga la referencia del jugador
2. **Checkpoints no funcionan**: Asegurar que tengan Collider2D como Trigger
3. **UI no se actualiza**: Verificar asignación de TextMeshPro en GameManager

### Debug
- Activar logs en la consola de Unity
- Verificar tags de GameObjects
- Comprobar configuración de Physics2D

## 📝 Créditos

Desarrollado con Unity 2022.3 LTS
- **Movimiento**: New Input System
- **UI**: TextMeshPro
- **Física**: Unity Physics2D

---

¡Disfruta jugando El Super Huemul! 🦌✨
