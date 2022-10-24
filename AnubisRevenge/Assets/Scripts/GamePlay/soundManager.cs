using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] public AudioSource aud;
    [Header("----- Player Sounds -----")]
    [SerializeField] public AudioClip jump;
    [Range(0, 1)] [SerializeField] public float jumpVol;
    [SerializeField] public AudioClip hurt;
    [Range(0, 1)] [SerializeField] public float hurtVol;
    [SerializeField] public AudioClip walk;
    [Range(0, 1)] [SerializeField] public float walkVol;
    [SerializeField] public AudioClip fall;
    [Range(0, 1)] [SerializeField] public float fallVol;
    [SerializeField] public AudioClip dead;
    [Range(0, 1)] [SerializeField] public float deadVol;

    [Header("----- Music Sounds -----")]
    [SerializeField] public AudioClip levelMusic;
    [Range(0, 1)] [SerializeField] public float levelMusicVol;
    [SerializeField] public AudioClip hubMusic;
    [Range(0, 1)] [SerializeField] public float hubVol;
    [SerializeField] public AudioClip bossMusic;
    [Range(0, 1)] [SerializeField] public float bossMusicVol;

    [Header("----- Weapons Soundss -----")]
    [SerializeField] public AudioClip meleeSwing;
    [Range(0, 1)] [SerializeField] public float meleeSwingVol;
    [SerializeField] public AudioClip dynamiteThrow;
    [Range(0, 1)] [SerializeField] public float dynamiteThrowVol;
    [SerializeField] public AudioClip meleeHit;
    [Range(0, 1)] [SerializeField] public float meleeHitVol;
    [SerializeField] public AudioClip dynamite;
    [Range(0, 1)] [SerializeField] public float dynamiteVol;
    [SerializeField] public AudioClip dynamiteHit;
    [Range(0, 1)] [SerializeField] public float dynamiteHitVol;
    [SerializeField] public AudioClip bulletHit;
    [Range(0, 1)] [SerializeField] public float bulletHitVol;
    [SerializeField] public AudioClip shoot;
    [Range(0, 1)] [SerializeField] public float shootVol;
}
