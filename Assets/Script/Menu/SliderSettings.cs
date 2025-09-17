using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
public class SliderSettings : MonoBehaviour
{
    public GameObject soundBrightSen;
    SoundBrightSen component;

    public Slider SoundSlider;
    public Slider BrightSlider;
    public Slider SenSlider;

    public PostProcessProfile profile;

    AutoExposure exposure;
    public float value;
    // Start is called before the first frame update
    void Start()
    {
        component=soundBrightSen.GetComponent<SoundBrightSen>();
        SoundSlider.onValueChanged.AddListener(delegate { OnSoundChange(); });
        BrightSlider.onValueChanged.AddListener(delegate { OnBrightChange(); });
        SenSlider.onValueChanged.AddListener(delegate { OnMouseSenChange(); });
        profile.TryGetSettings(out exposure);
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
        if (BrightSlider.value < 0.04)
            exposure.keyValue.value =value * component.bright;
        else
            exposure.keyValue.value = BrightSlider.value * component.bright;
    }
    void OnMouseSenChange()
    {
        component.sen = SenSlider.value;
        Debug.Log("Sound: " + component.sen);
    }
}
