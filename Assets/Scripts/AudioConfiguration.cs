using UnityEngine;

namespace ElSuperHuemul.Audio
{
	[System.Serializable]
	public class AudioSettings
	{
		[Header("Configuración de Volumen")]
		[Range(0f, 1f)]
		public float volumenMaestro = 1f;
		[Range(0f, 1f)]
		public float volumenMusica = 0.5f;
		[Range(0f, 1f)]
		public float volumenEfectos = 0.7f;

		[Header("Configuración de Audio")]
		public bool musicaActivada = true;
		public bool efectosActivados = true;
	}

	public class AudioConfiguration : MonoBehaviour
	{
		[Header("Configuración Global")]
		public AudioSettings configuracionAudio = new AudioSettings();

		void Start()
		{
			// Cargar configuración guardada
			CargarConfiguracion();

			// Aplicar configuración al AudioManager
			if (AudioManager.Instance != null)
			{
				AudioManager.Instance.CambiarVolumenMusica(configuracionAudio.volumenMusica);
				AudioManager.Instance.CambiarVolumenEfectos(configuracionAudio.volumenEfectos);

				if (!configuracionAudio.musicaActivada || !configuracionAudio.efectosActivados)
				{
					AudioManager.Instance.SilenciarTodo();
				}
			}
		}

		void CargarConfiguracion()
		{
			configuracionAudio.volumenMaestro = PlayerPrefs.GetFloat("VolumenMaestro", 1f);
			configuracionAudio.volumenMusica = PlayerPrefs.GetFloat("VolumenMusica", 0.5f);
			configuracionAudio.volumenEfectos = PlayerPrefs.GetFloat("VolumenEfectos", 0.7f);
			configuracionAudio.musicaActivada = PlayerPrefs.GetInt("MusicaActivada", 1) == 1;
			configuracionAudio.efectosActivados = PlayerPrefs.GetInt("EfectosActivados", 1) == 1;
		}

		public void GuardarConfiguracion()
		{
			PlayerPrefs.SetFloat("VolumenMaestro", configuracionAudio.volumenMaestro);
			PlayerPrefs.SetFloat("VolumenMusica", configuracionAudio.volumenMusica);
			PlayerPrefs.SetFloat("VolumenEfectos", configuracionAudio.volumenEfectos);
			PlayerPrefs.SetInt("MusicaActivada", configuracionAudio.musicaActivada ? 1 : 0);
			PlayerPrefs.SetInt("EfectosActivados", configuracionAudio.efectosActivados ? 1 : 0);
			PlayerPrefs.Save();
		}

		public void CambiarVolumenMusica(float nuevoVolumen)
		{
			configuracionAudio.volumenMusica = nuevoVolumen;
			if (AudioManager.Instance != null)
			{
				AudioManager.Instance.CambiarVolumenMusica(nuevoVolumen);
			}
			GuardarConfiguracion();
		}

		public void CambiarVolumenEfectos(float nuevoVolumen)
		{
			configuracionAudio.volumenEfectos = nuevoVolumen;
			if (AudioManager.Instance != null)
			{
				AudioManager.Instance.CambiarVolumenEfectos(nuevoVolumen);
			}
			GuardarConfiguracion();
		}

		public void AlternarMusica()
		{
			configuracionAudio.musicaActivada = !configuracionAudio.musicaActivada;

			if (AudioManager.Instance != null)
			{
				if (configuracionAudio.musicaActivada)
				{
					AudioManager.Instance.DesactivarSilencio();
				}
				else
				{
					AudioManager.Instance.DetenerMusica();
				}
			}

			GuardarConfiguracion();
		}

		public void AlternarEfectos()
		{
			configuracionAudio.efectosActivados = !configuracionAudio.efectosActivados;
			GuardarConfiguracion();
		}
	}
}
