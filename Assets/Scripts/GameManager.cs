using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using ElSuperHuemul;

namespace ElSuperHuemul.Game
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;

		[Header("UI")]
		public TextMeshProUGUI textoPuntos;
		public TextMeshProUGUI textoTiempo;
		public TextMeshProUGUI textoVidas;

		[Header("Configuración")]
		public float tiempoJuego = 60f; // 60 segundos de juego
		public int vidasIniciales = 3;

		[Header("Sistema de Vidas y Checkpoints")]
		public Transform jugador;
		public float tiempoInvulnerabilidad = 2f;

		private int puntosActuales = 0;
		private float tiempoRestante;
		private bool juegoActivo = true;
		private int vidasActuales;
		private Vector3 posicionCheckpoint;
		private bool enCheckpoint = false;
		private bool invulnerable = false;

		void Awake()
		{
			// Singleton pattern
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		void Start()
		{
			tiempoRestante = tiempoJuego;
			vidasActuales = vidasIniciales;

			// Establecer posición inicial como checkpoint
			if (jugador != null)
			{
				posicionCheckpoint = jugador.position;
			}

			// Iniciar música de juego
			if (AudioManager.Instance != null)
			{
				AudioManager.Instance.ReproducirMusicaJuego();
			}

			ActualizarUI();
		}

		void Update()
		{
			if (juegoActivo)
			{
				// Actualizar timer
				tiempoRestante -= Time.deltaTime;

				if (tiempoRestante <= 0)
				{
					tiempoRestante = 0;
					GameOver();
				}

				ActualizarUI();
			}

			// Reiniciar juego
			Keyboard keyboard = Keyboard.current;
			if (keyboard != null)
			{
				if (keyboard.rKey.wasPressedThisFrame)
				{
					ReiniciarJuego();
				}

				// Volver al menú
				if (keyboard.escapeKey.wasPressedThisFrame)
				{
					VolverAlMenu();
				}
			}
		}

		public void ActualizarPuntos(int nuevosPuntos)
		{
			puntosActuales = nuevosPuntos;
			ActualizarUI();
		}

		void ActualizarUI()
		{
			if (textoPuntos != null)
			{
				textoPuntos.text = "Puntos: " + puntosActuales;
			}

			if (textoTiempo != null)
			{
				int minutos = Mathf.FloorToInt(tiempoRestante / 60);
				int segundos = Mathf.FloorToInt(tiempoRestante % 60);
				textoTiempo.text = string.Format("Tiempo: {0:00}:{1:00}", minutos, segundos);
			}

			if (textoVidas != null)
			{
				textoVidas.text = "Vidas: " + vidasActuales;
			}
		}

		public void GameOver()
		{
			juegoActivo = false;

			// Reproducir sonido de Game Over
			if (AudioManager.Instance != null)
			{
				AudioManager.Instance.ReproducirSonidoGameOver();
			}

			// Guardar puntuación
			PlayerPrefs.SetInt("UltimaPuntuacion", puntosActuales);
			PlayerPrefs.Save();

			// Cargar escena de Game Over
			SceneManager.LoadScene("GameOver");
		}

		public void EstablecerCheckpoint(Vector3 nuevaPosicion)
		{
			posicionCheckpoint = nuevaPosicion;
			enCheckpoint = true;
			Debug.Log("Checkpoint establecido en: " + nuevaPosicion);
		}

		public void PerderVida()
		{
			if (invulnerable || !juegoActivo)
				return;

			vidasActuales--;
			ActualizarUI();

			if (vidasActuales <= 0)
			{
				GameOver();
			}
			else
			{
				// Salto hacia atrás
				if (jugador != null)
				{
					Rigidbody2D rb = jugador.GetComponent<Rigidbody2D>();
					if (rb != null)
					{
						Vector2 fuerzaRetroceso = new Vector2(-2f, 2f); // Ajusta los valores según sea necesario
						rb.AddForce(fuerzaRetroceso, ForceMode2D.Impulse);
					}
				}

				// Respawn en checkpoint
				RespawnEnCheckpoint();
			}
		}

		void RespawnEnCheckpoint()
		{
			if (jugador != null)
			{
				// Mover jugador al checkpoint
				jugador.position = posicionCheckpoint;

				// Activar invulnerabilidad temporal
				StartCoroutine(InvulnerabilidadTemporal());

				// Resetear física del jugador
				Rigidbody2D rb = jugador.GetComponent<Rigidbody2D>();
				if (rb != null)
				{
					rb.linearVelocity = Vector2.zero;
				}

				Debug.Log("Respawn en checkpoint: " + posicionCheckpoint);
			}
		}

		System.Collections.IEnumerator InvulnerabilidadTemporal()
		{
			invulnerable = true;

			// Efecto visual de parpadeo
			SpriteRenderer spriteRenderer = jugador.GetComponent<SpriteRenderer>();
			if (spriteRenderer != null)
			{
				Debug.Log("SpriteRenderer encontrado en el jugador.");
				float tiempoParpadeo = 0.05f; // Ajustar duración del parpadeo
				float tiempoTranscurrido = 0f;

				while (tiempoTranscurrido < tiempoInvulnerabilidad)
				{
					spriteRenderer.enabled = false; // Ocultar sprite
					Debug.Log("Jugador invisible.");
					yield return new WaitForSeconds(tiempoParpadeo);
					spriteRenderer.enabled = true; // Mostrar sprite
					Debug.Log("Jugador visible.");
					yield return new WaitForSeconds(tiempoParpadeo);
					tiempoTranscurrido += tiempoParpadeo * 2;
				}

				spriteRenderer.enabled = true; // Asegurarse de que el sprite esté visible al final
				Debug.Log("Jugador visible al final de la invulnerabilidad.");
			}
			else
			{
				Debug.LogWarning("El jugador no tiene un componente SpriteRenderer.");
				yield return new WaitForSeconds(tiempoInvulnerabilidad);
			}

			invulnerable = false;
			Debug.Log("Invulnerabilidad terminada");
		}

		public bool EstaInvulnerable()
		{
			return invulnerable;
		}

		public static void ReiniciarJuego()
		{
			SceneManager.LoadScene("Juego");
		}

		public static void VolverAlMenu()
		{
			SceneManager.LoadScene("Inicio");
		}

		public static void SalirDelJuego()
		{
			Application.Quit();
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			// Verificar si el objeto con el que colisionamos es un enemigo
			if (collision.gameObject.CompareTag("Enemigo"))
			{
				PerderVida();

				// Reproducir sonido de colisión con enemigo
				if (AudioManager.Instance != null)
				{
					AudioManager.Instance.ReproducirSonidoEnemigo();
				}
			}
		}
	}
}
