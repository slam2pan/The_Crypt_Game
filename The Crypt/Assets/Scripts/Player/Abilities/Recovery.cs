using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recovery : Abilities
{
    
    private PlayerController playerController;

    public Recovery()
    {
        this.abilityName = "Recovery";
        this.abilityDescription = "Regenerate 1 hp, but lose movement speed for a few seconds";
        this.abilityCooldown = 10f;
        this.abilityDuration = 6f;
    }

    void Start()
    {
        playerController = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
        imageCooldown = GameObject.Find(keyCode.ToString() + "Cooldown").GetComponent<Image>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.onCooldown)
        {
            imageCooldown.fillAmount = 0;
            if (Input.GetKeyDown(keyCode))
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
        audioManager.Play("Recovery");
    }

    private IEnumerator DecreaseSpeed(PlayerController playerController) 
    {
        yield return new WaitForSeconds(abilityDuration);
        playerController.Speed = 5f;
    }
}
