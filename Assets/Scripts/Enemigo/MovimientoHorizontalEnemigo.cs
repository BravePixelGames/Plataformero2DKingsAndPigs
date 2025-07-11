using UnityEngine;

public class MovimientoHorizontalEnemigo : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float velocidadDeMovimiento;
    [SerializeField] private bool ocupado = false;

    private void FixedUpdate()
    {
        if (ocupado)
        {
            rb2D.linearVelocity = Vector2.zero;
            return;
        }

        Correr();
    }

    private void Correr()
    {
        rb2D.linearVelocity = new Vector2(velocidadDeMovimiento, rb2D.linearVelocity.y);
    }

    public void CambiarEstadoOcuparEnemigo(bool estado)
    {
        ocupado = estado;
    }

    public bool EstaOcupado() => ocupado;
}
