using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class GameOverController : MonoBehaviour
{
	[Header("UI")]
	public TextMeshProUGUI textoPuntuacionFinal;
	public TextMeshProUGUI textoMejorPuntuacion;
	public TextMeshProUGUI textoNuevoRecord;
	public Button botonReiniciar;
	public Button botonMenu;

	void Start()
	{
		// Configurar botones
		if (botonReiniciar != null)
		{
			botonReiniciar.onClick.AddListener(ReiniciarJuego);
		}

		if (botonMenu != null)
		{
			botonMenu.onClick.AddListener(VolverAlMenu);
		}

		// Mostrar puntuaciones
		MostrarPuntuaciones();
	}

	void Update()
	{
		// Controles de teclado (New Input System)
		Keyboard keyboard = Keyboard.current;
		if (keyboard != null)
		{
			if (keyboard.rKey.wasPressedThisFrame)
			{
				ReiniciarJuego();
			}

			if (keyboard.escapeKey.wasPressedThisFrame || keyboard.mKey.wasPressedThisFrame)
			{
				VolverAlMenu();
			}
		}
	}

	void MostrarPuntuaciones()
	{
		int ultimaPuntuacion = PlayerPrefs.GetInt("UltimaPuntuacion", 0);
		int mejorPuntuacion = PlayerPrefs.GetInt("MejorPuntuacion", 0);

		// Verificar si es un nuevo récord
		bool nuevoRecord = false;
		if (ultimaPuntuacion > mejorPuntuacion)
		{
			mejorPuntuacion = ultimaPuntuacion;
			PlayerPrefs.SetInt("MejorPuntuacion", mejorPuntuacion);
			PlayerPrefs.Save();
			nuevoRecord = true;
		}

		// Actualizar UI
		if (textoPuntuacionFinal != null)
		{
			textoPuntuacionFinal.text = "Puntuación Final: " + ultimaPuntuacion;
		}

		if (textoMejorPuntuacion != null)
		{
			textoMejorPuntuacion.text = "Mejor Puntuación: " + mejorPuntuacion;
		}

		if (textoNuevoRecord != null)
		{
			textoNuevoRecord.gameObject.SetActive(nuevoRecord);
			if (nuevoRecord)
			{
				textoNuevoRecord.text = "¡NUEVO RÉCORD!";
			}
		}
	}

	public void ReiniciarJuego()
	{
		SceneManager.LoadScene("Juego");
	}

	public void VolverAlMenu()
	{
		SceneManager.LoadScene("Inicio");
	}
}
