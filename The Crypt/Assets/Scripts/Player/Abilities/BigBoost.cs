using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigBoost : Abilities
{

    private PlayerController playerController;
    private Image imageCooldown;
    private AudioManager audioManager;

    public BigBoost()
    {
        this.abilityName = "BigBoost";
        this.abilityDescription = "Give increased movement speed for a period of time";
        this.abilityCooldown = 20f;
    }


    void Start()
    {
        imageCooldown = GameObject.Find("RCooldown").GetComponent<Image>();
        playerController = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.onCooldown)
        {
            imageCooldown.fillAmount = 0;
            if (Input.GetKeyDown(KeyCode.R))
            {
                ModifySpeed();
                onCooldown = true;
                StartCoroutine(PutOnCooldown(abilityCooldown));
            }
        } else {
            CooldownTimer(imageCooldown, abilityCooldown);
        }
    }

    void ModifySpeed()
    {
        playerController.Speed *= 2;
        audioManager.Play("BigBoost");
        StartCoroutine(speedCountdown());
    }

    private IEnumerator speedCountdown()
    {
        yield return new WaitForSeconds(5);
        playerController.Speed = 5;
    }
}
