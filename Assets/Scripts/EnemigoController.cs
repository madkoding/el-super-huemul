using UnityEngine;
using ElSuperHuemul;
using ElSuperHuemul.Game;

namespace JuegoAIEP
{
	public class EnemigoController : MonoBehaviour
	{
		[Header("Configuración")]
		public float velocidad = 2f;
		public float rangoDeteccion = 5f;
		public Transform player;

		[Header("Ground Check")]
		public Transform groundCheck;
		public float groundCheckRadius = 0.2f;
		public LayerMask groundLayerMask;
		public Transform edgeCheck; // Para detectar bordes de plataformas

		private Rigidbody2D rb;
		private SpriteRenderer spriteRenderer;
		private Vector2 direccionMovimiento;
		private bool enSuelo;

		void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			spriteRenderer = GetComponent<SpriteRenderer>();

			if (spriteRenderer == null)
			{
				Debug.LogWarning($"El objeto {gameObject.name} no tiene un componente SpriteRenderer.");
			}

			// Buscar al jugador si no está asignado
			if (player == null)
			{
				GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
				if (playerObj != null)
				{
					player = playerObj.transform;
				}
			}

			// Crear ground check si no existe
			if (groundCheck == null)
			{
				GameObject groundCheckObj = new GameObject("EnemyGroundCheck");
				groundCheckObj.transform.SetParent(transform);
				groundCheckObj.transform.localPosition = new Vector3(0, -0.6f, 0);
				groundCheck = groundCheckObj.transform;
			}

			// Crear edge check si no existe
			if (edgeCheck == null)
			{
				GameObject edgeCheckObj = new GameObject("EnemyEdgeCheck");
				edgeCheckObj.transform.SetParent(transform);
				edgeCheckObj.transform.localPosition = new Vector3(0.5f, -0.6f, 0); // Adelante del enemigo
				edgeCheck = edgeCheckObj.transform;
			}

			// Dirección inicial aleatoria
			direccionMovimiento = Random.Range(0, 2) == 0 ? Vector2.left : Vector2.right;
		}
		void Update()
		{
			// Detección de borde SIMPLE pero efectiva
			float rayDistance = 1.0f;
			Vector2 rayOrigin = transform.position;

			// Raycast hacia abajo en la dirección de movimiento
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin + Vector2.right * direccionMovimiento.x * 0.6f, Vector2.down, rayDistance, groundLayerMask);

			// Si NO hay suelo delante, cambiar dirección
			if (hit.collider == null)
			{
				direccionMovimiento.x *= -1;
				Debug.Log("Enemigo: No hay suelo delante, cambiando dirección");
			}

			if (player != null)
			{
				float distanciaAlJugador = Vector2.Distance(transform.position, player.position);

				// Si el jugador está en rango, moverse hacia él (pero respetando bordes)
				if (distanciaAlJugador <= rangoDeteccion)
				{
					Vector2 direccionAlJugador = (player.position - transform.position).normalized;
					direccionMovimiento = new Vector2(direccionAlJugador.x, 0);
				}
				else
				{
					// Patrulla aleatoria
					if (Random.Range(0f, 1f) < 0.01f) // 1% de posibilidad de cambiar dirección cada frame
					{
						direccionMovimiento.x *= -1;
					}
				}
			}

			// Voltear sprite según dirección
			if (direccionMovimiento.x > 0)
			{
				spriteRenderer.flipX = false;
			}
			else if (direccionMovimiento.x < 0)
			{
				spriteRenderer.flipX = true;
			}
		}
		void FixedUpdate()
		{
			// Verificar si está en el suelo
			enSuelo = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);

			// Solo moverse si está en el suelo
			if (enSuelo)
			{
				// Aplicar movimiento
				rb.linearVelocity = new Vector2(direccionMovimiento.x * velocidad, rb.linearVelocity.y);
			}
			else
			{
				// Si no está en suelo, detener movimiento y cambiar dirección
				rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
				direccionMovimiento.x *= -1;
				Debug.Log("Enemigo: No está en suelo, deteniendo y cambiando dirección");
			}
		}
		void OnDrawGizmosSelected()
		{
			// Dibujar rango de detección del jugador
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, rangoDeteccion);

			// Dibujar ground check
			if (groundCheck != null)
			{
				Gizmos.color = enSuelo ? Color.green : Color.red;
				Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
			}

			// Dibujar raycast de detección de borde
			Vector2 rayOrigin = transform.position;
			Vector2 rayEnd = rayOrigin + Vector2.right * direccionMovimiento.x * 0.6f + Vector2.down * 1.0f;

			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(rayOrigin + Vector2.right * direccionMovimiento.x * 0.6f, rayEnd);

			// Dibujar dirección
			Gizmos.color = Color.white;
			Vector3 direction = new Vector3(direccionMovimiento.x * 1f, 0, 0);
			Gizmos.DrawLine(transform.position, transform.position + direction);
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			// Cambiar dirección al chocar con obstáculos
			if (collision.gameObject.CompareTag("Plataforma") || collision.gameObject.CompareTag("Wall"))
			{
				direccionMovimiento.x *= -1;
			}

			// Verificar si el objeto con el que colisionamos es el jugador
			if (collision.gameObject.CompareTag("Player1"))
			{
				Debug.Log("Enemigo: Colisión con el jugador detectada");

				// Reproducir sonido de colisión con el jugador
				if (AudioManager.Instance != null)
				{
					AudioManager.Instance.ReproducirSonidoEnemigo();
				}

				// Llamar al método PerderVida del GameManager
				if (GameManager.Instance != null)
				{
					GameManager.Instance.PerderVida();
				}
			}
		}
	}
}
