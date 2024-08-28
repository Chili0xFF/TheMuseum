using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "ScriptableObjects/SoundSO")]
public class SoundSO : ScriptableObject
{
    [TextArea] public string Comment = "Information Here.";
    [SerializeField] private List<Sound> m_sound;

    private Dictionary<string, Sound> _dict;

    public void OnEnable()
    {
        _dict = new Dictionary<string, Sound>(m_sound.Count);
        for (int i = 0; i < m_sound.Count; ++i)
        {
            var sfx = m_sound[i];
            if (!_dict.ContainsKey(sfx.nameOfSound))
                _dict.Add(sfx.nameOfSound, sfx);
        }
    }

    public AudioClip GetSoundByName(string nameOfSound)
    {
        if (_dict.TryGetValue(nameOfSound, out Sound sound))
            return sound.clip;

        Debug.LogError("Sound " + nameOfSound + " was not found! Returning default");
        return m_sound[0].clip;
    }

    public AudioClip[] GetAllSoundsByName(string nameOfSound)
    {
        List<AudioClip> clips = new List<AudioClip>();
        foreach (Sound sound in m_sound)
        {
            if (sound.nameOfSound.Equals(nameOfSound)) clips.Add(sound.clip);
        }
        return clips.ToArray();
    }

    [System.Serializable]
    private class Sound
    {
        public string nameOfSound;
        public AudioClip clip;
        public bool DoIHaveTheRights;
    }
}

