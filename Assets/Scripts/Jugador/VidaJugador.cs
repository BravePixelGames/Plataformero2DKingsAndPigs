using System;
using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public Action<int> JugadorTomoDaño;
    public Action<int> JugadorSeCuro;

    [SerializeField] private int vidaMaxima;
    [SerializeField] private int vidaActual;

    private void Awake()
    {
        vidaActual = vidaMaxima;
    }

    public void TomarDaño(int daño)
    {
        int vidaTemporal = vidaActual - daño;

        vidaTemporal = Mathf.Clamp(vidaTemporal, 0, vidaMaxima);

        vidaActual = vidaTemporal;

        JugadorTomoDaño?.Invoke(vidaActual);

        if (vidaActual <= 0)
        {
            DestruirJugador();
        }
    }

    private void DestruirJugador()
    {
        Destroy(gameObject);
    }

    public void CurarVida(int curacion)
    {
        int vidaTemporal = vidaActual + curacion;

        vidaTemporal = Mathf.Clamp(vidaTemporal, 0, vidaMaxima);

        vidaActual = vidaTemporal;

        JugadorSeCuro?.Invoke(vidaActual);
    }


    public int GetVidaMaxima() => vidaMaxima;
    public int GetVidaActual() => vidaActual;
}
