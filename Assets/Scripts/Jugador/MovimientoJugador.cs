using System;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private const string STRING_VELOCIDAD_HORIZONTAL = "VelocidadHorizontal";
    private const string STRING_VELOCIDAD_VERTICAL = "VelocidadVertical";
    private const string STRING_EN_SUELO = "EnSuelo";
    private const string STRING_ATERRIZAR = "Aterrizar";

    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D colisionadorJugador;

    [Header("Movimiento Horizontal")]
    [SerializeField] private float velocidadMovimiento;
    private float entradaHorizontal;
    private float entradaVertical;

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
        entradaVertical = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            entradaSalto = true;
        }

        ControlarAnimaciones();
    }

    private void FixedUpdate()
    {
        ControlarMovimientoHorizontal();
        ControlarSalto();
        entradaSalto = false;
    }

    private void ControlarSalto()
    {
        bool estabaEnElSuelo = enSuelo;
        enSuelo = false;

        Collider2D suelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, capasSalto);

        if (suelo)
        {
            enSuelo = true;
            if (!estabaEnElSuelo && rb2D.linearVelocity.y <= 0)
            {
                animator.SetTrigger(STRING_ATERRIZAR);
            }
        }

        if (!entradaSalto) { return; }
        if (!enSuelo) { return; }

        if (entradaVertical < 0)
        {
            DesactivarPlataformas();
        }
        else
        {
            Saltar();
        }
    }

    private void Saltar()
    {
        entradaSalto = false;
        rb2D.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
    }

    private void DesactivarPlataformas()
    {
        Collider2D[] objetosTocados = Physics2D.OverlapBoxAll(controladorSuelo.position, dimensionesCaja, 0f, capasSalto);

        foreach (Collider2D objeto in objetosTocados)
        {
            if (objeto.GetComponent<PlatformEffector2D>() != null)
            {
                Physics2D.IgnoreCollision(colisionadorJugador, objeto, true);
            }
        }
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

    private void ControlarAnimaciones()
    {
        animator.SetFloat(STRING_VELOCIDAD_HORIZONTAL, Mathf.Abs(rb2D.linearVelocity.x));
        animator.SetFloat(STRING_VELOCIDAD_VERTICAL, Mathf.Sign(rb2D.linearVelocity.y));
        animator.SetBool(STRING_EN_SUELO, enSuelo);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
