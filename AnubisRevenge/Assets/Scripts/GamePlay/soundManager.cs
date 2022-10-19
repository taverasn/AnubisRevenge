using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    [SerializeField] public AudioSource aud;
    [Header("----- Player Sounds -----")]
    [SerializeField] public AudioClip jump;
    [SerializeField] public AudioClip hurt;
    [SerializeField] public AudioClip walk;
    [SerializeField] public AudioClip fall;
    [SerializeField] public AudioClip dead;
    [Header("----- Music Sounds -----")]
    [SerializeField] public AudioClip levelMusic;
    [SerializeField] public AudioClip hubMusic;
    [SerializeField] public AudioClip bossMusic;
    [Header("----- Weapons SoundsClip")]
    [SerializeField] public AudioClip meleeSwing;
    [SerializeField] public AudioClip dynamiteThrow;
    [SerializeField] public AudioClip meleeHit;
    [SerializeField] public AudioClip dynamite;
    [SerializeField] public AudioClip dynamiteHit;
    [SerializeField] public AudioClip bulletHit;
    [SerializeField] public AudioClip shoot;

}
