using System.Collections;
using UnityEngine;

public class ArreglarGenerador : MonoBehaviour
{
    public Interrupciones interrupciones;
    public float tiempoReparacion = 5f;

    private float tiempoEnTrigger = 0f;
    private bool enReparacion = false;
    private Coroutine reparacionCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ControladorVR"))
        {
            if (!enReparacion)
            {
                enReparacion = true;
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
            Debug.Log("Tiempo en trigger: " + tiempoEnTrigger);
            yield return null;
        }

        Debug.Log("Reparación completada. Desactivando partículas y activando prefabs.");

        if (interrupciones == null)
        {
            Debug.LogError("La referencia a 'interrupciones' es null.");
            yield break;
        }

        Debug.Log("Partículas: " + interrupciones.particulas.Length);
        Debug.Log("Prefabs: " + interrupciones.prefabs.Length);

        foreach (GameObject particula in interrupciones.particulas)
        {
            Debug.Log("Desactivando partícula: " + particula?.name);
            if (particula != null)
                particula.SetActive(false);
        }
        foreach (GameObject prefab in interrupciones.prefabs)
        {
            Debug.Log("Activando prefab: " + prefab?.name);
            if (prefab != null)
                prefab.SetActive(true);
        }
    }

}
