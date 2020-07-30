using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerController : MonoBehaviour
{
    private bool invulnurable = false;
    public bool Invulnurable
    {
        get { return invulnurable; }
        set { invulnurable = value; }
    }
    private float timeInvulnurable = 1f;

    private float speed = 5f;

    public float Speed {
        get {return speed; }
        set {speed = value; }
    }

    private float maxHealth = 10;
    private float playerHealth;
    public float Health
    {
        get { return playerHealth; }
        private set { playerHealth = value; }
    }
    private Rigidbody2D playerRb;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = maxHealth;
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 tempVect = new Vector2(horizontalInput, verticalInput);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        playerRb.MovePosition(playerRb.position + tempVect);

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !invulnurable)
        {
            audioManager.Play("Hurt");
            CameraShaker.Instance.ShakeOnce(3f, 4f, .1f, 1f);
            ChangeHealth(-1);
            StartCoroutine(invulnurableTime());
        }
    }

    public void ChangeHealth(int amount)
    {
        playerHealth = Mathf.Clamp(playerHealth + amount, 0, maxHealth);
        HealthBar.instance.SetValue(playerHealth / (float)maxHealth);
    }

    // Turn the direction of the character around for ability purposes
    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    // Timer for taking damage
    private IEnumerator invulnurableTime()
    {
        invulnurable = true;
        yield return new WaitForSeconds(timeInvulnurable);
        invulnurable = false;
    }
}
