using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header("Objetivo")]
	public Transform objetivo; // El jugador

	[Header("Configuración de Seguimiento")]
	public float velocidadSeguimiento = 5f;
	public Vector3 offset = new Vector3(0, 2f, -10f);

	[Header("Límites de la Cámara")]
	public bool usarLimites = true;
	public float limiteIzquierdo = -15f;
	public float limiteDerecho = 15f;
	public float limiteInferior = -5f;
	public float limiteSuperior = 10f;

	[Header("Zona Muerta")]
	public bool usarZonaMuerta = true;
	public float anchoZonaMuerta = 2f;
	public float altoZonaMuerta = 1f;

	private Vector3 posicionObjetivo;
	private Vector3 ultimaPosicionObjetivo;

	void Start()
	{
		// Buscar al jugador automáticamente si no está asignado
		if (objetivo == null)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player1");
			if (player != null)
			{
				objetivo = player.transform;
			}
		}

		if (objetivo != null)
		{
			ultimaPosicionObjetivo = objetivo.position;
		}
	}

	void LateUpdate()
	{
		if (objetivo == null) return;

		// Calcular la posición objetivo de la cámara
		CalcularPosicionObjetivo();

		// Mover la cámara hacia la posición objetivo
		if (velocidadSeguimiento > 0)
		{
			transform.position = Vector3.Lerp(transform.position, posicionObjetivo, velocidadSeguimiento * Time.deltaTime);
		}
		else
		{
			transform.position = posicionObjetivo;
		}

		ultimaPosicionObjetivo = objetivo.position;
	}

	void CalcularPosicionObjetivo()
	{
		Vector3 posicionDeseada = objetivo.position + offset;

		// Aplicar zona muerta si está habilitada
		if (usarZonaMuerta)
		{
			Vector3 diferencia = objetivo.position - ultimaPosicionObjetivo;
			Vector3 posicionCamaraActual = transform.position - offset;

			// Verificar si el jugador está fuera de la zona muerta
			float diferenciaX = objetivo.position.x - posicionCamaraActual.x;
			float diferenciaY = objetivo.position.y - posicionCamaraActual.y;

			// Solo mover en X si está fuera de la zona muerta horizontal
			if (Mathf.Abs(diferenciaX) > anchoZonaMuerta / 2f)
			{
				float nuevaX = objetivo.position.x;
				if (diferenciaX > 0)
					nuevaX -= anchoZonaMuerta / 2f;
				else
					nuevaX += anchoZonaMuerta / 2f;

				posicionDeseada.x = nuevaX + offset.x;
			}
			else
			{
				posicionDeseada.x = transform.position.x;
			}

			// Solo mover en Y si está fuera de la zona muerta vertical
			if (Mathf.Abs(diferenciaY) > altoZonaMuerta / 2f)
			{
				float nuevaY = objetivo.position.y;
				if (diferenciaY > 0)
					nuevaY -= altoZonaMuerta / 2f;
				else
					nuevaY += altoZonaMuerta / 2f;

				posicionDeseada.y = nuevaY + offset.y;
			}
			else
			{
				posicionDeseada.y = transform.position.y;
			}
		}

		// Aplicar límites si están habilitados
		if (usarLimites)
		{
			posicionDeseada.x = Mathf.Clamp(posicionDeseada.x, limiteIzquierdo, limiteDerecho);
			posicionDeseada.y = Mathf.Clamp(posicionDeseada.y, limiteInferior, limiteSuperior);
		}

		// Mantener la Z del offset
		posicionDeseada.z = offset.z;

		posicionObjetivo = posicionDeseada;
	}

	void OnDrawGizmosSelected()
	{
		if (objetivo == null) return;

		// Dibujar la zona muerta
		if (usarZonaMuerta)
		{
			Gizmos.color = Color.yellow;
			Vector3 centro = objetivo.position;
			Gizmos.DrawWireCube(centro, new Vector3(anchoZonaMuerta, altoZonaMuerta, 0));
		}

		// Dibujar los límites de la cámara
		if (usarLimites)
		{
			Gizmos.color = Color.red;
			Vector3 centroLimites = new Vector3(
					(limiteIzquierdo + limiteDerecho) / 2f,
					(limiteInferior + limiteSuperior) / 2f,
					0
			);
			Vector3 tamanoLimites = new Vector3(
					limiteDerecho - limiteIzquierdo,
					limiteSuperior - limiteInferior,
					0
			);
			Gizmos.DrawWireCube(centroLimites, tamanoLimites);
		}

		// Dibujar línea hacia el objetivo
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, objetivo.position);
	}
}
