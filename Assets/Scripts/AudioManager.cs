using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    public void PlaySound(string name)
    {
        foreach (var sound in Sounds)
        {
            if (name == sound.Name)
            {
                sound.Source.Play();
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        foreach(var sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.clip;
            sound.Source.loop = sound.Loop;
        }

        PlaySound("GameBackground");
    }
}
