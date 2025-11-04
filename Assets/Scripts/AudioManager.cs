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
    private float musicVolume;
    private float fxVolume;
    // Start is called before the first frame update
    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayVilageMusic();
    }
    // Update is called once per frame
    void Update()
    {
        musicVolume = musicSlider.value;
        musicAudioSource.volume = musicVolume;
        fxVolume = FXSlider.value;
        FXAudioSource.volume = fxVolume;
    }


    
    public void OnLoadSceneVolume(Scene scene, LoadSceneMode mode)
    {
        musicSlider.value = musicVolume;
        FXSlider.value = fxVolume;
        SceneManager.sceneLoaded -= OnLoadSceneVolume;

    }
    public void PlayVilageMusic()
    {
        musicAudioSource.clip = VilageMusic;
        musicAudioSource.Play();
    }
    public void PlayGameMusic()
    {
        musicAudioSource.clip = GameMusic;
        musicAudioSource.Play();
    }
    public void PlayerFx(AudioClip clip)
    {
        FXAudioSource.PlayOneShot(clip);
    }
}
