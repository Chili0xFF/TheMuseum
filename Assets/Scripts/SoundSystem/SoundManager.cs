using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {  get; private set; }

    [Header("Mixers")]
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private AudioMixerGroup musicMixer;
    [SerializeField] private AudioMixerGroup sfxMixer;
    [SerializeField] private AudioMixerGroup narrationMixer;

    [Header("Data")]
    [SerializeField] private SoundSO music;
    [SerializeField] private SoundSO sfx;

    [Header("Internal")]
    [SerializeField] private AudioSource m_AudioPrefab;

    [Header("Testing")]
    [SerializeField] public bool testMode = false;

    void Start()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;


        Debug.Log("To be rewrote cleaner!");
        if (testMode) {
            Debug.Log("Test mode turned on");
            SetMusicByName("mainMenu",true);
            PlaySoundByName("click");
        }
    }

    public AudioSource PlaySoundByName(string name, float volume = 1f, float pitch = 1f) {
        AudioSource audioSource = Instantiate(m_AudioPrefab, transform);
        audioSource.clip = sfx.GetSoundByName(name); ;
        audioSource.gameObject.name = audioSource.clip.name;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.outputAudioMixerGroup = sfxMixer;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
        return audioSource;
    }

    public AudioSource SetMusicByName(string name, bool loopable = false, float volume = 1f, float pitch = 1f) {
        AudioSource audioSource = Instantiate(m_AudioPrefab, transform);
        audioSource.clip = music.GetSoundByName(name); ;
        audioSource.gameObject.name = audioSource.clip.name;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.outputAudioMixerGroup = musicMixer;
        audioSource.loop = loopable;
        audioSource.Play();
        if(!loopable) Destroy(audioSource.gameObject, audioSource.clip.length);
        return audioSource;
    }
}
