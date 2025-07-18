using UnityEngine;
using ElSuperHuemul.Game;

namespace ElSuperHuemul
{
	public class FallDetector : MonoBehaviour
	{
		[Header("Configuración")]
		public float alturaMinima = -10f; // Altura mínima antes de considerar caída
		public LayerMask groundLayerMask;

		private Transform jugador;
		private bool yaReprodujoSonidoCaida = false;

		void Start()
		{
			// Buscar al jugador
			GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
			if (playerObj != null)
			{
				jugador = playerObj.transform;
			}
		}

		void Update()
		{
			if (jugador != null)
			{
				// Verificar si el jugador está cayendo muy abajo
				if (jugador.position.y < alturaMinima)
				{
					if (!yaReprodujoSonidoCaida)
					{
						// Reproducir sonido de caída
						if (AudioManager.Instance != null)
						{
							AudioManager.Instance.ReproducirSonidoCaida();
						}
						yaReprodujoSonidoCaida = true;
					}

					// Perder vida por caída
					if (GameManager.Instance != null)
					{
						GameManager.Instance.PerderVida();
					}
				}
				else
				{
					yaReprodujoSonidoCaida = false;
				}
			}
		}
	}
}
