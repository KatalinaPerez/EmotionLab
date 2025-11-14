using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{
    void Awake()
    {
        // Busca otros objetos con este mismo script
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("MusicPlayer");

        // Si ya existe uno, destruye este nuevo
        if (musicObjects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        // Si este es el primero, no lo destruyas al cargar una nueva escena
        DontDestroyOnLoad(this.gameObject);
    }
}