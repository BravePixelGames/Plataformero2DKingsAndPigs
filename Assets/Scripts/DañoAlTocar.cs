using UnityEngine;

public class DañoAlTocar : MonoBehaviour
{
    [SerializeField] private int dañoPorToque;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out VidaJugador vidaJugador))
        {
            vidaJugador.TomarDaño(dañoPorToque);
        }
    }
}
