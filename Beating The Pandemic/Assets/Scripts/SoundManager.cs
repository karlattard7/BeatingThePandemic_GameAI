using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip equipWeapon, heal, attackMelee, attackRanged, virus, door, scream, pick, damage, doorLock;
    private static AudioSource AudioSrc;

    // Start is called before the first frame update
    void Start()
    {
        equipWeapon = Resources.Load<AudioClip>("equip_weapon");
        heal = Resources.Load<AudioClip>("potion");
        attackMelee = Resources.Load<AudioClip>("sword");
        attackRanged = Resources.Load<AudioClip>("arrow");
        virus = Resources.Load<AudioClip>("virus");
        door = Resources.Load<AudioClip>("door");
        scream = Resources.Load<AudioClip>("scream");
        pick = Resources.Load<AudioClip>("pick");
        damage = Resources.Load<AudioClip>("hit");
        doorLock = Resources.Load<AudioClip>("lock");


        AudioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        if (AudioSrc)
            switch (clip)
            {
                case "equip":
                    AudioSrc.PlayOneShot(equipWeapon);
                    break;
                case "potion":
                    AudioSrc.PlayOneShot(heal);
                    break;
                case "sword":
                    AudioSrc.PlayOneShot(attackMelee);
                    break;
                case "arrow":
                    AudioSrc.PlayOneShot(attackRanged);
                    break;
                case "virus":
                    AudioSrc.PlayOneShot(virus);
                    break;
                case "door":
                    AudioSrc.PlayOneShot(door);
                    break;
                case "scream":
                    AudioSrc.PlayOneShot(scream);
                    break;
                case "pick":
                    AudioSrc.PlayOneShot(pick);
                    break;
                case "damage":
                    AudioSrc.PlayOneShot(damage);
                    break;
                case "lock":
                    AudioSrc.PlayOneShot(doorLock);
                    break;
            }
        else
            Debug.LogError("Audio source was not found!!!");
    }
}
