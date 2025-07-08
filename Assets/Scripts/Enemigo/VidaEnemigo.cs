using UnityEngine;

public class VidaEnemigo : MonoBehaviour, IGolpeable
{
    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;
    [SerializeField] private MovimientoEnemigo movimientoEnemigo;

    [Header("Vida")]
    [SerializeField] private int vidaMaxima;
    [SerializeField] private int vidaActual;

    [Header("Retroceso")]
    [SerializeField] private Vector2 fuerzaRetroceso;
    [SerializeField] private float tiempoMinimoRetroceso;

    private void Awake()
    {
        vidaActual = vidaMaxima;
    }

    public void TomarDaño(int cantidadDeDaño, Transform sender)
    {
        int cantidadDeVidaTemporal = vidaActual - cantidadDeDaño;

        cantidadDeVidaTemporal = Mathf.Clamp(cantidadDeVidaTemporal, 0, vidaMaxima);

        vidaActual = cantidadDeVidaTemporal;

        if (vidaActual == 0)
        {
            Destroy(gameObject);
        }

        Retroceso(sender);
    }

    private void Retroceso(Transform sender)
    {
        movimientoEnemigo.CambiarAEstadoOcupado(tiempoMinimoRetroceso, sender);

        Vector2 direccion = (transform.position - sender.position).normalized;

        Vector2 fuerza = new(Mathf.Sign(direccion.x) * fuerzaRetroceso.x, fuerzaRetroceso.y);

        rb2D.linearVelocity = Vector2.zero;

        rb2D.AddForce(fuerza, ForceMode2D.Impulse);

        animator.SetTrigger("Golpe");
    }
}
