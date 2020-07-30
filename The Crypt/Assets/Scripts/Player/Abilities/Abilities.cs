using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Abilities : MonoBehaviour
{
    [SerializeField] protected string abilityName = "New Ability Name";
    [SerializeField] protected string abilityDescription = "New Ability Description";
    [SerializeField] protected float abilityCooldown = 5f;
    [SerializeField] protected float abilityDuration = 0f;
    protected bool onCooldown = false;
    protected AudioManager audioManager;
    protected Image imageCooldown;
    [SerializeField] protected KeyCode keyCode;

    virtual protected IEnumerator PutOnCooldown(float abilityCooldown) {
        yield return new WaitForSeconds(abilityCooldown);
        onCooldown = false;
    }

    virtual protected void CooldownTimer(Image imageCooldown, float abilityCooldown)
    {
        imageCooldown.fillAmount += 1 / abilityCooldown * Time.deltaTime;
    }

    virtual public void ChangeKeybind(string newKey)
    {
        keyCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), newKey);
    }
}

