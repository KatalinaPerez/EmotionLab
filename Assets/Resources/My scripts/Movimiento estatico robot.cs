using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RobotVolador : MonoBehaviour
{
    // Configuración del movimiento
    public float amplitud = 1.0f; // Altura máxima del movimiento
    public float velocidad = 1.0f; // Velocidad del movimiento
    
    private Vector3 posicionInicial;
    private float offsetAleatorio;

    void Start()
    {
        // Guardamos la posición inicial
        posicionInicial = transform.position;
        
        // Generamos un offset aleatorio para que no todos los robots se muevan igual
        offsetAleatorio = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        // Calculamos el nuevo valor en Y usando una función seno para movimiento suave
        float nuevaY = posicionInicial.y + Mathf.Sin((Time.time + offsetAleatorio) * velocidad) * amplitud;
        
        // Mantenemos la posición en X y Z, solo cambiamos Y
        transform.position = new Vector3(posicionInicial.x, nuevaY, posicionInicial.z);
        
        // Opcional: puedes agregar una pequeña rotación para hacerlo más natural
        float rotacion = Mathf.Sin(Time.time * velocidad * 0.5f) * 5f;
        transform.rotation = Quaternion.Euler(rotacion, 0, 0);
    }
}
