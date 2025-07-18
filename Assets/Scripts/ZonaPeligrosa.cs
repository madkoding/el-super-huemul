using UnityEngine;
using ElSuperHuemul.Game;

namespace ElSuperHuemul.Game
{
	public class ZonaPeligrosa : MonoBehaviour
	{
		[Header("Configuración")]
		public bool esZonaMortal = true; // Si es true, mata al jugador, si es false, solo le quita una vida

		void OnTriggerEnter2D(Collider2D other)
		{
			Debug.Log($"OnTriggerEnter2D activado por: {other.name}");

			if (other.CompareTag("Player1"))
			{
				if (esZonaMortal)
				{
					// Zona mortal (como caída al vacío)
					Debug.Log("¡Jugador cayó en zona mortal!");
					GameManager.Instance.PerderVida();
				}
				else
				{
					// Zona peligrosa (como lava, espinas, etc.)
					if (!GameManager.Instance.EstaInvulnerable())
					{
						Debug.Log("¡Jugador tocó zona peligrosa!");
						GameManager.Instance.PerderVida();
					}
				}
			}
		}
	}
}
