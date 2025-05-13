using Unity.Cinemachine;
using UnityEngine;

public class SeguirJugadorCamara : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private void Start()
    {
        SeguirJugador();
    }

    private void SeguirJugador()
    {
        MovimientoJugador jugador = FindFirstObjectByType<MovimientoJugador>();

        if (jugador == null)
        {
            Debug.LogWarning("No se encontro el jugador");

            return;
        }

        Transform jugadorTransform = jugador.transform;

        cinemachineCamera.Follow = jugadorTransform;
    }
}
