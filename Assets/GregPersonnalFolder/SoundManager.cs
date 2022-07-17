using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("BattleSounds")] 
    [SerializeField] AudioClip swordslash;
    [SerializeField] AudioClip heal;


    [Header("UI and scene sounds")]
    [SerializeField] AudioClip buttonclic;
    [SerializeField] AudioClip buttonclicback;
    [SerializeField] AudioClip victory;
    [SerializeField] AudioClip defeat;


    [Header("laboratory sounds")]
    [SerializeField] AudioClip potionfill;
    [SerializeField] AudioClip waterplouf;
    [SerializeField] AudioClip cauldroncrafting;
    [SerializeField] AudioClip[] potionbreacks;
    [SerializeField] AudioClip[] crunchingIngredients;
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
    public void SoundHeal()
    {
        audio.PlayOneShot(heal);
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
    public void SoundWaterPlouf(CraftingBoxType craftingBoxType)
    {
        if(craftingBoxType == CraftingBoxType.CAULDRON || craftingBoxType == CraftingBoxType.ALEMBIC) { audio.PlayOneShot(waterplouf); }
        if (craftingBoxType == CraftingBoxType.MORTAR) { SoudCrunchingIngredients(); }

    }
    public void SoundRecipeCrafting(CraftingBoxType craftingBoxType)
    {
        if (craftingBoxType == CraftingBoxType.CAULDRON || craftingBoxType == CraftingBoxType.ALEMBIC) { audio.PlayOneShot(cauldroncrafting); }
        if (craftingBoxType == CraftingBoxType.MORTAR) {  }

    }

    public void SoundRecipeCrafted(CraftingBoxType craftingBoxType)
    {
        if (craftingBoxType == CraftingBoxType.CAULDRON || craftingBoxType == CraftingBoxType.ALEMBIC) { audio.PlayOneShot(potionfill); }
        if (craftingBoxType == CraftingBoxType.MORTAR){ SoudCrunchingIngredients(); }

    }
    public void SoudPotionBreack()
    {
        audio.PlayOneShot(potionbreacks[Random.Range(0, potionbreacks.Length)]);
    }
    public void SoudCrunchingIngredients()
    {
        audio.PlayOneShot(crunchingIngredients[Random.Range(0, crunchingIngredients.Length)]);
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
