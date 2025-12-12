using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource; //su camera in genere
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip playerWalking;
    public AudioClip weaponShoot;
}
