using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ElSuperHuemul;
using ElSuperHuemul.Game;

namespace ElSuperHuemul
{
	public class PlayerController : MonoBehaviour
	{
		[Header("Movimiento")]
		public float velocidadMovimiento = 5f;
		public float fuerzaSalto = 4f;

		[Header("Ground Check")]
		public Transform groundCheck;
		public float groundCheckRadius = 0.2f;
		public LayerMask groundLayerMask;

		private Rigidbody2D rb;
		private bool enSuelo;
		private float inputHorizontal;
		private SpriteRenderer spriteRenderer;
		private Animator animator;

		// Puntuación
		public int puntos = 0;

		void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			spriteRenderer = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();

			// Crear ground check si no existe
			if (groundCheck == null)
			{
				GameObject groundCheckObj = new GameObject("GroundCheck");
				groundCheckObj.transform.SetParent(transform);
				groundCheckObj.transform.localPosition = new Vector3(0, -0.7f, 0); // Más abajo
				groundCheck = groundCheckObj.transform;
			}
		}

		void Update()
		{
			// Input de movimiento WASD + Flechas (New Input System)
			inputHorizontal = 0f;

			Keyboard keyboard = Keyboard.current;
			if (keyboard != null)
			{
				// WASD y Flechas
				if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
				{
					inputHorizontal = 1f; // Derecha
				}
				else if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
				{
					inputHorizontal = -1f; // Izquierda
				}

				// Salto con W, Espacio o Flecha Arriba
				if (keyboard.wKey.wasPressedThisFrame || keyboard.spaceKey.wasPressedThisFrame || keyboard.upArrowKey.wasPressedThisFrame)
				{
					Debug.Log("Tecla de salto presionada. En suelo: " + enSuelo);
					if (enSuelo)
					{
						Saltar();
						Debug.Log("¡Saltando!");
					}
					else
					{
						Debug.Log("No puede saltar - no está en el suelo");
					}
				}
			}

			// Voltear sprite según dirección
			if (inputHorizontal > 0)
			{
				spriteRenderer.flipX = false;
			}
			else if (inputHorizontal < 0)
			{
				spriteRenderer.flipX = true;
			}
		}

		void FixedUpdate()
		{
			// Movimiento horizontal
			rb.linearVelocity = new Vector2(inputHorizontal * velocidadMovimiento, rb.linearVelocity.y);    // Verificar si está en el suelo (más preciso)
			bool enSueloAnterior = enSuelo;
			enSuelo = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);

			// Debug solo cuando cambia el estado
			if (enSuelo != enSueloAnterior)
			{
				Debug.Log("Estado del suelo cambió a: " + enSuelo);
			}
		}

		void Saltar()
		{
			// Solo saltar si realmente está en el suelo
			if (enSuelo)
			{
				rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);

				// Reproducir sonido de salto
				if (AudioManager.Instance != null)
				{
					AudioManager.Instance.ReproducirSonidoSalto();
				}

				Debug.Log("Salto ejecutado con fuerza: " + fuerzaSalto);
			}
			else
			{
				Debug.Log("Intento de salto bloqueado - no está en suelo");
			}
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			// Recoger estrella
			if (other.CompareTag("Estrella"))
			{
				puntos += 10;
				GameManager.Instance.ActualizarPuntos(puntos);

				// Reproducir sonido de estrella
				if (AudioManager.Instance != null)
				{
					AudioManager.Instance.ReproducirSonidoEstrella();
				}

				Destroy(other.gameObject);
				Debug.Log("Estrella recogida! Puntos: " + puntos);
			}

			// Colisión con enemigo - Perder vida
			if (other.CompareTag("Enemigo") && !GameManager.Instance.EstaInvulnerable())
			{
				// Reproducir sonido de enemigo
				if (AudioManager.Instance != null)
				{
					AudioManager.Instance.ReproducirSonidoEnemigo();
				}

				Debug.Log("¡Colisión con enemigo! Perdiendo una vida");
				GameManager.Instance.PerderVida();
			}
		}

		// También detectar colisiones físicas con enemigos
		public static void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Enemigo") && !GameManager.Instance.EstaInvulnerable())
			{
				// Reproducir sonido de enemigo
				if (AudioManager.Instance != null)
				{
					AudioManager.Instance.ReproducirSonidoEnemigo();
				}

				Debug.Log("¡Colisión física con enemigo! Perdiendo una vida");
				GameManager.Instance.PerderVida();
			}
		}

		void OnDrawGizmosSelected()
		{
			if (groundCheck != null)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
			}
		}
	}
}
