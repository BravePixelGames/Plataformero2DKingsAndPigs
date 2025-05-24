using UnityEngine;

public class CorazonUI : MonoBehaviour
{
    private const string STRING_RESTAURAR_ANIMACION = "Restaurar";
    private const string STRING_GOLPE_ANIMACION = "Golpe";

    [SerializeField] private Animator animator;
    [SerializeField] private bool estaActivo;

    public void ActivarCorazon()
    {
        animator.SetTrigger(STRING_RESTAURAR_ANIMACION);
        estaActivo = true;
    }

    public void DesactivarCorazon()
    {
        animator.SetTrigger(STRING_GOLPE_ANIMACION);
        estaActivo = false;
    }

    public bool EstaActivo() => estaActivo;
}
