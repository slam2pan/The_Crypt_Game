using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultShot : MonoBehaviour
{
    private float shotVelocity = 10f;
    public GameObject defaultShotPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
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
        Debug.Log(direction);

        GameObject defaultShot = Instantiate(defaultShotPrefab, (Vector2)transform.position + (direction * 0.3f), Quaternion.identity);
        defaultShot.GetComponent<Rigidbody2D>().velocity = direction * shotVelocity;
        StartCoroutine(DestroyShot(defaultShot));
    }

    IEnumerator DestroyShot(GameObject shotObject)
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(shotObject);
    }
}
