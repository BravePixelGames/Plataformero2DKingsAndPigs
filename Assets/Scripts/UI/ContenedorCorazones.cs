using System;
using UnityEngine;

public class ContenedorCorazones : MonoBehaviour
{
    [SerializeField] private CorazonUI[] corazones;
    private VidaJugador vidaJugador;

    private void Start()
    {
        vidaJugador = FindFirstObjectByType<VidaJugador>();

        vidaJugador.JugadorTomoDaño += ActivarCorazones;
        vidaJugador.JugadorSeCuro += ActivarCorazones;

        ActivarCorazones(vidaJugador.GetVidaActual());
    }

    void OnDisable()
    {
        vidaJugador.JugadorTomoDaño -= ActivarCorazones;
        vidaJugador.JugadorSeCuro -= ActivarCorazones;
    }

    private void ActivarCorazones(int vidaActual)
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < vidaActual)
            {
                if (corazones[i].EstaActivo()) { continue; }

                corazones[i].ActivarCorazon();
            }
            else
            {
                if (!corazones[i].EstaActivo()) { continue; }

                corazones[i].DesactivarCorazon();
            }
        }
    }
}
