using System.Collections;
using UnityEngine;

/// <summary>
/// Posiciona y orienta el panel de consentimiento (Canvas) frente al jugador
/// justo al iniciar la escena, usando la dirección hacia donde está mirando
/// la cámara VR en ese momento.
///
/// Por qué hace falta: la posición del Canvas en la escena es fija en el
/// mundo (m_LocalPosition), pero hacia dónde mira el jugador al ponerse el
/// casco depende de la orientación real calibrada del dispositivo (Guardian /
/// recentrado), que puede no coincidir con el eje +Z de la escena. Este
/// script recalcula posición y rotación del panel en base a la cámara real,
/// así siempre aparece de frente sin importar hacia dónde estaba mirando el
/// jugador al iniciar.
///
/// Adjuntar este script al GameObject "Canvas" de la escena Consentimiento.
/// </summary>
public class PosicionarPanelFrenteJugador : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Cámara del jugador (Main Camera del XR Origin). Si se deja vacío, se usa Camera.main")]
    [SerializeField] private Transform camaraJugador;

    [Header("Configuración")]
    [Tooltip("Distancia (en metros) a la que aparecerá el panel frente al jugador")]
    [SerializeField] private float distancia = 3f;

    [Tooltip("Altura fija (en metros, eje Y global) a la que se ubica el panel")]
    [SerializeField] private float altura = 1.5f;

    IEnumerator Start()
    {
        // Esperar un frame para asegurarse de que la pose de la cámara XR
        // ya esté aplicada (en el primer frame puede no estar lista todavía).
        yield return null;

        Transform camara = camaraJugador != null
            ? camaraJugador
            : (Camera.main != null ? Camera.main.transform : null);

        if (camara == null)
        {
            Debug.LogWarning("[PosicionarPanelFrenteJugador] No se encontró la cámara del jugador.");
            yield break;
        }

        // Dirección horizontal hacia donde mira el jugador (ignorando la
        // inclinación de la cabeza hacia arriba/abajo).
        Vector3 direccion = camara.forward;
        direccion.y = 0f;
        if (direccion.sqrMagnitude < 0.0001f)
            direccion = camara.up;
        direccion.Normalize();

        // Ubicar el panel frente al jugador, a la distancia y altura configuradas.
        Vector3 nuevaPosicion = camara.position + direccion * distancia;
        nuevaPosicion.y = altura;
        transform.position = nuevaPosicion;

        // Rotar el panel para que quede mirando de frente hacia el jugador.
        transform.rotation = Quaternion.LookRotation(direccion, Vector3.up);
    }
}
