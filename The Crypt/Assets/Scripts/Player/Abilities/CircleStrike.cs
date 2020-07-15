using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleStrike : Abilities
{
    public GameObject defaultShotPrefab;
    private Image imageCooldown;
    private int numProjectiles = 8;
    private float shotVelocity = 10f;
    private AudioManager audioManager;

    public CircleStrike()
    {
        this.abilityName = "CircleStrike";
        this.abilityDescription = "Shoot projectiles in every direction";
        this.abilityCooldown = 5f;
    }

    void Start()
    {
        imageCooldown = GameObject.Find("QCooldown").GetComponent<Image>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.onCooldown)
        {
            imageCooldown.fillAmount = 0;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Shoot();
                onCooldown = true;
                StartCoroutine(PutOnCooldown(this.abilityCooldown));
            }
        } else {
            CooldownTimer(imageCooldown, abilityCooldown);
        }
    }

    // Fire shots in every 45 degree angle
    void Shoot()
    {
        float angleStep = 360f / numProjectiles;
        float angle = 0f;
        List<GameObject> allPellets = new List<GameObject>();

        for (int i = 0; i < numProjectiles; i++)
        {
            Vector2 shotDirection;
            // Must convert degrees to radians
            shotDirection.x = Mathf.Cos((angle * Mathf.PI) / 180);
            shotDirection.y = Mathf.Sin((angle * Mathf.PI) / 180);

            GameObject onePellet = Instantiate(defaultShotPrefab, (Vector2)transform.position + (shotDirection * 0.3f), Quaternion.identity);
            onePellet.GetComponent<Rigidbody2D>().velocity = shotDirection * shotVelocity;
            allPellets.Add(onePellet);

            angle += angleStep;
        }

        audioManager.Play("CircleStrike");

        StartCoroutine(destroyShot(allPellets));
    }

    // Destroy all pellets after 0.25 seconds
    private IEnumerator destroyShot(List<GameObject> allPellets)
    {
        yield return new WaitForSeconds(0.25f);
        foreach (GameObject pellet in allPellets)
        {
            Destroy(pellet);
        }
    }
}
