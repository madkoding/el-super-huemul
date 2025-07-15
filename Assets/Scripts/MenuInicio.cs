using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

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

		// Mostrar mejor puntuación
		MostrarMejorPuntuacion();
	}

	void Update()
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

	public void IniciarJuego()
	{
		SceneManager.LoadScene("Juego");
	}

	public void SalirDelJuego()
	{
		Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
	}
}
