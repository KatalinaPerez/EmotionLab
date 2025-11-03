using UnityEngine;
using UnityEngine.UI;

public class PreguntasPizarra : MonoBehaviour
{
    public Button[] optionButtons;

    private int selectedIndex = -1;
    public Color selectedColor;
    public Color defaultColor = Color.white;

    public void SelectOption(int index)
    {
        selectedIndex = index;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            var image = optionButtons[i].GetComponent<Image>();
            if (image != null)
            {
                image.color = (i == selectedIndex) ? selectedColor : defaultColor;
            }
        }

    }
}
