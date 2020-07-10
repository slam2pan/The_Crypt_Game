using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abilities : MonoBehaviour
{
    [SerializeField] protected string abilityName = "New Ability Name";
    [SerializeField] protected string abilityDescription = "New Ability Description";
    [SerializeField] protected float abilityCooldown = 5f;
    protected bool onCooldown = false;

    virtual protected IEnumerator putOnCooldown(float abilityCooldown) {
        yield return new WaitForSeconds(abilityCooldown);
        onCooldown = false;
    }
}

