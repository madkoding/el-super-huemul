using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using static ElSuperHuemul.AudioManager;
using ElSuperHuemul;

namespace ElSuperHuemul
{
    public class MenuInicio : MonoBehaviour
    {
        [Header("UI")]
        public Button botonJugar;
        public Button botonSalir;
        public TextMeshProUGUI textoMejorPuntuacion;

        void Start()
        {
            // Configurar botones
            if (botonJugar != null)
            {
                botonJugar.onClick.AddListener(IniciarJuego);
            }

            if (botonSalir != null)
            {
                botonSalir.onClick.AddListener(SalirDelJuego);
            }

            // Reproducir música del menú
            if (Instance != null)
            {
                Instance.ReproducirMusicaMenu();
            }

            // Mostrar mejor puntuación
            MostrarMejorPuntuacion();
        }

        static void Update()
        {
            // Controles de teclado (New Input System)
            Keyboard keyboard = Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.enterKey.wasPressedThisFrame || keyboard.spaceKey.wasPressedThisFrame)
                {
                    IniciarJuego();
                }

                if (keyboard.escapeKey.wasPressedThisFrame)
                {
                    SalirDelJuego();
                }
            }
        }

        void MostrarMejorPuntuacion()
        {
            if (textoMejorPuntuacion != null)
            {
                int mejorPuntuacion = PlayerPrefs.GetInt("MejorPuntuacion", 0);
                textoMejorPuntuacion.text = "Mejor Puntuación: " + mejorPuntuacion;
            }
        }

        public static void IniciarJuego()
        {
            SceneManager.LoadScene("Juego");
        }

        public static void SalirDelJuego()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
