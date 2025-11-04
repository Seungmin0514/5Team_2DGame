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
    public Slider musicSlider;
    public Slider FXSlider;
    private float Volume;
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
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        Volume = musicSlider.value;
        musicAudioSource.volume = Volume;
    }


    public void OnLoadSceneVolume(Scene scene, LoadSceneMode mode)
    {


        musicSlider.value = Volume;
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
}
