using System;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb2D;

    [Header("Movimiento Horizontal")]
    [SerializeField] private float velocidadMovimiento;
    private float entradaHorizontal;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector2 dimensionesCaja;
    [SerializeField] private LayerMask capasSalto;
    [SerializeField] private bool sePuedeMoverEnElAire;
    private bool enSuelo;
    private bool entradaSalto;

    private void Update()
    {
        entradaHorizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            entradaSalto = true;
        }

        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, capasSalto);
    }

    private void FixedUpdate()
    {
        ControlarMovimientoHorizontal();
        ControlarSalto();
        entradaSalto = false;
    }

    private void ControlarSalto()
    {
        if (!entradaSalto) { return; }
        if (!enSuelo) { return; }
        Saltar();
    }

    private void Saltar()
    {
        entradaSalto = false;
        rb2D.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
    }

    private void ControlarMovimientoHorizontal()
    {
        if (!enSuelo && !sePuedeMoverEnElAire) { return; }

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
