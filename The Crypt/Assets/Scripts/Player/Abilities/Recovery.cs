using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recovery : Abilities
{
    
    private PlayerController playerController;
    private Image imageCooldown;

    public Recovery()
    {
        this.abilityName = "Recovery";
        this.abilityDescription = "Regenerate 1 hp, but lose movement speed for a few seconds";
        this.abilityCooldown = 10f;
    }

    void Start()
    {
        playerController = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
        imageCooldown = GameObject.Find("ECooldown").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.onCooldown)
        {
            imageCooldown.fillAmount = 0;
            if (Input.GetKeyDown(KeyCode.E))
            {
                recoverHealth();
                onCooldown = true;
                StartCoroutine(PutOnCooldown(abilityCooldown));
            }
        } else {
            CooldownTimer(imageCooldown, abilityCooldown);
        }
    }

    private void recoverHealth()
    {
        playerController.ChangeHealth(1);
        playerController.Speed -= 1f;
        StartCoroutine(DecreaseSpeed(playerController));
    }

    private IEnumerator DecreaseSpeed(PlayerController playerController) 
    {
        yield return new WaitForSeconds(6);
        playerController.Speed = 5f;
    }
}
