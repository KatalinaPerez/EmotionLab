using UnityEngine;
using UnityEngine.UI;

public class PreguntasFormulario : MonoBehaviour
{
    [Tooltip("Si dejas vacío optionButtons, se buscarán todos los Buttons hijos (incluidos inactivos).")]
    public Button[] optionButtons;

    [Tooltip("Nombre exacto del child que actúa como indicador de selección.")]
    public string selectedImageChildName = "SelectedImage";

    private int selectedIndex = -1;

    void Awake()
    {
        // Si no asignaste botones en el Inspector, los buscamos en los hijos
        if (optionButtons == null || optionButtons.Length == 0)
        {
            optionButtons = GetComponentsInChildren<Button>(true);
        }

        // Registrar listeners (limpiando listeners anteriores para evitar duplicados)
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int idx = i; // captura para el listener
            if (optionButtons[i] == null)
            {
                Debug.LogWarning($"[{name}] optionButtons[{i}] es null.");
                continue;
            }

            // quitar listeners previos que podrían venir del editor o de otros scripts
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() =>
            {
                Debug.Log($"[PreguntasFormulario:{name}] Click en botón idx={idx} (name={optionButtons[idx].name})");
                SelectOption(idx);
            });

            // Asegurarse que el indicador inicial esté desactivado
            Transform sel = optionButtons[i].transform.Find(selectedImageChildName);
            if (sel != null)
                sel.gameObject.SetActive(false);
            else
                Debug.LogWarning($"[PreguntasFormulario:{name}] El botón '{optionButtons[i].name}' NO tiene hijo '{selectedImageChildName}'.");
        }
    }

    public void SelectOption(int index)
    {
        if (index < 0 || index >= optionButtons.Length)
        {
            Debug.LogWarning($"[PreguntasFormulario:{name}] SelectOption índice fuera de rango: {index}");
            return;
        }

        selectedIndex = index;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            Transform sel = optionButtons[i].transform.Find(selectedImageChildName);
            if (sel != null)
                sel.gameObject.SetActive(i == selectedIndex);
        }

        Debug.Log($"[PreguntasFormulario:{name}] Seleccionado índice {selectedIndex} -> {optionButtons[selectedIndex].name}");
    }

    public int GetSelectedIndex() => selectedIndex;
}
