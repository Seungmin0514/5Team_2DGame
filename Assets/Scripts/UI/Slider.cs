using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider musicSlider;
    [SerializeField] private UnityEngine.UI.Slider fxSlider;
    // Start is called before the first frame update
    void Awake()
    {
        AudioManager.Instance.musicSlider = musicSlider;
        AudioManager.Instance.FXSlider = fxSlider;
        musicSlider.value = AudioManager.Instance.musicVolume;
        fxSlider.value = AudioManager.Instance.fxVolume;
    }

    
}
