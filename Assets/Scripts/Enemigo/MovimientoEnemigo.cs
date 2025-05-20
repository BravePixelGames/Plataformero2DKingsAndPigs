using System;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;
    [SerializeField] private EstadosEnemigo estadoActual;

    [Header("Movimiento Horizontal")]
    [SerializeField] private float velocidadDeMovimientoActual;
    [SerializeField] private float ultimaVelocidadDeMovimiento;
    [SerializeField] private Transform controladorFrente;
    [SerializeField] private float distanciaRayoFrente;
    [SerializeField] private LayerMask capasSuelo;
    private bool tocandoSueloFrente;

    [Header("Esperar")]
    [SerializeField] private float tiempoAEsperar;
    private float tiempoAEsperarActual;


    private void Update()
    {
        tocandoSueloFrente = Physics2D.Raycast(controladorFrente.position, transform.right * -1, distanciaRayoFrente, capasSuelo);

        if (tiempoAEsperarActual > 0)
        {
            tiempoAEsperarActual -= Time.deltaTime;
        }

        ControlarAnimaciones();
    }

    private void FixedUpdate()
    {
        switch (estadoActual)
        {
            case EstadosEnemigo.Correr:
                ComportamientoCorrer();
                break;
            case EstadosEnemigo.Esperar:
                ComportamientoEsperar();
                break;
        }
    }

    private void ComportamientoCorrer()
    {
        rb2D.linearVelocity = new Vector2(velocidadDeMovimientoActual, rb2D.linearVelocity.y);

        if (tocandoSueloFrente)
        {
            Girar();
            CambiarAEstadoEsperar();
        }

        MirarEnDireccionDelMovimiento();
    }

    private void CambiarAEstadoCorrer()
    {
        velocidadDeMovimientoActual = ultimaVelocidadDeMovimiento * -1;
        estadoActual = EstadosEnemigo.Correr;
    }

    private void CambiarAEstadoEsperar()
    {
        ultimaVelocidadDeMovimiento = velocidadDeMovimientoActual;
        velocidadDeMovimientoActual = 0;
        rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocity.y);
        estadoActual = EstadosEnemigo.Esperar;
        tiempoAEsperarActual = tiempoAEsperar;
    }

    private void ComportamientoEsperar()
    {
        if (tiempoAEsperarActual < 0)
        {
            CambiarAEstadoCorrer();
        }
    }

    private void MirarEnDireccionDelMovimiento()
    {
        if ((velocidadDeMovimientoActual > 0 && !MirandoALaDerecha()) || (velocidadDeMovimientoActual < 0 && MirandoALaDerecha()))
        {
            Girar();
        }
    }

    private void Girar()
    {
        Vector3 rotacion = transform.eulerAngles;
        rotacion.y = rotacion.y == 0 ? 180 : 0;
        transform.eulerAngles = rotacion;
    }

    private bool MirandoALaDerecha()
    {
        return transform.eulerAngles.y == 180;
    }

    private void ControlarAnimaciones()
    {
        animator.SetFloat("VelocidadHorizontal", Mathf.Abs(rb2D.linearVelocity.x));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorFrente.position, controladorFrente.position + distanciaRayoFrente * transform.right * -1);
    }
}
