using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource effectAudioSource;
    [SerializeField] private AudioSource defaultAudioSource;
    [SerializeField] private AudioSource bossAudioSource;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioClip energyClip;
    void Awake()
    {

         if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); // Giữ lại GameManager khi chuyển scene
    }

    public void PlayShootSound()
    {
        effectAudioSource.PlayOneShot(shootClip);
    }

    public void PlayReloadSound()
    {
        effectAudioSource.PlayOneShot(reloadClip);
    }

    public void PlayEnergySound()
    {
        effectAudioSource.PlayOneShot(energyClip);
    }
    public void PlayDefaultAudio()
{
    bossAudioSource.Stop();
    defaultAudioSource.Play();
}

public void PlayBossAudio()
{
    defaultAudioSource.Stop();
    bossAudioSource.Play();
}

public void StopAudioGame()
{
    effectAudioSource.Stop();
    bossAudioSource.Stop();
    defaultAudioSource.Stop();
}

}
