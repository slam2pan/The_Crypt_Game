using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameForm : Abilities
{
    private PlayerController playerController;
    private GameObject firePS;
    private GameObject fireParticles;
    private bool powerActive = false;

    public FlameForm()
    {
        this.abilityName = "FlameForm";
        this.abilityDescription = "Turn invulnurable and destroy everything you touch";
        this.abilityCooldown = 60;
        this.abilityDuration = 10f;
    }

    void Start()
    {
        playerController = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
        imageCooldown = GameObject.Find(keyCode.ToString() + "Cooldown").GetComponent<Image>();
        firePS = GameAssets.i.firePS;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.onCooldown)
        {
            imageCooldown.fillAmount = 0;
            if (Input.GetKeyDown(keyCode))
            {
                StartCoroutine(StartDestruction());
                onCooldown = true;
                StartCoroutine(PutOnCooldown(abilityCooldown));
            }
        } else {
            CooldownTimer(imageCooldown, abilityCooldown);
        }

        if (fireParticles != null)
        {
            fireParticles.transform.position = transform.position;
        }
    }

    private IEnumerator StartDestruction()
    {
        playerController.Invulnurable = true;
        powerActive = true;
        fireParticles = Instantiate(firePS, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(abilityDuration);
        Destroy(fireParticles);
        playerController.Invulnurable = false;
        powerActive = false;
    }

    // Make collisions with player destroy the collision object
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Enemy") && powerActive)
        {
            Destroy(collisionInfo.gameObject);
        }
    }
}
