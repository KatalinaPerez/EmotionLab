using UnityEngine;
using UnityEngine.UI;

public class PreguntasFormulario : MonoBehaviour
{
    public Button[] optionButtons;

    private int selectedIndex = -1;
    public Sprite selectedImagePrefab;

    public void SelectOption(int index)
    {
        selectedIndex = index;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            var selectedImage = optionButtons[i].transform.Find("SelectedImage");
            if (selectedImage != null)
            {
                selectedImage.gameObject.SetActive(i == selectedIndex);
                Instantiate(selectedImagePrefab, optionButtons[i].transform);
            }
        }

    }
}
