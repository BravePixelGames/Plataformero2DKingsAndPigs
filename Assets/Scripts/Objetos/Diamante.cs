using System;
using UnityEngine;

public class Diamante : MonoBehaviour
{
    public static Action DiamanteRecolectado;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Recolectar();
        }
    }

    private void Recolectar()
    {
        DiamanteRecolectado?.Invoke();
        Destroy(gameObject);
    }
}
