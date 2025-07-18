using UnityEngine;
using ElSuperHuemul;

namespace ElSuperHuemul
{
	[System.Serializable]
	public class AudioManagerSetup : MonoBehaviour
	{
		[Header("Configuración Automática")]
		[SerializeField] private bool configurarAutomaticamente = true;

		[Header("Prefabs de Audio")]
		public GameObject audioManagerPrefab;

		void Awake()
		{
			// Buscar si ya existe un AudioManager en la escena
			AudioManager existingAudioManager = FindFirstObjectByType<AudioManager>();

			if (existingAudioManager == null && configurarAutomaticamente)
			{
				CrearAudioManager();
			}
		}

		static void CrearAudioManager()
		{
			// Crear el GameObject del AudioManager
			GameObject audioManagerObj = new GameObject("AudioManager");

			// Agregar los AudioSources
			AudioSource musicaSource = audioManagerObj.AddComponent<AudioSource>();
			AudioSource efectosSource = audioManagerObj.AddComponent<AudioSource>();

			// Configurar el AudioSource de música
			musicaSource.loop = true;
			musicaSource.playOnAwake = false;
			musicaSource.volume = 0.5f;

			// Configurar el AudioSource de efectos
			efectosSource.loop = false;
			efectosSource.playOnAwake = false;
			efectosSource.volume = 0.7f;
		}

		[ContextMenu("Crear AudioManager")]
		public void CrearAudioManagerManual()
		{
			CrearAudioManager();
		}
	}
}
