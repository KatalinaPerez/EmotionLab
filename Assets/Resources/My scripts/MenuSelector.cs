using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelector : MonoBehaviour
{
    public static string presentacionSeleccionada;
    public static string escenaSeleccionada;

    // Llamado por los botones de selección de presentación
    public void SeleccionarPresentacion(string nombrePresentacion)
    {
        presentacionSeleccionada = nombrePresentacion;
        PlayerPrefs.SetString("PresentacionSeleccionada", nombrePresentacion);
        Debug.Log("Presentación seleccionada: " + nombrePresentacion);
    }

    // Llamado por los botones de selección de escena
    public void SeleccionarEscena(string nombreEscena)
    {
        escenaSeleccionada = nombreEscena;
        PlayerPrefs.SetString("EscenaSeleccionada", nombreEscena);
        Debug.Log("Escena seleccionada: " + nombreEscena);
    }

    // Llamado por el botón "Iniciar"
    public void Iniciar()
    {
        if (string.IsNullOrEmpty(presentacionSeleccionada) || string.IsNullOrEmpty(escenaSeleccionada))
        {
            Debug.LogWarning("Debes seleccionar una presentación y una escena antes de iniciar.");
            return;
        }

        SceneManager.LoadScene(escenaSeleccionada);
    }
}

