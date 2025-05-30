using UnityEngine;

public class Corazon : MonoBehaviour, IInteractuable
{
    [SerializeField] private Animator animator;
    [SerializeField] private int cantidadCuracion;
    private bool sePuedeUsar = true;

    public void Interactuar()
    {
        if (!sePuedeUsar) { return; }
        sePuedeUsar = false;
        animator.SetTrigger("Recoger");

        VidaJugador vidaJugador = FindFirstObjectByType<VidaJugador>();
        if (vidaJugador != null)
        {
            vidaJugador.CurarVida(cantidadCuracion);
        }
    }

    public void DestruirObjeto()
    {
        Destroy(gameObject);
    }
}
