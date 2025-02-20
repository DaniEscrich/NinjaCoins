using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip Coin, Jump;

    public static AudioClip JumpSound, CoinSound;

    static AudioSource audioSrc;


    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        JumpSound = Jump;
        CoinSound = Coin;
    }

    // Update is called once per frame
    public static void PlaySound(string soundClip)
    {
        switch(soundClip){
            case "Coin":
                audioSrc.PlayOneShot(CoinSound);
                break;
            case "Jump":
                audioSrc.PlayOneShot(JumpSound);
                break;
        }
    }
}
