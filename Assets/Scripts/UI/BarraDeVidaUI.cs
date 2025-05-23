using System;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaUI : MonoBehaviour
{
    [SerializeField] private Slider sliderBarraDeVida;
    [SerializeField] private VidaJugador vidaJugador;
    
    private void Start()
    {
        vidaJugador = FindFirstObjectByType<VidaJugador>();

        vidaJugador.JugadorTomoDaño += CambiarBarraVidaTomarDaño;
        vidaJugador.JugadorSeCuro += CambiarBarraVidaCuracion;

        IniciarBarraDeVida(vidaJugador.GetVidaMaxima(), vidaJugador.GetVidaActual());
    }

    void OnDisable()
    {
        vidaJugador.JugadorTomoDaño -= CambiarBarraVidaTomarDaño;
        vidaJugador.JugadorSeCuro -= CambiarBarraVidaCuracion;
    }

    private void IniciarBarraDeVida(int vidaMaxima, int vidaActual)
    {
        sliderBarraDeVida.maxValue = vidaMaxima;
        sliderBarraDeVida.value = vidaActual;
    }

    private void CambiarBarraVidaTomarDaño(int vidaActual)
    {
        sliderBarraDeVida.value = vidaActual;
    }

    private void CambiarBarraVidaCuracion(int vidaActual)
    {
        sliderBarraDeVida.value = vidaActual;
    }
}
