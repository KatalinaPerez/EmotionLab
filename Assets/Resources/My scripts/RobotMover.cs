using UnityEngine;

public class RobotMover : MonoBehaviour
{
    public Transform targetPosition;  // Asigna la posición destino desde el editor
    public float moveSpeed = 2f;

    private bool shouldMove = false;

    void Update()
    {
        if (shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition.position) < 1f)
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