using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    
    private void Start()
    {
        AudioManager.Instance.musicSlider = gameObject.GetComponent<UnityEngine.UI.Slider>();  
    }
}
