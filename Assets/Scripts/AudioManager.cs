using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Source------")]
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;

    [Header("------Audio Clips------")]
    public AudioClip bgm;
    public AudioClip gunShot;
    public AudioClip emptyGunShot;
    public AudioClip bottleBreaking;


    private void Start()
    {
        bgmSource.clip = bgm;
        bgmSource.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

}
