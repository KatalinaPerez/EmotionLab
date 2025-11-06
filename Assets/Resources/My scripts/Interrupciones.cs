using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupciones : MonoBehaviour
{
    public Temporizador temporizador;
    public GameObject[] particulas;
    [Header("Prefabs que se apagarán")]
    public GameObject[] prefabs;
    [Header("Control de Música")]
    public AudioSource musicaDeFondo;
    public AudioSource musicaDeInterrupcion;
    [Header("UI")]
    public GameObject textoNotificacion;
    private float tiempoActual;
    private bool interrupcionGenerador = false;
    // Start is called before the first frame update
    void Start()
    {
        // Asegurarnos de que la música de interrupción esté lista pero en silencio.
        if (musicaDeInterrupcion != null)
        {
            musicaDeInterrupcion.volume = 0;
            musicaDeInterrupcion.Play(); // La ponemos en play, pero con volumen 0
        }
        // Nos aseguramos de que el texto de notificación esté oculto
        if (textoNotificacion != null)
        {
            textoNotificacion.SetActive(false);
        }
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
        // Mostrar el texto de notificación
        if (textoNotificacion != null)
        {
            textoNotificacion.SetActive(true);
        }

        // Bajar la música de fondo y subir la de interrupción
        if (musicaDeFondo != null)
        {
            musicaDeFondo.volume = 0f; 
        }
        
        if (musicaDeInterrupcion != null)
        {
            musicaDeInterrupcion.volume = 0.8f; 
        }
    }
    // Necesitarás llamar a esta función cuando el evento TERMINE
    public void TerminarInterrupcionGenerador()
    {
        // Revertir todo
        foreach (GameObject prefab in prefabs)
        {
            prefab.SetActive(true);
        }
        foreach (GameObject particula in particulas)
        {
            particula.SetActive(false);
        }
        // Ocultar el texto de notificación
        if (textoNotificacion != null)
        {
            textoNotificacion.SetActive(false);
        }
        // Bajar la música de interrupción y subir la de fondo
        if (musicaDeFondo != null)
        {
            musicaDeFondo.volume = 0.1f; // Vuelve al volumen normal
        }
        
        if (musicaDeInterrupcion != null)
        {
            musicaDeInterrupcion.volume = 0; // Silencia la música de interrupción
        }
    }
    // Esta función PÚBLICA solo oculta el texto de notificación
    public void OcultarNotificacion()
    {
        if (textoNotificacion != null)
        {
            textoNotificacion.SetActive(false);
        }
    }
    // Esta función PÚBLICA solo muestra el texto de notificación
    public void MostrarNotificacion()
    {
        if (textoNotificacion != null)
        {
            textoNotificacion.SetActive(true);
        }
    }  
}
