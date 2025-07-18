using UnityEngine;

namespace ElSuperHuemul
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Audio Sources")]
        public AudioSource musicaSource;
        public AudioSource efectosSource;

        [Header("Música de Fondo")]
        public AudioClip musicaJuego;
        public AudioClip musicaMenu;
        public AudioClip musicaGameOver;
        [Range(0f, 1f)]
        public float volumenMusica = 0.5f;

        [Header("Efectos de Sonido")]
        public AudioClip sonidoSalto;
        public AudioClip sonidoEstrella;
        public AudioClip sonidoEnemigo;
        public AudioClip sonidoCaida;
        public AudioClip sonidoGameOver;
        public AudioClip sonidoVictoria;
        [Range(0f, 1f)]
        public float volumenEfectos = 0.7f;

        void Awake()
        {
            // Singleton pattern
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            // Configurar volúmenes
            if (musicaSource != null)
            {
                musicaSource.volume = volumenMusica;
            }
            if (efectosSource != null)
            {
                efectosSource.volume = volumenEfectos;
            }

            // Reproducir música del menú al iniciar
            ReproducirMusicaMenu();
        }

        #region Música
        public void ReproducirMusicaMenu()
        {
            if (musicaMenu != null && musicaSource != null)
            {
                musicaSource.Stop();
                musicaSource.clip = musicaMenu;
                musicaSource.loop = true;
                musicaSource.Play();
            }
        }

        public void ReproducirMusicaJuego()
        {
            if (musicaJuego != null && musicaSource != null)
            {
                musicaSource.Stop();
                musicaSource.clip = musicaJuego;
                musicaSource.loop = true;
                musicaSource.Play();
            }
        }

        public void ReproducirMusicaGameOver()
        {
            if (musicaGameOver != null && musicaSource != null)
            {
                musicaSource.Stop();
                musicaSource.clip = musicaGameOver;
                musicaSource.loop = false;
                musicaSource.Play();
            }
        }

        public void DetenerMusica()
        {
            if (musicaSource != null)
            {
                musicaSource.Stop();
            }
        }

        public void CambiarVolumenMusica(float nuevoVolumen)
        {
            volumenMusica = Mathf.Clamp01(nuevoVolumen);
            if (musicaSource != null)
            {
                musicaSource.volume = volumenMusica;
            }
        }
        #endregion

        #region Efectos de Sonido
        public void ReproducirSonidoSalto()
        {
            ReproducirEfecto(sonidoSalto);
        }

        public void ReproducirSonidoEstrella()
        {
            ReproducirEfecto(sonidoEstrella);
        }

        public void ReproducirSonidoEnemigo()
        {
            ReproducirEfecto(sonidoEnemigo);
        }

        public void ReproducirSonidoCaida()
        {
            ReproducirEfecto(sonidoCaida);
        }

        public void ReproducirSonidoGameOver()
        {
            ReproducirEfecto(sonidoGameOver);
        }

        public void ReproducirSonidoVictoria()
        {
            ReproducirEfecto(sonidoVictoria);
        }

        private void ReproducirEfecto(AudioClip clip)
        {
            if (clip != null && efectosSource != null)
            {
                efectosSource.PlayOneShot(clip);
                Debug.Log("Reproduciendo efecto de sonido: " + clip.name);
            }
            else
            {
                Debug.LogWarning("No se pudo reproducir el efecto de sonido. Clip o efectosSource es nulo.");
            }
        }

        public void CambiarVolumenEfectos(float nuevoVolumen)
        {
            volumenEfectos = Mathf.Clamp01(nuevoVolumen);
            if (efectosSource != null)
            {
                efectosSource.volume = volumenEfectos;
            }
        }
        #endregion

        #region Configuración General
        public void SilenciarTodo()
        {
            if (musicaSource != null)
            {
                musicaSource.mute = true;
            }
            if (efectosSource != null)
            {
                efectosSource.mute = true;
            }
        }

        public void DesactivarSilencio()
        {
            if (musicaSource != null)
            {
                musicaSource.mute = false;
            }
            if (efectosSource != null)
            {
                efectosSource.mute = false;
            }
        }
        #endregion
    }
}
