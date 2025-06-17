using Unity.Cinemachine;
using UnityEngine;

public class ControladorVibracion : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;

    [Header("Vibracion Camara")]
    [SerializeField] private float vibracionX;
    [SerializeField] private float vibracionY;

    void OnEnable()
    {
        CombateJugador.JugadorGolpeoUnObjetivo += GenerarMovimientoCamara;
    }

    void OnDisable()
    {
        CombateJugador.JugadorGolpeoUnObjetivo -= GenerarMovimientoCamara;
    }

    private void GenerarMovimientoCamara()
    {
        float velocidadAleatoriaX = Random.Range(-vibracionX, vibracionX);
        float velocidadAleatoriaY = Random.Range(-vibracionY, vibracionY);

        Vector2 velocidad = new(velocidadAleatoriaX, velocidadAleatoriaY);

        cinemachineImpulseSource.GenerateImpulse(velocidad);
    }
}
