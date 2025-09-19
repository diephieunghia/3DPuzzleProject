using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    BowDraw,
    BowShoot,
    Move,
    Jump,


}
public class SoundManager : MonoBehaviour
{
    public static SoundManager ins;
    [SerializeField] private AudioClip[] soundLists;
    [SerializeField] AudioSource audioSource;

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
    public static void PlaySound(SoundType sound, float volume)
    {

    }

}
