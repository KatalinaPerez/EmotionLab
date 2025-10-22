using UnityEngine;
using System.Collections;

public class BreathingExercise : MonoBehaviour
{
    public GameObject balloonObject;        // 🎈 El globo o balón
    public DialogoTips dialogoTips;         // 🔗 Referencia al script de diálogos
    public int nextPanelIndexAfterExercise; // Panel al que debe ir después del ejercicio

    public int cycles = 3;                  // Número de respiraciones
    public float inhaleDuration = 4f;       // Duración inhalar
    public float holdDuration = 7f;         // Duración sostener
    public float exhaleDuration = 8f;       // Duración exhalar
    public float scaleMultiplier = 1f;    // Tamaño máximo del globo al inhalar
    private bool isRunning = false;

    [Header("UI opcional")] //Texto Inhala, Exhala
    public Text intructionText;
    public Slider progressBar; 

    public void StartExercise()
    {
        if (!isRunning)
        {
            StartCoroutine(BreathingRoutine());
        }
    }

    private IEnumerator BreathingRoutine()
    {
        isRunning = true;
        balloonObject.SetActive(true);
        if (instructionText != null) instructionText.gameObject.SetActive(true);
        if (progressBar != null) progressBar.gameObject.SetActive(true);

        for (int i = 0; i < cycles; i++)
        {
            // Inhalar
            if (instructionText != null) instructionText.text = "Inhala...";
            yield return StartCoroutine(ScaleBalloon(Vector3.one * scaleMultiplier, inhaleDuration));

            // Sostener
            if (instructionText != null) instructionText.text = "Sostén...";
            yield return new WaitForSeconds(holdDuration);

            // Exhalar
            if (instructionText != null) instructionText.text = "Exhala...";
            yield return StartCoroutine(ScaleBalloon(Vector3.one, exhaleDuration));
        }
        
        // Final del ejercicio
        if (instructionText != null) instructionText.text = "Excelente trabajo 🙌🏽";
        yield return new WaitForSeconds(1.5f);

        balloonObject.SetActive(false);
        if (instructionText != null) instructionText.gameObject.SetActive(false);
        if (progressBar != null) progressBar.gameObject.SetActive(false);
        isRunning = false;

        // Al terminar el ejercicio, decirle al script de diálogo que avance
        dialogoTips.GoToPanel(nextPanelIndexAfterExercise);
    }

    private IEnumerator ScaleBalloon(Vector3 targetScale, float duration)
    {
        Vector3 initialScale = balloonObject.transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            balloonObject.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            
            // Actualiza la barra de progreso (si existe)
            if (progressBar != null) progressBar.value = t;
            yield return null;
        }
    }
}
