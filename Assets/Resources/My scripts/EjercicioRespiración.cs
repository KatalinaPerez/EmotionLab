using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BreathingExercise : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject balloonObject;        // Objeto del globo
    public DialogoTips dialogoTips;         // Referencia al script de diálogos
    public int nextPanelIndexAfterExercise; // Panel al que debe ir después

    [Header("Duraciones del ejercicio")]
    public int cycles = 3;                  // Número de respiraciones
    public float inhaleDuration = 4f;       // Segundos inhalar
    public float holdDuration = 7f;         // Segundos sostener
    public float exhaleDuration = 8f;       // Segundos exhalar
    public float scaleMultiplier = 1f;    // Tamaño máximo del globo

    [Header("UI opcional")]
    public Text instructionText;            // 🧘 Texto: “Inhala...”, “Exhala...”
    public Slider progressBar;              // 📊 Barra de progreso (opcional)

    private bool isRunning = false;

    // 🔹 Llamado desde el botón "Iniciar ejercicio"
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

            // Exhalar
            if (instructionText != null) instructionText.text = "Exhala...";
            yield return StartCoroutine(ScaleBalloon(Vector3.one, exhaleDuration));
        }

        // Final del ejercicio
        if (instructionText != null) instructionText.text = "Excelente trabajo 💙";
        yield return new WaitForSeconds(1.5f);

        balloonObject.SetActive(false);
        if (instructionText != null) instructionText.gameObject.SetActive(false);
        if (progressBar != null) progressBar.gameObject.SetActive(false);

        isRunning = false;

        // 🟢 Avanza al siguiente panel del diálogo
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
