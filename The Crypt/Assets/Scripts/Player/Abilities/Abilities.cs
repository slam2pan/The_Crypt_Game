using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Abilities : MonoBehaviour
{
    [SerializeField] protected string abilityName = "New Ability Name";
    [SerializeField] protected string abilityDescription = "New Ability Description";
    [SerializeField] protected float abilityCooldown = 5f;
    protected bool onCooldown = false;

    virtual protected IEnumerator PutOnCooldown(float abilityCooldown) {
        yield return new WaitForSeconds(abilityCooldown);
        onCooldown = false;
    }

    virtual protected void CooldownTimer(Image imageCooldown, float abilityCooldown)
    {
        imageCooldown.fillAmount += 1 / abilityCooldown * Time.deltaTime;
    }
}

