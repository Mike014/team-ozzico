using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;       // L’AudioSource del player
    public AudioClip footstepClip;        // Il tuo Footsteps.wav

    [Header("Step Settings")]
    public float stepInterval = 0.35f;    // Quanto tempo tra un passo e l'altro
    public float pitchRandomness = 0.1f;  // Varietà nel suono

    private TopDownMover2D mover;
    private float stepTimer;

    void Awake()
    {
        mover = GetComponent<TopDownMover2D>();
    }

    // void Update()
    // {
    //     Vector2 velocity = mover.GetVelocity();   // SE mover ha un metodo per la velocità

    //     // Se ti serve, puoi usare mover.Input invece della velocità
    
    //     if (velocity.magnitude > 0.1f)
    //     {
    //         stepTimer -= Time.deltaTime;

    //         if (stepTimer <= 0f)
    //         {
    //             PlayFootstep();
    //             stepTimer = stepInterval;
    //         }
    //     }
    //     else
    //     {
    //         // Player fermo → reset del timer
    //         stepTimer = 0f;
    //     }
    // }

    // void PlayFootstep()
    // {
    //     audioSource.pitch = 1f + Random.Range(-pitchRandomness, pitchRandomness);
    //     audioSource.PlayOneShot(footstepClip);
    // }
}
