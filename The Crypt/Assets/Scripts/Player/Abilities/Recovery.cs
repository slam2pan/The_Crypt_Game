using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : Abilities
{
    
    private PlayerController playerController;
    public Recovery()
    {
        this.abilityName = "Recovery";
        this.abilityDescription = "Regenerate 1 hp, but lose movement speed for a few seconds";
        this.abilityCooldown = 10f;
    }

    void Start()
    {
        playerController = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.onCooldown)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                recoverHealth();
                onCooldown = true;
                StartCoroutine(putOnCooldown(abilityCooldown));
            }
        }
    }

    private void recoverHealth()
    {
        playerController.ChangeHealth(1);
        playerController.Speed -= 2f;
        StartCoroutine(DecreaseSpeed(playerController));
    }

    private IEnumerator DecreaseSpeed(PlayerController playerController) 
    {
        yield return new WaitForSeconds(6);
        playerController.Speed = 5f;
    }
}
