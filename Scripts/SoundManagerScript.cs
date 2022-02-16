using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip pickupSound, jumpSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        pickupSound = Resources.Load<AudioClip>("Coin01");
        jumpSound = Resources.Load<AudioClip>("Jump01");
        //Can add more sounds here by copy pasting above and adding more to variables on line 7
        audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound (string clip)
    {
        switch (clip){
            case "pickup":
                audioSrc.PlayOneShot(pickupSound);
                break;
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
                //Add more cases for more sound fx
        }
    }
}
