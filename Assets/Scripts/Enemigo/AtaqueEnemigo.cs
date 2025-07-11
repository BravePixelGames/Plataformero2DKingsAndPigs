using UnityEngine;

public class AtaqueEnemigo : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private LayerMask capasGolpeables;
    [SerializeField] private MovimientoEnemigo movimientoEnemigo;

    [Header("Ataque")]
    [SerializeField] private float radioCirculo;
    [SerializeField] private int dañoAtaque;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoUltimoAtaque;
    [SerializeField] private float duracionDelAtaque;
    [SerializeField] private float tiempoAEsperarDespuesDelAtaque;

    private void Update()
    {
        if (movimientoEnemigo.GetEstadoActual() == EstadosEnemigo.Ocupado) { return; }

        if (!movimientoEnemigo.EstaEnElSuelo()) { return; }

        if (Time.time < tiempoUltimoAtaque + tiempoEntreAtaques) { return; }

        Collider2D collider = Physics2D.OverlapCircle(controladorAtaque.position, radioCirculo, capasGolpeables);

        if (collider)
        {
            movimientoEnemigo.CambiarAEstadoOcupado(duracionDelAtaque, transform);
            rb2D.linearVelocity = Vector2.zero;
            animator.SetTrigger("Atacar");
        }
    }

    public void Atacar()
    {
        Collider2D[] objetosTocados = Physics2D.OverlapCircleAll(controladorAtaque.position, radioCirculo, capasGolpeables);

        foreach (Collider2D objeto in objetosTocados)
        {
            if (objeto.TryGetComponent(out VidaJugador vidaJugador))
            {
                vidaJugador.TomarDaño(dañoAtaque);
            }
        }

        tiempoUltimoAtaque = Time.time;
    }

    public void AtaqueTermino()
    {
        animator.SetBool("Ocupado", false);
        movimientoEnemigo.CambiarAEstadoEsperar(tiempoAEsperarDespuesDelAtaque);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioCirculo);
    }
}
