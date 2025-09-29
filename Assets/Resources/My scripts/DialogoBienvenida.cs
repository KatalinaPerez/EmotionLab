using UnityEngine;
using UnityEngine.UI;

public class DialogoBienvenida : MonoBehaviour
{
    public GameObject[] textPanels; // Paneles con cuadros de texto
    private int currentPanelIndex = 0;

    public RobotMover robotMover; // Asignar desde el editor

    public void OnNextButton()
    {
        textPanels[currentPanelIndex].SetActive(false);
        currentPanelIndex++;

        if (currentPanelIndex < textPanels.Length)
        {
            textPanels[currentPanelIndex].SetActive(true);
        }
        else
        {
            // Fin de la intro, mover robot
            robotMover.StartMoving();
        }
    }

    public void OnSkipIntroButton()
    {
        foreach (GameObject panel in textPanels)
        {
            panel.SetActive(false);
        }

        robotMover.StartMoving();
    }
}
