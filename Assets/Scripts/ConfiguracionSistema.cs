/*
INSTRUCCIONES DE CONFIGURACIÓN - SISTEMA DE VIDAS Y CHECKPOINTS

Este sistema implementa:
1. Sistema de 3 vidas (configurable)
2. Checkpoints que guardan la posición del jugador
3. Respawn en el último checkpoint al perder una vida
4. Invulnerabilidad temporal tras respawn
5. Zonas peligrosas que quitan vidas

CONFIGURACIÓN EN UNITY:

1. GAMEMANAGER:
   - Asignar el Transform del jugador en "Jugador"
   - Configurar "Vidas Iniciales" (por defecto 3)
   - Configurar "Tiempo Invulnerabilidad" (por defecto 2 segundos)
   - Asignar TextMeshPro para mostrar vidas en "Texto Vidas"

2. CHECKPOINTS:
   - Crear GameObject con tag "Checkpoint"
   - Agregar Collider2D como Trigger
   - Agregar script "Checkpoint"
   - Configurar colores y efectos visuales

3. ZONAS PELIGROSAS:
   - Crear GameObject con Collider2D como Trigger
   - Agregar script "ZonaPeligrosa"
   - Configurar si es mortal o solo peligrosa

4. UI:
   - Agregar TextMeshPro para mostrar vidas
   - Asignar al GameManager

TAGS NECESARIOS:
- Player (para el jugador)
- Checkpoint (para los checkpoints)
- Enemigo (para los enemigos)
- Estrella (para las estrellas/puntos)

LAYERS RECOMENDADOS:
- Player
- Enemies
- Checkpoints
- Hazards (zonas peligrosas)
*/
