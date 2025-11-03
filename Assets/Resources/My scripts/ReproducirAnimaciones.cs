using UnityEngine;
using System.Collections;

public class ReproducirAnimaciones : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Enable to play animations automatically")]
    public bool autoPlayAnimations = true;

    [SerializeField]
    [Tooltip("Minimum time between animations (seconds)")]
    public float minTimeBetweenAnimations = 5f;

    [SerializeField]
    [Tooltip("Maximum time between animations (seconds)")]
    public float maxTimeBetweenAnimations = 10f;

    private Animator animator;
    private AnimationClip[] animationClips;

    void Start()
    {
        animator = GetComponent<Animator>();
        
        if (animator != null && autoPlayAnimations)
        {
            // Obtiene todos los clips de animación del Animator Controller
            animationClips = animator.runtimeAnimatorController.animationClips;
            
            if (animationClips.Length > 0)
            {
                PlayRandomAnimation();
                StartCoroutine(PlayAnimationsLoop());
            }
            else
            {
                Debug.LogWarning("No animation clips found in the Animator Controller.");
            }
        }
    }

    // Reproduce una animación aleatoria
    private void PlayRandomAnimation()
    {
        AnimationClip randomClip = animationClips[Random.Range(0, animationClips.Length)];
        animator.CrossFadeInFixedTime(randomClip.name, 0.3f); // Transición suave de 0.3 segundos
    }

    // Bucle infinito para cambiar animaciones
    private IEnumerator PlayAnimationsLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenAnimations, maxTimeBetweenAnimations);
            yield return new WaitForSeconds(waitTime);
            PlayRandomAnimation();
        }
    }
}