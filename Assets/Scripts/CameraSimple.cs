using UnityEngine;

public class CameraSimple : MonoBehaviour
{
	[Header("Configuración")]
	public Transform jugador;
	public float suavizado = 5f;
	public Vector3 offset = new Vector3(0, 2f, -10f);

	[Header("Límites de Cámara")]
	public bool usarLimites = true;
	public Transform fondoBackground; // Arrastra aquí el GameObject del background

	// Límites calculados automáticamente
	private float limiteIzquierdo;
	private float limiteDerecho;
	private float limiteInferior;
	private float limiteSuperior;

	// Tamaño de la cámara
	private Camera cam;
	private float alturaCamera;
	private float anchoCamera;

	void Start()
	{
		// Buscar al jugador automáticamente
		if (jugador == null)
		{
			GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
			if (playerObj != null)
			{
				jugador = playerObj.transform;
			}
		}

		// Buscar el background automáticamente si no está asignado
		if (fondoBackground == null)
		{
			GameObject background = GameObject.Find("background");
			if (background != null)
			{
				fondoBackground = background.transform;
			}
		}

		// Configurar cámara y límites
		cam = GetComponent<Camera>();
		CalcularLimites();
	}

	void CalcularLimites()
	{
		if (fondoBackground != null && usarLimites)
		{
			// Obtener el tamaño de la cámara
			alturaCamera = cam.orthographicSize;
			anchoCamera = alturaCamera * cam.aspect;

			// Obtener el tamaño del background
			SpriteRenderer bgRenderer = fondoBackground.GetComponent<SpriteRenderer>();
			if (bgRenderer != null)
			{
				Vector3 tamanoBackground = bgRenderer.bounds.size;
				Vector3 centroBackground = bgRenderer.bounds.center;

				// Calcular límites para que la cámara no se salga del background
				limiteIzquierdo = centroBackground.x - tamanoBackground.x / 2 + anchoCamera;
				limiteDerecho = centroBackground.x + tamanoBackground.x / 2 - anchoCamera;
				limiteInferior = centroBackground.y - tamanoBackground.y / 2 + alturaCamera;
				limiteSuperior = centroBackground.y + tamanoBackground.y / 2 - alturaCamera;
			}
		}
	}

	void LateUpdate()
	{
		if (jugador != null)
		{
			Vector3 posicionDeseada = jugador.position + offset;

			// Aplicar límites si están habilitados
			if (usarLimites && fondoBackground != null)
			{
				posicionDeseada.x = Mathf.Clamp(posicionDeseada.x, limiteIzquierdo, limiteDerecho);
				posicionDeseada.y = Mathf.Clamp(posicionDeseada.y, limiteInferior, limiteSuperior);
			}

			transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);
		}
	}

	void OnDrawGizmosSelected()
	{
		if (usarLimites && fondoBackground != null)
		{
			// Dibujar los límites de la cámara
			Gizmos.color = Color.yellow;
			Vector3 centro = new Vector3(
				(limiteIzquierdo + limiteDerecho) / 2f,
				(limiteInferior + limiteSuperior) / 2f,
				transform.position.z
			);
			Vector3 tamano = new Vector3(
				limiteDerecho - limiteIzquierdo,
				limiteSuperior - limiteInferior,
				0
			);
			Gizmos.DrawWireCube(centro, tamano);
		}
	}
}
