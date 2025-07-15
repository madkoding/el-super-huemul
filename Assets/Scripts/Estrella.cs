using UnityEngine;

public class Estrella : MonoBehaviour
{
	[Header("Animación")]
	public float velocidadRotacion = 50f;
	public float velocidadFlotacion = 1f;
	public float amplitudFlotacion = 0.5f;

	private Vector3 posicionInicial;
	private float tiempoFlotacion;

	void Start()
	{
		posicionInicial = transform.position;
		tiempoFlotacion = Random.Range(0f, 2f * Mathf.PI); // Desfase aleatorio
	}

	void Update()
	{
		// Rotación
		transform.Rotate(0, 0, velocidadRotacion * Time.deltaTime);

		// Flotación
		tiempoFlotacion += velocidadFlotacion * Time.deltaTime;
		float nuevaY = posicionInicial.y + Mathf.Sin(tiempoFlotacion) * amplitudFlotacion;
		transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player1"))
		{
			// Efecto de sonido aquí si lo tienes
			// AudioSource.PlayClipAtPoint(sonidoRecoleccion, transform.position);

			// El PlayerController se encarga de sumar puntos y destruir el objeto
		}
	}
}
