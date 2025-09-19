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

    public float sound = 1;
    public float bright = 0.5f;
    public float sen = 0.1f;

    public PostProcessProfile profile;

    AutoExposure exposure;
    //value for brightness only if slider to small
    public float value;
    // Start is called before the first frame update
    private void Awake()
    {
        


    }
    void Start()
    {
        soundBrightSen = GameObject.FindWithTag("Settings");
        if (soundBrightSen != null)
        {
            component = soundBrightSen.GetComponent<SoundBrightSen>();
            SoundSlider.value = component.sound;
            BrightSlider.value = component.brightSaved;
            SenSlider.value = component.sen;
        }
        else
        {

        }
        if (soundBrightSen!=null)
        {
            SoundSlider.onValueChanged.AddListener(delegate { OnSoundChange(); });
            BrightSlider.onValueChanged.AddListener(delegate { OnBrightChange(); });
            SenSlider.onValueChanged.AddListener(delegate { OnMouseSenChange(); });
            profile.TryGetSettings(out exposure);
        }
        else
        {
            profile.TryGetSettings(out exposure);
            SoundSlider.onValueChanged.AddListener(delegate { SoundChange2(); });
            BrightSlider.onValueChanged.AddListener(delegate { BrightChange2(); });
            SenSlider.onValueChanged.AddListener(delegate { MouseSenChange2(); });
        }
        
    }

    void OnSoundChange()
    {
        component.sound = SoundSlider.value;
        sound = SoundSlider.value;
        if (GameSettings.ins)
        {
            GameSettings.ins.soundVolume = sound;
        }
    }
    void OnBrightChange()
    {
        component.brightSaved = BrightSlider.value;
        bright = BrightSlider.value;
        if (GameSettings.ins)
        {
            GameSettings.ins.brightness = bright;
        }
        if (BrightSlider.value < 0.04)
            exposure.keyValue.value =value * component.bright;
        else
            exposure.keyValue.value = BrightSlider.value * component.bright;
    }
    void OnMouseSenChange()
    {
        component.sen = SenSlider.value;
        sen= SenSlider.value;
        if(GameSettings.ins)
        {
            GameSettings.ins.mouseSensivity = sen;
        }
        if (SenSlider.value < 0.001)
            component.sen = 0.01f;
        if(GameSettings.ins)
            GameSettings.ins.mouseSensivity = component.sen * GameSettings.ins.mouseSenMultiply;
    }
    void SoundChange2()
    {
        sound= SoundSlider.value;
        if (GameSettings.ins)
        {
            GameSettings.ins.soundVolume= sound;
        }
    }
    void BrightChange2()
    {
        bright= BrightSlider.value;
        if (GameSettings.ins)
        {
            GameSettings.ins.brightness = bright;
        }
    }
    void MouseSenChange2()
    {
        sen= SenSlider.value;
        if (GameSettings.ins)
            GameSettings.ins.mouseSensivity = sen * GameSettings.ins.mouseSenMultiply;
    }
}
