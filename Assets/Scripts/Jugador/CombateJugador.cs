using System;
using System.Collections.Generic;
using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    public static Action JugadorGolpeoUnObjetivo;

    [Header("Referencias")]
    [SerializeField] private Animator animator;
    [SerializeField] private Ataque[] ataquesJugador;

    [Header("Ataque")]
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoUltimoAtaque;

    [Header("Combo")]
    [SerializeField] private float ventanaDeCombo;
    [SerializeField] private int indiceCombo = 0;
    [SerializeField] private float tiempoEntreCombos;
    [SerializeField] private float tiempoBufferEntrada = 0.25f;
    private Queue<float> bufferEntradas = new();

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            bufferEntradas.Enqueue(Time.time);
        }

        while (bufferEntradas.Count > 0 && Time.time > bufferEntradas.Peek() + tiempoBufferEntrada)
        {
            bufferEntradas.Dequeue();
        }

        if (bufferEntradas.Count > 0)
        {
            IntentarAtacar();
        }

        if (indiceCombo > 0 && Time.time > tiempoUltimoAtaque + ventanaDeCombo)
        {
            ResetearCombo();
        }
    }

    private void IntentarAtacar()
    {
        if (Time.time < tiempoUltimoAtaque + tiempoEntreAtaques) { return; }

        if (bufferEntradas.Count > 0)
        {
            bufferEntradas.Dequeue();
            Atacar();
        }
    }

    private void Atacar()
    {
        Ataque ataqueActual = ataquesJugador[indiceCombo];

        animator.SetTrigger(ataqueActual.stringAnimacion);

        tiempoUltimoAtaque = Time.time;

        Collider2D[] objetosTocados = ObtenerObjetosTocados(ataqueActual);

        bool objetivoGolpeado = false;

        foreach (Collider2D objeto in objetosTocados)
        {
            if (objeto.TryGetComponent(out IGolpeable golpeable))
            {
                golpeable.TomarDaño(ataqueActual.cantidadDeDaño, transform);
                objetivoGolpeado = true;
            }
        }

        if (objetivoGolpeado)
        {
            JugadorGolpeoUnObjetivo?.Invoke();
        }

        indiceCombo++;

        if (indiceCombo >= ataquesJugador.Length)
        {
            tiempoUltimoAtaque += tiempoEntreCombos;
            ResetearCombo();
        }
    }

    private static Collider2D[] ObtenerObjetosTocados(Ataque ataqueActual)
    {
        Collider2D[] objetosTocados = ataqueActual.tipoDeAtaque switch
        {
            TipoDeAtaque.Caja => Physics2D.OverlapBoxAll(
                                ataqueActual.controladorAtaque.position,
                                ataqueActual.dimensionesCaja,
                                0f
                                ),
            TipoDeAtaque.Circulo => Physics2D.OverlapCircleAll(
                                ataqueActual.controladorAtaque.position,
                                ataqueActual.radioAtaque
                                ),
            _ => Physics2D.OverlapCircleAll(
                                ataqueActual.controladorAtaque.position,
                                ataqueActual.radioAtaque
                                ),
        };

        return objetosTocados;
    }

    private void ResetearCombo()
    {
        indiceCombo = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        foreach (Ataque ataque in ataquesJugador)
        {
            switch (ataque.tipoDeAtaque)
            {
                case TipoDeAtaque.Caja:
                    Gizmos.DrawWireCube(ataque.controladorAtaque.position, ataque.dimensionesCaja);
                    break;
                case TipoDeAtaque.Circulo:
                    Gizmos.DrawWireSphere(ataque.controladorAtaque.position, ataque.radioAtaque);
                    break;
            }
        }
    }
}
