using UnityEngine;
using UnityEngine.UI;

public class DialogoPreparacion : MonoBehaviour
{
    public GameObject[] textPanels; // Paneles con cuadros de texto
    private int currentPanelIndex = 0;

    public GameObject robot; // Asignar desde el editor

    private bool flag = false;
    public Transform targetPosition;  // Asigna la posición destino desde el editor


    void Update()
    {

        //ver si el robot está cerca de la posicion deseada
        if (!flag && Vector3.Distance(robot.transform.position, targetPosition.position) < 1)
        {
            flag = true;
        }


        if (flag == true && currentPanelIndex == 0) 
        {
            textPanels[currentPanelIndex].SetActive(true);
            flag = false; // Resetear la bandera para que no se active de nuevo
        }
    }


    public void OnNextButton()
    {
        textPanels[currentPanelIndex].SetActive(false);
        currentPanelIndex++;

        if (currentPanelIndex < textPanels.Length)
        {
            textPanels[currentPanelIndex].SetActive(true);
        }

    }

    public void OnSkipIntroButton()
    {
        foreach (GameObject panel in textPanels)
        {
            panel.SetActive(false);
        }

    }
}
