using UnityEngine;
using UnityEngine.Events;

public class FormularioCierre : MonoBehaviour
{
    [Header("Preguntas del formulario")]
    [Tooltip("Evento que se dispara al responder todas las preguntas")]
    public UnityEvent onFormularioCompletado;
    
    //Guardamos las preguntas de forma interna para no arrastrar nada
    private PreguntasFormulario[] preguntasEncontradas;
    private bool formularioYaFinalizado = false;

    private void Start()
    {
        // El script busca automáticamente todas las preguntas que estén dentro del Clipboard
        preguntasEncontradas = GetComponentsInChildren<PreguntasFormulario>(true);

        if (preguntasEncontradas.Length == 0)
        {
            Debug.LogWarning("[FormularioCierreManager] ¡Ojo! No se encontró ningún script 'PreguntasFormulario' hijo de este objeto.");
            return;
        }

        // Nos suscribimos a los botones de todas las preguntas detectadas
        foreach (PreguntasFormulario pregunta in preguntasEncontradas)
        {
            if (pregunta == null) continue;

            for (int i = 0; i < pregunta.optionButtons.Length; i++)
            {
                if (pregunta.optionButtons[i] == null) continue;
                
                // Escuchamos los clics de las caritas en tiempo real
                pregunta.optionButtons[i].onClick.AddListener(VerificarFormulario);
            }
        }
    }

    /// <summary>
    /// Comprueba si absolutamente todas las preguntas del portapapeles tienen una respuesta seleccionada.
    /// </summary>
    public void VerificarFormulario()
    {
        if (formularioYaFinalizado) return;

        // Validamos una por una
        foreach (PreguntasFormulario pregunta in preguntasEncontradas)
        {
            if (pregunta == null) continue;

            if (!pregunta.HasAnswer())
            {
                // Si falta aunque sea una sola carita por marcar, frena la verificación
                return; 
            }
        }

        // Si el código pasa el bucle, significa que completaron todo el formulario
        formularioYaFinalizado = true;

        // Registrar cierre de sesión y guardar JSON antes de continuar
        var manager = EmotionDataManager.Instance;
        if (manager != null)
        {
            manager.FinalizarSesion();
            manager.GuardarAJson();
        }
        else
        {
            Debug.LogWarning("[FormularioCierre] EmotionDataManager no encontrado — sesión no guardada.");
        }

        // Avisamos al DialogoCierre para que apague el portapapeles y libere el botón
        if (onFormularioCompletado != null)
        {
            onFormularioCompletado.Invoke();
        }
    }
    
}