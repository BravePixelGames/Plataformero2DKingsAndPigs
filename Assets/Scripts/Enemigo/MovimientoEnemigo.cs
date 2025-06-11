using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;
    [SerializeField] private EstadosEnemigo estadoActual;
    [SerializeField] private LayerMask capasSuelo;

    [Header("Movimiento Horizontal")]
    [SerializeField] private float velocidadDeMovimientoBase;
    [SerializeField] private float velocidadDeMovimientoActual;
    [SerializeField] private Transform controladorFrente;
    [SerializeField] private float distanciaRayoFrente;
    private bool tocandoSueloFrente;

    [SerializeField] private Transform controladorFrenteArriba;
    [SerializeField] private bool tocandoSueloFrenteArriba;

    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distanciaRayoSuelo;
    [SerializeField] private bool tocandoSuelo;

    [Header("Esperar")]
    [SerializeField] private float tiempoAEsperar;
    private float tiempoAEsperarActual;

    [Header("Saltar")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private Vector2 dimensionesCaja;
    [SerializeField] private Transform controladorEstaEnSuelo;
    [SerializeField] private bool estaEnElSuelo;


    private void Update()
    {
        tocandoSueloFrente = Physics2D.Raycast(controladorFrente.position, transform.right * -1, distanciaRayoFrente, capasSuelo);
        tocandoSueloFrenteArriba = Physics2D.Raycast(controladorFrenteArriba.position, transform.right * -1, distanciaRayoFrente, capasSuelo);

        tocandoSuelo = Physics2D.Raycast(controladorSuelo.position, transform.up * -1, distanciaRayoSuelo, capasSuelo);
        estaEnElSuelo = Physics2D.OverlapBox(controladorEstaEnSuelo.position, dimensionesCaja, 0f, capasSuelo);

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
            case EstadosEnemigo.Saltar:
                ComportamientoSaltar();
                break;
        }
    }

    private void ComportamientoCorrer()
    {
        Correr();

        if (tocandoSueloFrente)
        {
            if (tocandoSueloFrenteArriba)
            {
                Girar();
                CambiarAEstadoEsperar();
            }
            else
            {
                Saltar();
                CambiarAEstadoSaltar();
            }
        }

        if (!tocandoSuelo && estaEnElSuelo)
        {
            int valorAleatorio = Random.Range(0, 2);

            if (valorAleatorio == 0)
            {
                Saltar();
                CambiarAEstadoSaltar();
            }
            else
            {
                Girar();
                CambiarAEstadoEsperar();
            }
        }
    }

    private void ComportamientoEsperar()
    {
        if (tiempoAEsperarActual > 0)
        {
            tiempoAEsperarActual -= Time.fixedDeltaTime;
        }
        else
        {
            CambiarAEstadoCorrer();
        }
    }

    private void ComportamientoSaltar()
    {
        Correr();

        if (estaEnElSuelo)
        {
            CambiarAEstadoCorrer();
        }
    }

    private void CambiarAEstadoEsperar()
    {
        velocidadDeMovimientoActual = 0;
        rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocity.y);
        estadoActual = EstadosEnemigo.Esperar;
        tiempoAEsperarActual = tiempoAEsperar;
    }

    private void CambiarAEstadoCorrer()
    {
        estadoActual = EstadosEnemigo.Correr;
        velocidadDeMovimientoActual = velocidadDeMovimientoBase;
    }

    private void CambiarAEstadoSaltar()
    {
        estadoActual = EstadosEnemigo.Saltar;
    }

    private void Saltar()
    {
        rb2D.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
    }

    private void Correr()
    {
        float direccion = transform.eulerAngles.y == 0 ? -1 : 1;
        rb2D.linearVelocity = new Vector2(velocidadDeMovimientoActual * direccion, rb2D.linearVelocity.y);
    }

    private void Girar()
    {
        Vector3 rotacion = transform.eulerAngles;
        rotacion.y = rotacion.y == 0 ? 180 : 0;
        transform.eulerAngles = rotacion;
    }

    private void ControlarAnimaciones()
    {
        animator.SetFloat("VelocidadHorizontal", Mathf.Abs(rb2D.linearVelocity.x));
        animator.SetFloat("VelocidadVertical", Mathf.Sign(rb2D.linearVelocity.y));
        animator.SetBool("EnSuelo", estaEnElSuelo);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorFrente.position, controladorFrente.position + distanciaRayoFrente * transform.right * -1);
        Gizmos.DrawLine(controladorFrenteArriba.position, controladorFrenteArriba.position + distanciaRayoFrente * transform.right * -1);

        Gizmos.DrawLine(controladorSuelo.position, controladorSuelo.position + distanciaRayoSuelo * transform.up * -1);
        Gizmos.DrawWireCube(controladorEstaEnSuelo.position, dimensionesCaja);
    }
}
