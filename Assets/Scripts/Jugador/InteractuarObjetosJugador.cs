using UnityEngine;

public class InteractuarObjetosJugador : MonoBehaviour
{
    [SerializeField] private Transform controladorInteractuar;
    [SerializeField] private Vector2 dimensionesCaja;
    [SerializeField] private LayerMask capasInteractuables;

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar"))
        {
            Interactuar();
        }
    }

    private void Interactuar()
    {
        Collider2D[] objetosTocados = Physics2D.OverlapBoxAll(controladorInteractuar.position, dimensionesCaja, 0f, capasInteractuables);

        foreach (Collider2D objeto in objetosTocados)
        {
            if (objeto.TryGetComponent(out IInteractuable interactuable))
            {
                interactuable.Interactuar();
                break;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(controladorInteractuar.position, dimensionesCaja);
    }
}
