using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ability : ScriptableObject
{
    [Space]
    [Header("Universal Ability Values")]

    public float damage;

    public float coolDown;

    public AudioClip sound;

    public enum Anim
    {

    }

    [HideInInspector]
    public bool m_canUseAbility = true;


    /// <summary>
    /// The equivalent to void Start(), but must still be called manually
    /// </summary>
    /// <param name="obj">The GameObject which is calling this function</param>
    public virtual void Initialize(GameObject obj = null)
    {

    }

    /// <summary>
    /// This function will apply the skill/effect to the target character (used for damage and/or debuffs) & start cooldowns
    /// </summary>
    /// <param name="hand">The hand/controller that is casting/using this ability</param>
    public virtual void UseAbility(PlayerController player, float coolDownStartDelay = 0.0f)
    {
        if (m_canUseAbility == true)
        {
            m_canUseAbility = false;
            player.StartCoroutine(StartCooldown(coolDownStartDelay));
        }
    }
    /// <summary>
    /// This function stop using the current ability
    /// </summary>
    public virtual void StopAbility()
    {

    }

    public IEnumerator StartCooldown(float coolDownStartDelay)
    {
        m_canUseAbility = false;
        yield return new WaitForSeconds(coolDownStartDelay);
        yield return new WaitForSeconds(coolDown);
        m_canUseAbility = true;
    }

    public virtual string GetName()
    {
        return name;
    }
}
