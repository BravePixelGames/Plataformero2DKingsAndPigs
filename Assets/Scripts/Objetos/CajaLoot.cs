using Unity.Cinemachine;
using UnityEngine;

public class CajaLoot : MonoBehaviour, IGolpeable
{
    [Header("Referencias")]
    [SerializeField] private Rigidbody2D[] partesCaja;
    [SerializeField] private GameObject[] lootDeCaja;
    [SerializeField] private Animator animator;
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;

    [Header("Valores Caja")]
    [SerializeField] private int puntosDeVida;
    [SerializeField] private float fuerzaDestruir;
    [SerializeField] private float torqueAlDestruir;

    [Header("Valores Loot")]
    [SerializeField] private Vector2 fuerzaLootRango;
    [SerializeField] private float offsetYLoot;
    [SerializeField] private Vector2 offsetPosicionXLoot;

    public void TomarDaño(int cantidadDaño, Transform sender)
    {
        puntosDeVida -= cantidadDaño;
        animator.SetTrigger("Golpe");

        if (puntosDeVida <= 0)
        {
            DestruirCaja();
        }
    }

    private void DestruirCaja()
    {
        ActivarPartesCaja();
        GenerarLoot();
        cinemachineImpulseSource.GenerateImpulse();
        Destroy(gameObject);
    }

    private void ActivarPartesCaja()
    {
        foreach (Rigidbody2D parteCaja in partesCaja)
        {
            parteCaja.transform.SetParent(null);
            parteCaja.gameObject.SetActive(true);

            Vector2 direccion = parteCaja.transform.position - transform.position;
            parteCaja.AddForce(direccion * fuerzaDestruir, ForceMode2D.Impulse);
            parteCaja.AddTorque(Mathf.Sign(direccion.x) * torqueAlDestruir, ForceMode2D.Impulse);
        }
    }

    private void GenerarLoot()
    {
        foreach (GameObject objeto in lootDeCaja)
        {
            float posicionXAleatoria = Random.Range(offsetPosicionXLoot.x, offsetPosicionXLoot.y);

            Vector3 posicionCrearObjeto = new(
                transform.position.x + posicionXAleatoria,
                transform.position.y + offsetYLoot,
                transform.position.z);

            GameObject objetoCreado = Instantiate(objeto, posicionCrearObjeto, Quaternion.identity);

            if (objetoCreado.TryGetComponent(out Rigidbody2D rb2D))
            {
                Vector2 direccion = objetoCreado.transform.position - transform.position;

                float fuerzaAleatoria = Random.Range(fuerzaLootRango.x, fuerzaLootRango.y);

                rb2D.AddForce(direccion * fuerzaAleatoria, ForceMode2D.Impulse);
            }
        }
    }
}
