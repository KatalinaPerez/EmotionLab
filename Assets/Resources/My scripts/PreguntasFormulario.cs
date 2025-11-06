using UnityEngine;
using UnityEngine.UI;

public class PreguntasFormulario : MonoBehaviour
{
    [Header("Botones de emociones para esta pregunta")]
    public Button[] optionButtons;

    [Header("Sprite para marcar selección")]
    public Sprite selectedImagePrefab;

    private int selectedIndex = -1;

    void Start()
    {
        foreach (Button btn in optionButtons)
        {
            btn.onClick.RemoveAllListeners(); 
        }

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i; 
            optionButtons[i].onClick.AddListener(() => SelectOption(index));
        }
    }

    public void SelectOption(int index)
{
    for (int i = 0; i < optionButtons.Length; i++)
    {
        Transform selectedImage = optionButtons[i].transform.Find("SelectedImage");
        if (selectedImage != null)
            selectedImage.gameObject.SetActive(false);
    }

    selectedIndex = index;

    Transform target = optionButtons[selectedIndex].transform.Find("SelectedImage");
    if (target != null)
        target.gameObject.SetActive(true);
}

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }
}
