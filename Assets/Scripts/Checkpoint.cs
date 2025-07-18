using UnityEngine;
using ElSuperHuemul.Game;

namespace ElSuperHuemul.Game
{
	public class Checkpoint : MonoBehaviour
	{
		[Header("Checkpoint")]
		public bool esCheckpointActivo = false;

		[Header("Efectos Visuales")]
		public GameObject efectoActivacion;
		public Color colorInactivo = Color.gray;
		public Color colorActivo = Color.green;

		[Header("Sonido")]
		public AudioClip sonidoActivacion;

		private SpriteRenderer spriteRenderer;
		private AudioSource audioSource;
		private bool yaActivado = false;

		void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			audioSource = GetComponent<AudioSource>();

			if (spriteRenderer != null)
			{
				spriteRenderer.color = colorInactivo;
			}

			if (audioSource == null)
			{
				audioSource = gameObject.AddComponent<AudioSource>();
			}
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player1") && !yaActivado)
			{
				ActivarCheckpoint();
			}
		}

		void ActivarCheckpoint()
		{
			yaActivado = true;
			esCheckpointActivo = true;

			// Cambiar color visual
			if (spriteRenderer != null)
			{
				spriteRenderer.color = colorActivo;
			}

			// Mostrar efecto
			if (efectoActivacion != null)
			{
				Instantiate(efectoActivacion, transform.position, Quaternion.identity);
			}

			// Reproducir sonido
			if (sonidoActivacion != null && audioSource != null)
			{
				audioSource.PlayOneShot(sonidoActivacion);
			}

			// Registrar este checkpoint en el GameManager
			GameManager.Instance.EstablecerCheckpoint(transform.position);

			Debug.Log("Checkpoint activado en: " + transform.position);
		}

		public void DesactivarCheckpoint()
		{
			esCheckpointActivo = false;
			if (spriteRenderer != null)
			{
				spriteRenderer.color = colorInactivo;
			}
		}
	}
}
