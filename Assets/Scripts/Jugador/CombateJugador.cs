using System;
using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    private const string STRING_ANIMACION_ATAQUE = "Atacar";
    public static Action JugadorGolpeoUnObjetivo;

    [Header("Referencias")]
    [SerializeField] private Animator animator;

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private int dañoAtaque;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoUltimoAtaque;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            IntentarAtacar();
        }
    }

    private void IntentarAtacar()
    {
        if (Time.time < tiempoUltimoAtaque + tiempoEntreAtaques) { return; }

        Atacar();
    }

    private void Atacar()
    {
        animator.SetTrigger(STRING_ANIMACION_ATAQUE);

        tiempoUltimoAtaque = Time.time;

        Collider2D[] objetosTocados = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);

        bool objetivoGolpeado = false;

        foreach (Collider2D objeto in objetosTocados)
        {
            if (objeto.TryGetComponent(out IGolpeable golpeable))
            {
                golpeable.TomarDaño(dañoAtaque);
                objetivoGolpeado = true;
            }
        }

        if (objetivoGolpeado)
        {
            JugadorGolpeoUnObjetivo?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }
}
