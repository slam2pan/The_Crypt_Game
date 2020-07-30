using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDropIcon : MonoBehaviour
{
    private GameObject abilityMenuUI;
    private AbilityMenu abilityMenu;
    private GameObject player;

    void Start()
    {
        abilityMenuUI = GameObject.Find("AbilityMenuCanvas").transform.Find("AbilityMenu").gameObject;
        abilityMenu = GameObject.Find("AbilityMenuCanvas").transform.Find("AbilityMenu").GetComponent<AbilityMenu>();
        player = GameObject.Find("Player(Clone)");
    }

    // When player picks up ability, load ability menu so player can pick what to replace
    void OnTriggerEnter2D(Collider2D other)
    {
        SpriteRenderer abilityName = transform.Find("DropAbilityIcon").GetComponent<SpriteRenderer>();
        
        // Do not allow player to have repeat abilities
        if (other.CompareTag("Player") && player.GetComponent(abilityName.sprite.name.ToString()) == null)
        {
            abilityMenuUI.SetActive(true);
            Time.timeScale = 0f;
            abilityMenu.SetAbility(abilityName);
            Destroy(gameObject);
        }
    }
}
