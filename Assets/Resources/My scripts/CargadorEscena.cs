using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Componente simple para cargar una escena desde un botón del Inspector (OnClick).
/// Pensado para el botón 'Comenzar' que aparece junto al robot al expirar el
/// temporizador en Salón / Oficina, para pasar a la escena de Cierre.
///
/// Uso:
///   1. Añadir este componente al GameObject del botón.
///   2. Configurar 'nombreEscena' en el Inspector (ej: "Cierre"). Debe estar
///      añadida en File > Build Settings.
///   3. En el botón -> OnClick() -> arrastrar el GameObject -> seleccionar
///      CargadorEscena.CargarEscena().
/// </summary>
public class CargadorEscena : MonoBehaviour
{
    [Tooltip("Nombre exacto de la escena a cargar. Debe estar añadida en File > Build Settings.")]
    public string nombreEscena = "Cierre";

    [Tooltip("Si no está vacío ('facil' | 'dificil'), al presionar este botón marca ese modo como " +
             "completado en EmotionDataManager antes de cargar la escena. Dejar vacío si este botón " +
             "no representa el fin de un modo Fácil/Difícil.")]
    public string modoCompletadoAlFinalizar = "";

    /// <summary>
    /// Carga la escena configurada en 'nombreEscena'.
    /// </summary>
    public void CargarEscena()
    {
        if (!string.IsNullOrEmpty(modoCompletadoAlFinalizar))
            EmotionDataManager.Instance?.MarcarModoCompletado(modoCompletadoAlFinalizar);

        if (string.IsNullOrEmpty(nombreEscena))
        {
            Debug.LogWarning("[CargadorEscena] 'nombreEscena' está vacío. No se hace transición.");
            return;
        }

        // EmotionDataManager hace auto-guardado a JSON al descargar la escena.
        Debug.Log($"[CargadorEscena] Cargando escena: '{nombreEscena}'");
        SceneManager.LoadScene(nombreEscena);
    }
}
