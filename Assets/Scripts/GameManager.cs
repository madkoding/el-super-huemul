using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[Header("UI")]
	public TextMeshProUGUI textoPuntos;
	public TextMeshProUGUI textoTiempo;

	[Header("Configuración")]
	public float tiempoJuego = 60f; // 60 segundos de juego

	private int puntosActuales = 0;
	private float tiempoRestante;
	private bool juegoActivo = true;

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
	}

	public void GameOver()
	{
		juegoActivo = false;

		// Guardar puntuación
		PlayerPrefs.SetInt("UltimaPuntuacion", puntosActuales);
		PlayerPrefs.Save();

		// Cargar escena de Game Over
		SceneManager.LoadScene("GameOver");
	}

	public void ReiniciarJuego()
	{
		SceneManager.LoadScene("Juego");
	}

	public void VolverAlMenu()
	{
		SceneManager.LoadScene("Inicio");
	}

	public void SalirDelJuego()
	{
		Application.Quit();
	}
}
