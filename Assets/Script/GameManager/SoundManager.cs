using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.PlayerSettings;

public enum SoundType
{
    BowDraw,
    BowShoot,
    Move,
    Dash,
    HeadShot,
    BodyShot,
    MonsterDead,
    AxeHit,
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager ins;
    [SerializeField] private AudioClip[] soundLists;
    [SerializeField] AudioSource audioSource;

    public float min = 200;
    public float max = 500;

    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
    }

    public void PlaySoundOneShot(SoundType sound, float volume)
    {
        audioSource.PlayOneShot(soundLists[(int)sound],GameSettings.ins.soundVolume);
    }
    public void StopSound()
    {
        ins.audioSource.Stop();
    }
    public void PlaySound(AudioSource source, float volume,bool loop)
    {
        source.volume=GameSettings.ins.soundVolume;
        source.Play();
        source.loop = loop;
    }
    public void StopSoundOutside(AudioSource source) {
        source.Stop();
    }
    public void PlaySoundAtLocation(SoundType sound,Vector3 location)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = location;

        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = soundLists[(int)sound];
        aSource.volume = GameSettings.ins.soundVolume;
        aSource.spatialBlend = 1.0f;      // 3D sound
        aSource.minDistance = min;         // inner radius
        aSource.maxDistance = max; // how far it can be heard
        aSource.rolloffMode = AudioRolloffMode.Linear;

        aSource.Play();
        Object.Destroy(tempGO, soundLists[(int)sound].length);
    }

}
