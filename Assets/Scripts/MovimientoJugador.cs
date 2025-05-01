using System;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float velocidadMovimiento;
    private float entradaHorizontal;

    private void Update()
    {
        entradaHorizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        ControlarMovimientoHorizontal();
    }

    private void ControlarMovimientoHorizontal()
    {
        rb2D.linearVelocity = new Vector2(entradaHorizontal * velocidadMovimiento, rb2D.linearVelocity.y);

        if ((entradaHorizontal > 0 && !MirandoALaDerecha()) || (entradaHorizontal < 0 && MirandoALaDerecha()))
        {
            Girar();
        }
    }

    private void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private bool MirandoALaDerecha()
    {
        return transform.localScale.x == 1;
    }
}
