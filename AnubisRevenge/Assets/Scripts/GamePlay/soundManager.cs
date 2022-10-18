using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    [Header("----- Player Sounds -----")]
    [SerializeField] public AudioSource jump;
    [SerializeField] public AudioSource hurt;
    [SerializeField] public AudioSource walk;
    [SerializeField] public AudioSource run;
    [SerializeField] public AudioSource crouch;
    [SerializeField] public AudioSource fall;
    [SerializeField] public AudioSource dead;
    [Header("----- Music Sounds -----")]
    [SerializeField] public AudioSource levelMusic;
    [SerializeField] public AudioSource hubMusic;
    [SerializeField] public AudioSource bossMusic;
    [Header("----- Weapons Sounds -----")]
    [SerializeField] public AudioSource meleeSwing;
    [SerializeField] public AudioSource dynamiteThrow;
    [SerializeField] public AudioSource meleeHit;
    [SerializeField] public AudioSource bulletFlying;
    [SerializeField] public AudioSource dynamiteInMotion;
    [SerializeField] public AudioSource dynamite;
    [SerializeField] public AudioSource dynamiteHit;
    [SerializeField] public AudioSource bulletHit;
    [SerializeField] public AudioSource shoot;

}
