using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScriptOther : MonoBehaviour
{
    public static AudioClip clicSound, mouseclicSound, collectingSound;
    static AudioSource audioSrc;

    void Start()
    {
        clicSound = Resources.Load<AudioClip>("clic");
        mouseclicSound = Resources.Load<AudioClip>("mouseclic");
        collectingSound = Resources.Load<AudioClip>("collecting");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "clic":
                audioSrc.PlayOneShot(clicSound);
                break;
            case "mouseclic":
                audioSrc.PlayOneShot(mouseclicSound);
                break;
            case "collecting":
                audioSrc.PlayOneShot(collectingSound);
                break;
        }
    }
}
