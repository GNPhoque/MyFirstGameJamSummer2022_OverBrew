using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("BattleSounds")] 
    [SerializeField] AudioClip swordslash;

    [Header("UI and scene sounds")]
    [SerializeField] AudioClip buttonclic;
    [SerializeField] AudioClip buttonclicback;
    [SerializeField] AudioClip victory;
    [SerializeField] AudioClip defeat;


    [Header("laboratory sounds")]
    [SerializeField] AudioClip potionfill;
    [SerializeField] AudioClip[] potionbreacks;
    [SerializeField] AudioClip take;
    [SerializeField] AudioClip drop;


    AudioSource audio;
    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    #region BattleSounds
    public void SoundSwordslash()
    {
        audio.PlayOneShot(swordslash);
    }
    #endregion
    #region UIandSceneSounds
    public void SoundButtonClic()
    {
        audio.PlayOneShot(buttonclic);
    }
    public void SoundButtonClicBack()
    {
        audio.PlayOneShot(buttonclicback);
    }
    public void SoundVictory()
    {
        audio.PlayOneShot(victory);
    }
    public void SoundDefeat()
    {
        audio.PlayOneShot(defeat);
    }
    #endregion
    #region LaboratorySounds
    public void SoundPotionFill()
    {
        audio.PlayOneShot(potionfill);
    }
    public void SoudPotionBreack()
    {
        audio.PlayOneShot(potionbreacks[Random.Range(0, potionbreacks.Length)]);
    }
    public void SoundTake()
    {
        audio.PlayOneShot(take);
    }
    public void SoundDrop()
    {
        audio.PlayOneShot(drop);
    }
    #endregion

}
