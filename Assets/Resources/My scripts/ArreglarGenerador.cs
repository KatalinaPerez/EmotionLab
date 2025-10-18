using System.Collections;
using UnityEngine;
using TMPro; // <- necesario para usar TextMeshPro

public class ArreglarGenerador : MonoBehaviour
{
    public Interrupciones interrupciones;
    public float tiempoReparacion = 5f;

    private float tiempoEnTrigger = 0f;
    private bool enReparacion = false;
    private Coroutine reparacionCoroutine;

    [Header("UI de Reparación")]
    public GameObject textoReparando; // <- arrastrar aquí el texto desde el inspector

    private void Start()
    {
        // Aseguramos que el texto esté oculto al inicio
        if (textoReparando != null)
            textoReparando.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ControladorVR"))
        {
            if (!enReparacion)
            {
                enReparacion = true;
                if (textoReparando != null)
                    textoReparando.SetActive(true); // <- mostrar mensaje
                reparacionCoroutine = StartCoroutine(RepararGenerador());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ControladorVR"))
        {
            enReparacion = false;
            tiempoEnTrigger = 0f;
            if (reparacionCoroutine != null)
                StopCoroutine(reparacionCoroutine);
            if (textoReparando != null)
                textoReparando.SetActive(false); // <- ocultar mensaje
        }
    }

    private IEnumerator RepararGenerador()
    {
        tiempoEnTrigger = 0f;
        while (tiempoEnTrigger < tiempoReparacion)
        {
            if (!enReparacion)
                yield break;

            tiempoEnTrigger += Time.deltaTime;
            yield return null;
        }

        // Reparación completada
        if (textoReparando != null)
            textoReparando.SetActive(false);

        if (interrupciones == null)
        {
            Debug.LogError("La referencia a 'interrupciones' es null.");
            yield break;
        }

        foreach (GameObject particula in interrupciones.particulas)
            if (particula != null) particula.SetActive(false);

        foreach (GameObject prefab in interrupciones.prefabs)
            if (prefab != null) prefab.SetActive(true);
    }
}
