using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource FXAudioSource;
    public AudioClip VilageMusic;
    public AudioClip GameMusic;
    public UnityEngine.UI.Slider musicSlider;
    public UnityEngine.UI.Slider FXSlider;
    public float musicVolume;
    public float fxVolume;
    // Start is called before the first frame update
    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
    // Update is called once per frame
    void Update()
    {
        musicVolume = musicSlider.value;
        musicAudioSource.volume = musicVolume;
        fxVolume = FXSlider.value;
        FXAudioSource.volume = fxVolume;
    }


    
    
    public void PlayVilageMusic()
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = VilageMusic;
        musicAudioSource.Play();
    }
    public void PlayGameMusic()
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = GameMusic;
        musicAudioSource.Play();
    }
    public void PlayerFx(AudioClip clip)
    {
        FXAudioSource.PlayOneShot(clip);
    }
}
