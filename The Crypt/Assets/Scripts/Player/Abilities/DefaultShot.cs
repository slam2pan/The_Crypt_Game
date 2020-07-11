using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultShot : Abilities
{
    private float shotVelocity = 10f;
    public GameObject defaultShotPrefab;

    public DefaultShot()
    {
        this.abilityName = "DefaultShot";
        this.abilityDescription = "Shoot in the direction of the cursor";
        this.abilityCooldown = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.onCooldown)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                onCooldown = true;
                StartCoroutine(putOnCooldown(abilityCooldown));
            }
        }
    }

    // Shoot circles in the direction of the mouse
    private void Shoot()
    {
        Vector3 shootDirection = Input.mousePosition;
        shootDirection.z = Camera.main.nearClipPlane;
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(shootDirection);
        Vector2 direction = worldMousePosition - (Vector2)transform.position;
        direction.Normalize();

        GameObject defaultShot = Instantiate(defaultShotPrefab, (Vector2)transform.position + (direction * 0.3f), Quaternion.identity);
        defaultShot.GetComponent<Rigidbody2D>().velocity = direction * shotVelocity;

        StartCoroutine(DestroyShot(defaultShot));
    }

    IEnumerator DestroyShot(GameObject shotObject)
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(shotObject);
    }
}
