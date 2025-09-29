using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupciones : MonoBehaviour
{
    public Temporizador temporizador;
    public GameObject[] particulas;

    [Header("Prefabs que se apagarán")]
    public GameObject[] prefabs;
    private float tiempoActual;
    private bool interrupcionGenerador = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tiempoActual = temporizador.getTiempoRestante();

        if (!interrupcionGenerador && tiempoActual <= 110)
        {
            InterrumpirGenerador();
            interrupcionGenerador = true;
        }
    }

    public void InterrumpirGenerador()
    {
        foreach (GameObject prefab in prefabs)
        {
            prefab.SetActive(false);
        }

        foreach (GameObject particula in particulas)
        {
            particula.SetActive(true);
        }
    }
}
