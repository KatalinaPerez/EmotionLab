using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Diálogo de cierre para que conteste las ultimas preguntas
///
/// La lógica de navegación (textPanels, currentPanelIndex, OnNextButton,
/// OnSkipIntroButton) vive en <see cref="DialogoBase"/>.
/// </summary>
public class DialogoCierre : DialogoBase
{
    [Header("Referencias del cierre")]
    [Tooltip("Objeto Clipboard")]
    public GameObject clipboard;
    public Button btnVamosAlCierre;
    public string escenaSiguiente = "Cierre";

    private void Start()
    {
        if (clipboard != null)
            clipboard.SetActive(false);

        if (btnVamosAlCierre != null)        
            btnVamosAlCierre.interactable = false;
    }

    ///<summary>
    /// Sobreescribimos el avance de diálogo para mostrar el clipboard al llegar al último panel, y habilitar el botón para avanzar a la escena de cierre.
    /// </summary>

    public override void OnNextButton()
    {
        //Antes de avanzar al siguiente panel, verificamos si estamos en el último panel.
        if (currentPanelIndex == 1)
        {
            // Si es el último panel, mostramos el clipboard y habilitamos el botón para avanzar a la escena de cierre.
            if (clipboard != null)
                clipboard.SetActive(true);
        }
        else
        {
            // Si no es el último panel, avanzamos normalmente.
            base.OnNextButton();
        }
    }

    ///<summary>
        /// Funcion que llamarpa el formulario al responder la última pregunta.
        ///</summary>
    public void OnFormularioFinalizado()
    {
        //1. DEsaparece el clipboard
        if (clipboard != null)
            clipboard.SetActive(false);

        //2. Se habilita boton "Vamos al cierre"
        if (btnVamosAlCierre != null)
            btnVamosAlCierre.interactable = true;
    }

    /// <summary>
    /// Se ejecuta cuando se pulsa el botón "Vamos al cierre" (ya que es el último panel de la secuencia).
    /// </summary>
    
    protected override void OnSequenceFinished()
    {
        // Cargamos la escena de cierre
        if (!string.IsNullOrEmpty(escenaSiguiente))
            SceneManager.LoadScene(escenaSiguiente);
        else
            Debug.LogWarning("[DialogoCierre] Escena siguiente no asignada en el Inspector.");
    }
}
