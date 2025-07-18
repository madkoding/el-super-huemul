using UnityEngine;
using ElSuperHuemul.Game;

namespace ElSuperHuemul.Checkpoints
{
	public class CheckpointManager : MonoBehaviour
	{
		[Header("Configuración")]
		public Checkpoint[] todosLosCheckpoints;

		void Start()
		{
			// Buscar todos los checkpoints si no están asignados
			if (todosLosCheckpoints == null || todosLosCheckpoints.Length == 0)
			{
				todosLosCheckpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);
			}
		}

		public void ActivarCheckpoint(Vector3 posicion)
		{
			// Desactivar todos los checkpoints anteriores
			foreach (Checkpoint checkpoint in todosLosCheckpoints)
			{
				if (Vector3.Distance(checkpoint.transform.position, posicion) < 0.1f)
				{
					// Este es el checkpoint que se activó
					checkpoint.esCheckpointActivo = true;
				}
				else if (checkpoint.transform.position.x < posicion.x)
				{
					// Checkpoints anteriores se mantienen activos
					checkpoint.esCheckpointActivo = true;
				}
			}
		}

		public Vector3 ObtenerUltimoCheckpoint()
		{
			Vector3 ultimoCheckpoint = Vector3.zero;
			float mayorX = -Mathf.Infinity;

			foreach (Checkpoint checkpoint in todosLosCheckpoints)
			{
				if (checkpoint.esCheckpointActivo && checkpoint.transform.position.x > mayorX)
				{
					mayorX = checkpoint.transform.position.x;
					ultimoCheckpoint = checkpoint.transform.position;
				}
			}

			return ultimoCheckpoint;
		}
	}
}
