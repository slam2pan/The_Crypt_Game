using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AbilityMenu : MonoBehaviour
{
    private SpriteRenderer newAbility;
    private string newAbilityName;
    private GameObject player;
    private Image iconReference;

    void Awake()
    {
        iconReference = GameObject.Find("IconReference").GetComponent<Image>();
    }

    void Start()
    {
        player = GameObject.Find("Player(Clone)");
    }

    public void SetAbility(SpriteRenderer ability)
    {
        newAbility = ability;
        newAbilityName = newAbility.sprite.name;
        iconReference.sprite = Resources.Load<Sprite>("Sprites/Abilities/" + ability.sprite.name);
    } 

    // On click of ability keybind letter, allow player to change which ability they have
    public void ChangeAbility(string keyBind)
    {
        Image abilityIcon = GameObject.Find(keyBind + "Icon").GetComponent<Image>();
        
        // Delete old ability
        if (abilityIcon.sprite != null)
        {
            Sprite oldAbility = abilityIcon.sprite;
            Destroy(player.GetComponent(oldAbility.name));
        }

        // Add new player script, change keybind
        player.AddComponent(Type.GetType(newAbilityName));
        abilityIcon.sprite = Resources.Load<Sprite>("Sprites/Abilities/" + newAbilityName);
        Abilities abilityToChange = (Abilities) player.GetComponent(newAbilityName);
        abilityToChange.ChangeKeybind(keyBind);

        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void CancelAbilityChange()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
