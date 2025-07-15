using UnityEngine;

public class GeneradorNivel : MonoBehaviour
{
	[Header("Prefabs")]
	public GameObject prefabPlataforma;
	public GameObject prefabEstrella;
	public GameObject prefabEnemigo;

	[Header("Configuración de Nivel")]
	public int numeroPlataformas = 10;
	public int numeroEstrellas = 15;
	public int numeroEnemigos = 3;

	[Header("Límites del Nivel")]
	public Vector2 limitesX = new Vector2(-10f, 10f);
	public Vector2 limitesY = new Vector2(-3f, 5f);

	void Start()
	{
		GenerarNivel();
	}

	void GenerarNivel()
	{
		// Generar plataformas
		GenerarPlataformas();

		// Generar estrellas
		GenerarEstrellas();

		// Generar enemigos
		GenerarEnemigos();
	}

	void GenerarPlataformas()
	{
		if (prefabPlataforma == null) return;

		for (int i = 0; i < numeroPlataformas; i++)
		{
			Vector3 posicion = new Vector3(
					Random.Range(limitesX.x, limitesX.y),
					Random.Range(limitesY.x, limitesY.y),
					0
			);

			GameObject nuevaPlataforma = Instantiate(prefabPlataforma, posicion, Quaternion.identity);
			nuevaPlataforma.transform.SetParent(transform);
			nuevaPlataforma.name = "Plataforma_" + i;
		}
	}

	void GenerarEstrellas()
	{
		if (prefabEstrella == null) return;

		for (int i = 0; i < numeroEstrellas; i++)
		{
			Vector3 posicion = new Vector3(
					Random.Range(limitesX.x, limitesX.y),
					Random.Range(limitesY.x + 1f, limitesY.y + 1f), // Un poco más arriba
					0
			);

			// Verificar que no esté muy cerca de otras estrellas
			bool posicionValida = true;
			GameObject[] estrellasExistentes = GameObject.FindGameObjectsWithTag("Estrella");

			foreach (GameObject estrella in estrellasExistentes)
			{
				if (Vector3.Distance(posicion, estrella.transform.position) < 2f)
				{
					posicionValida = false;
					break;
				}
			}

			if (posicionValida)
			{
				GameObject nuevaEstrella = Instantiate(prefabEstrella, posicion, Quaternion.identity);
				nuevaEstrella.transform.SetParent(transform);
				nuevaEstrella.name = "Estrella_" + i;
			}
			else
			{
				i--; // Intentar de nuevo
			}
		}
	}

	void GenerarEnemigos()
	{
		if (prefabEnemigo == null) return;

		for (int i = 0; i < numeroEnemigos; i++)
		{
			Vector3 posicion = new Vector3(
					Random.Range(limitesX.x, limitesX.y),
					limitesY.x + 1f, // En el suelo
					0
			);

			GameObject nuevoEnemigo = Instantiate(prefabEnemigo, posicion, Quaternion.identity);
			nuevoEnemigo.transform.SetParent(transform);
			nuevoEnemigo.name = "Enemigo_" + i;
		}
	}
}
