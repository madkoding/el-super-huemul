using UnityEngine;
using UnityEngine.UI;
using ElSuperHuemul;

namespace ElSuperHuemul.Audio
{
	public class AudioTester : MonoBehaviour
	{
		[Header("Botones de Prueba")]
		public Button botonSalto;
		public Button botonEstrella;
		public Button botonEnemigo;
		public Button botonCaida;
		public Button botonGameOver;
		public Button botonVictoria;

		[Header("Botones de Música")]
		public Button botonMusicaMenu;
		public Button botonMusicaJuego;
		public Button botonMusicaGameOver;
		public Button botonDetenerMusica;

		[Header("Controles de Volumen")]
		public Slider sliderVolumenMusica;
		public Slider sliderVolumenEfectos;

		void Start()
		{
			ConfigurarBotones();
			ConfigurarSliders();
		}

		void ConfigurarBotones()
		{
			// Efectos de sonido
			if (botonSalto != null)
				botonSalto.onClick.AddListener(() => AudioManager.Instance?.ReproducirSonidoSalto());

			if (botonEstrella != null)
				botonEstrella.onClick.AddListener(() => AudioManager.Instance?.ReproducirSonidoEstrella());

			if (botonEnemigo != null)
				botonEnemigo.onClick.AddListener(() => AudioManager.Instance?.ReproducirSonidoEnemigo());

			if (botonCaida != null)
				botonCaida.onClick.AddListener(() => AudioManager.Instance?.ReproducirSonidoCaida());

			if (botonGameOver != null)
				botonGameOver.onClick.AddListener(() => AudioManager.Instance?.ReproducirSonidoGameOver());

			if (botonVictoria != null)
				botonVictoria.onClick.AddListener(() => AudioManager.Instance?.ReproducirSonidoVictoria());

			// Música
			if (botonMusicaMenu != null)
				botonMusicaMenu.onClick.AddListener(() => AudioManager.Instance?.ReproducirMusicaMenu());

			if (botonMusicaJuego != null)
				botonMusicaJuego.onClick.AddListener(() => AudioManager.Instance?.ReproducirMusicaJuego());

			if (botonMusicaGameOver != null)
				botonMusicaGameOver.onClick.AddListener(() => AudioManager.Instance?.ReproducirMusicaGameOver());

			if (botonDetenerMusica != null)
				botonDetenerMusica.onClick.AddListener(() => AudioManager.Instance?.DetenerMusica());
		}

		void ConfigurarSliders()
		{
			if (sliderVolumenMusica != null)
			{
				sliderVolumenMusica.value = AudioManager.Instance?.volumenMusica ?? 0.5f;
				sliderVolumenMusica.onValueChanged.AddListener(CambiarVolumenMusica);
			}

			if (sliderVolumenEfectos != null)
			{
				sliderVolumenEfectos.value = AudioManager.Instance?.volumenEfectos ?? 0.7f;
				sliderVolumenEfectos.onValueChanged.AddListener(CambiarVolumenEfectos);
			}
		}

		public static void CambiarVolumenMusica(float valor)
		{
			AudioManager.Instance?.CambiarVolumenMusica(valor);
		}

		public static void CambiarVolumenEfectos(float valor)
		{
			AudioManager.Instance?.CambiarVolumenEfectos(valor);
		}

		public static void Update()
		{
			// Pruebas con teclado
			if (Input.GetKeyDown(KeyCode.Alpha1))
				AudioManager.Instance?.ReproducirSonidoSalto();

			if (Input.GetKeyDown(KeyCode.Alpha2))
				AudioManager.Instance?.ReproducirSonidoEstrella();

			if (Input.GetKeyDown(KeyCode.Alpha3))
				AudioManager.Instance?.ReproducirSonidoEnemigo();

			if (Input.GetKeyDown(KeyCode.Alpha4))
				AudioManager.Instance?.ReproducirSonidoCaida();

			if (Input.GetKeyDown(KeyCode.Alpha5))
				AudioManager.Instance?.ReproducirSonidoGameOver();

			if (Input.GetKeyDown(KeyCode.Alpha6))
				AudioManager.Instance?.ReproducirSonidoVictoria();

			// Música
			if (Input.GetKeyDown(KeyCode.F1))
				AudioManager.Instance?.ReproducirMusicaMenu();

			if (Input.GetKeyDown(KeyCode.F2))
				AudioManager.Instance?.ReproducirMusicaJuego();

			if (Input.GetKeyDown(KeyCode.F3))
				AudioManager.Instance?.ReproducirMusicaGameOver();

			if (Input.GetKeyDown(KeyCode.F4))
				AudioManager.Instance?.DetenerMusica();
		}

		public static void OnGUI()
		{
			GUILayout.BeginArea(new Rect(10, 10, 300, 400));
			GUILayout.Label("=== AUDIO TESTER ===");
			GUILayout.Space(10);

			GUILayout.Label("Efectos de Sonido:");
			if (GUILayout.Button("1 - Salto")) AudioManager.Instance?.ReproducirSonidoSalto();
			if (GUILayout.Button("2 - Estrella")) AudioManager.Instance?.ReproducirSonidoEstrella();
			if (GUILayout.Button("3 - Enemigo")) AudioManager.Instance?.ReproducirSonidoEnemigo();
			if (GUILayout.Button("4 - Caída")) AudioManager.Instance?.ReproducirSonidoCaida();
			if (GUILayout.Button("5 - Game Over")) AudioManager.Instance?.ReproducirSonidoGameOver();
			if (GUILayout.Button("6 - Victoria")) AudioManager.Instance?.ReproducirSonidoVictoria();

			GUILayout.Space(10);
			GUILayout.Label("Música de Fondo:");
			if (GUILayout.Button("F1 - Música Menú")) AudioManager.Instance?.ReproducirMusicaMenu();
			if (GUILayout.Button("F2 - Música Juego")) AudioManager.Instance?.ReproducirMusicaJuego();
			if (GUILayout.Button("F3 - Música Game Over")) AudioManager.Instance?.ReproducirMusicaGameOver();
			if (GUILayout.Button("F4 - Detener Música")) AudioManager.Instance?.DetenerMusica();

			GUILayout.Space(10);
			GUILayout.Label("Instrucciones:");
			GUILayout.Label("- Teclas 1-6: Efectos de sonido");
			GUILayout.Label("- Teclas F1-F4: Música");

			GUILayout.EndArea();
		}
	}
}
