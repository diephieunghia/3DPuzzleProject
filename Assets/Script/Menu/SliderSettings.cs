using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderSettings : MonoBehaviour
{
    public GameObject soundBrightSen;
    SoundBrightSen component;

    public Slider SoundSlider;
    public Slider BrightSlider;
    public Slider SenSlider;

    // Start is called before the first frame update
    void Start()
    {
        component=soundBrightSen.GetComponent<SoundBrightSen>();
        SoundSlider.onValueChanged.AddListener(delegate { OnSoundChange(); });
        BrightSlider.onValueChanged.AddListener(delegate { OnBrightChange(); });
        SenSlider.onValueChanged.AddListener(delegate { OnMouseSenChange(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnSoundChange()
    {
        component.sound = SoundSlider.value;
        Debug.Log("Sound: " + component.sound);
    }
    void OnBrightChange()
    {
        component.bright = BrightSlider.value;
        Debug.Log("Sound: " + component.bright);
    }
    void OnMouseSenChange()
    {
        component.sen = SenSlider.value;
        Debug.Log("Sound: " + component.sen);
    }
}
