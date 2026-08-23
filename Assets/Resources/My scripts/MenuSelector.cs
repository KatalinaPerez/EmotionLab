using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelector : MonoBehaviour
{
    public static string presentacionSeleccionada;
    public static string escenaSeleccionada;

    [Header("Bloqueo modo Difícil")]
    [Tooltip("GameObject 'Dificil' (contenedor de Button_2 en Escenarios). Se oculta por completo " +
             "mientras el modo Fácil no se haya completado en esta sesión.")]
    [SerializeField] private GameObject cajaDificil;

    [Header("Highlight de selección faltante")]
    [Tooltip("Tablero de selección de presentación. Se resalta si se presiona 'Iniciar' sin elegir una.")]
    [SerializeField] private PreguntasPizarra tableroPresentaciones;
    [Tooltip("Tablero de selección de escena (Fácil/Difícil). Se resalta si se presiona 'Iniciar' sin elegir una.")]
    [SerializeField] private PreguntasPizarra tableroEscenarios;

    private void Start()
    {
        if (cajaDificil != null)
        {
            bool desbloqueado = EmotionDataManager.Instance != null && EmotionDataManager.Instance.FacilCompletado;
            cajaDificil.SetActive(desbloqueado);
        }
    }

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
        bool esDificil = nombreEscena == "Salón";
        bool dificilDesbloqueado = EmotionDataManager.Instance != null && EmotionDataManager.Instance.FacilCompletado;

        if (esDificil && !dificilDesbloqueado)
        {
            // La caja "Dificil" debería estar oculta (ver Start()) mientras esto sea falso;
            // este chequeo es solo una red de seguridad por si igual se llega a llamar.
            Debug.LogWarning("Modo Difícil bloqueado: primero debe completarse el modo Fácil.");
            return;
        }

        escenaSeleccionada = nombreEscena;
        PlayerPrefs.SetString("EscenaSeleccionada", nombreEscena);
        Debug.Log("Escena seleccionada: " + nombreEscena);

        // Registrar dificultad en EmotionDataManager para el JSON de sesión
        string dificultad = esDificil ? "dificil" : "facil";
        PlayerPrefs.SetString("DificultadSeleccionada", dificultad);
        EmotionDataManager.Instance?.SetDifficultyLevel(dificultad);
    }

    // Llamado por el botón "Iniciar"
    public void Iniciar()
    {
        bool faltaPresentacion = string.IsNullOrEmpty(presentacionSeleccionada);
        bool faltaEscena = string.IsNullOrEmpty(escenaSeleccionada);

        if (faltaPresentacion || faltaEscena)
        {
            Debug.LogWarning("Debes seleccionar una presentación y una escena antes de iniciar.");

            if (faltaPresentacion && tableroPresentaciones != null)
                tableroPresentaciones.ResaltarOpciones();
            if (faltaEscena && tableroEscenarios != null)
                tableroEscenarios.ResaltarOpciones();

            return;
        }

        SceneManager.LoadScene(escenaSeleccionada);
    }
}

