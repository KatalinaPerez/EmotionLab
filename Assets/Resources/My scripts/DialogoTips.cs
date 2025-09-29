using UnityEngine;
using UnityEngine.UI;

public class DialogoTips : MonoBehaviour
{
    public GameObject[] textPanels; // Paneles con cuadros de texto
    private int currentPanelIndex = 0;
    private bool flag = true;

    void Update()
    {
        if (flag)
        {
            textPanels[currentPanelIndex].SetActive(true);
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

    public void OnSkipTipsButton()
    {
        textPanels[currentPanelIndex].SetActive(false);
        currentPanelIndex = 6; // Mostrar el panel que comienza la presentacion.
        textPanels[currentPanelIndex].SetActive(true); 

    }

    public void OnExitButton()
    {
        //Desactivar todos los paneles
        foreach (GameObject panel in textPanels)
        {
            panel.SetActive(false);
        }
        flag = false; // Cambiar el estado del flag para evitar que se muestre el primer panel de nuevo
    }
}
