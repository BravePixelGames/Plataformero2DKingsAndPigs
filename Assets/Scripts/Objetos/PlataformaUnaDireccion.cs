using UnityEngine;

public class PlataformaUnaDireccion : MonoBehaviour
{
    [SerializeField] private Collider2D colisionadorPlataforma;

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision, colisionadorPlataforma, false);
        }
    }
}
