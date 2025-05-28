using System;
using TMPro;
using UnityEngine;

public class SistemaDiamantes : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoCantidadDiamantes;
    [SerializeField] private int cantidadDiamantes;
    [SerializeField] private int cantidadMaximaDeDiamantes;

    private void Start()
    {
        ActualizarTexto();
    }

    private void OnEnable()
    {
        Diamante.DiamanteRecolectado += SumarDiamantes;
    }

    void OnDisable()
    {
        Diamante.DiamanteRecolectado -= SumarDiamantes;
    }

    private void SumarDiamantes()
    {
        if (cantidadDiamantes + 1 > cantidadMaximaDeDiamantes) { return; }
        cantidadDiamantes += 1;
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        textoCantidadDiamantes.text = cantidadDiamantes.ToString("D2");
    }
}
