using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioTutorial : MonoBehaviour
{
	[Header("UI de Tutorial")]
	public TextMeshProUGUI textoTutorial;
	public Button botonSiguiente;
	public Button botonAnterior;
	public Button botonCerrar;

	private string[] pasosTutorial = {
				"1. Agrega archivos de audio a la carpeta Assets/Audio/",
				"2. Crea un GameObject vacío llamado 'AudioManager'",
				"3. Asigna el script AudioManager.cs al GameObject",
				"4. Agrega dos AudioSources al AudioManager",
				"5. Configura el primer AudioSource para música (Loop: ON)",
				"6. Configura el segundo AudioSource para efectos (Loop: OFF)",
				"7. Arrastra los archivos de audio a los campos correspondientes",
				"8. El sistema de audio está listo para usar!",
				"Controles:\n- Música se reproduce automáticamente\n- Efectos se activan con las acciones del jugador\n- El AudioManager persiste entre escenas"
		};

	private int pasoActual = 0;

	void Start()
	{
		if (botonSiguiente != null)
			botonSiguiente.onClick.AddListener(SiguientePaso);

		if (botonAnterior != null)
			botonAnterior.onClick.AddListener(PasoAnterior);

		if (botonCerrar != null)
			botonCerrar.onClick.AddListener(CerrarTutorial);

		ActualizarTutorial();
	}

	void SiguientePaso()
	{
		pasoActual++;
		if (pasoActual >= pasosTutorial.Length)
		{
			pasoActual = pasosTutorial.Length - 1;
		}
		ActualizarTutorial();
	}

	void PasoAnterior()
	{
		pasoActual--;
		if (pasoActual < 0)
		{
			pasoActual = 0;
		}
		ActualizarTutorial();
	}

	void ActualizarTutorial()
	{
		if (textoTutorial != null)
		{
			textoTutorial.text = pasosTutorial[pasoActual];
		}

		// Actualizar botones
		if (botonAnterior != null)
			botonAnterior.interactable = pasoActual > 0;

		if (botonSiguiente != null)
			botonSiguiente.interactable = pasoActual < pasosTutorial.Length - 1;
	}

	void CerrarTutorial()
	{
		gameObject.SetActive(false);
	}

	[ContextMenu("Mostrar Tutorial")]
	public void MostrarTutorial()
	{
		gameObject.SetActive(true);
		pasoActual = 0;
		ActualizarTutorial();
	}
}
