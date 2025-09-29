using UnityEngine;
using TMPro;

public class Temporizador : MonoBehaviour
{
    [Header("Configuración")]
    public TMP_Text displayText;
    public float tiempoInicial = 60f; // Tiempo en segundos
    public bool autoIniciar = false;

    [Header("Formato")]
    public bool mostrarHoras = false;
    public bool mostrarMilisegundos = false;
    public GameObject robot; // Asignar desde el editor
    public GameObject[] textPanels;
    private int currentPanelIndex = 0;

    private float tiempoRestante;
    private bool temporizadorActivo = false;

    void Start()
    {
        tiempoRestante = tiempoInicial;
        ActualizarDisplay();

        if (autoIniciar)
            IniciarTemporizador();
    }

    void Update()
    {
        if (temporizadorActivo && tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarDisplay();

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                temporizadorActivo = false;
                TemporizadorCompletado();
            }
        }
        else if (tiempoRestante <= 0)
        {
            PausarTemporizador();
            textPanels[currentPanelIndex].SetActive(true);
            robot.SetActive(true);
        }
    }

    private void ActualizarDisplay()
    {
        if (mostrarHoras)
            displayText.text = FormatearConHoras(tiempoRestante);
        else if (mostrarMilisegundos)
            displayText.text = FormatearConMilisegundos(tiempoRestante);
        else
            displayText.text = FormatearTiempoBasico(tiempoRestante);
    }

    private string FormatearTiempoBasico(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        return string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    private string FormatearConMilisegundos(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        int milisegundos = Mathf.FloorToInt((tiempo * 1000) % 1000);
        return string.Format("{0:00}:{1:00}.{2:000}", minutos, segundos, milisegundos);
    }

    private string FormatearConHoras(float tiempo)
    {
        int horas = Mathf.FloorToInt(tiempo / 3600);
        int minutos = Mathf.FloorToInt((tiempo % 3600) / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);
    }

    // Métodos públicos para controlar el temporizador
    public void IniciarTemporizador()
    {
        temporizadorActivo = true;
    }

    public void PausarTemporizador()
    {
        temporizadorActivo = false;
    }

    public void ReiniciarTemporizador()
    {
        tiempoRestante = tiempoInicial;
        ActualizarDisplay();
    }

    public void ConfigurarTiempo(float nuevoTiempo)
    {
        tiempoInicial = nuevoTiempo;
        ReiniciarTemporizador();
    }

    // Evento cuando el temporizador llega a cero
    private void TemporizadorCompletado()
    {
        Debug.Log("¡Tiempo completado!");
        // Aquí puedes añadir efectos de sonido, animaciones, etc.
    }

    public bool getTemporizadorActivo()
    {
        return temporizadorActivo;
    }
    
    public float getTiempoRestante()
    {
        return tiempoRestante;
    }
}