using UnityEngine;

public class RobotMover : MonoBehaviour
{
    [Header("Movimiento")]
    public Transform targetPosition;   // Posición final del robot
    public float moveSpeed = 2f;

    [Header("Rotación hacia el jugador")]
    public Transform lookTarget;       // Asigna aquí el jugador (por ejemplo, la cámara del VR)
    public float rotationSpeed = 2f;

    private bool shouldMove = false;

    void Update()
    {
        if (shouldMove)
        {
            // Movimiento hacia el destino
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition.position,
                moveSpeed * Time.deltaTime
            );

            // Rotación hacia el jugador (si está asignado)
            if (lookTarget != null)
            {
                Vector3 direction = (lookTarget.position - transform.position).normalized;
                direction.y = 0f; // evita que mire hacia arriba o abajo
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }

            // Detener el movimiento al llegar
            if (Vector3.Distance(transform.position, targetPosition.position) < 0.5f)
            {
                shouldMove = false;
            }
        }
    }

    public void StartMoving()
    {
        shouldMove = true;
    }
}
