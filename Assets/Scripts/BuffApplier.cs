using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffApplier : MonoBehaviour
{

    public PlayerController player;
    public GunSystem gunSystem;
    public float moveSpeedMultiplier = 1.1f;
    public float bulletSpeedMultiplier = 1.1f;
    public float damageMultiplier = 1.1f;
    public float attackSpeedMultiplier = 1.1f;
    public float criticalChanceIncrease = 0.1f;
    public float criticalDamageMultiplier = 0.1f;
    public float radiusMultiplier = 1.1f;
    public float healRegenerationAmount = 0.1f;


    void Start(){
        player = GetComponent<PlayerController>();
        gunSystem = GetComponent<GunSystem>();
        SoundManager soundManager = GameObject.Find("GameManager").GetComponent<SoundManager>();
    }
    public void IncreaseMoveSpeed(){
        player.movementSpeed *= moveSpeedMultiplier;
        player.ChangeBuffText("MOVE SPEED INCREASED!");

    }
    public void IncreaseBulletSpeed()
    {
        gunSystem.bulletSpeed *= bulletSpeedMultiplier;
        player.ChangeBuffText("Bullet speed INCREASED!");
    }

    public void IncreaseDamage()
    {
        gunSystem.damage *= damageMultiplier;
        player.ChangeBuffText("Damage INCREASED!");
    }

    public void IncreaseAttackSpeed()
    {
        gunSystem.attackSpeed *= attackSpeedMultiplier;
        player.ChangeBuffText("Attack speed INCREASED!");
    }

    public void BubbleShield()
    {
        player.ActivateBubbleShield();
        player.ChangeBuffText("Bubble shield activated!");

    }

    public void IncreaseCriticalChance()
    {
        Debug.Log("Current chance: " + gunSystem.criticalChance + " increased by: " + criticalChanceIncrease);
        gunSystem.criticalChance += criticalChanceIncrease;
        player.ChangeBuffText("Critical Chance INCREASED!");
    }

    public void IncreaseCritical()
    {
        gunSystem.criticalDamage += criticalDamageMultiplier;
        player.ChangeBuffText("Critical Damage INCREASED!");
    }

    public void IncreaseRadius()
    {
        gunSystem.explosionRadius *= radiusMultiplier;
        player.ChangeBuffText("Explosive Bullets!");
    }

    public void SlowingShots()
    {
        gunSystem.hasSlowingShots = true;
        player.ChangeBuffText("Slowing Bullets!");
    }

    public void TripleShots()
    {
        gunSystem.hasTripleShots = true;
        player.ChangeBuffText("Triple shots!");
    }

    public void IncreaseHealthRegeneration()
    {
        player.IncreaseMaxHealth(healRegenerationAmount);
        player.IncreaseHealth(healRegenerationAmount);
        player.ChangeBuffText("Health Regeneration Increased!");
    }

    public void CurveShots()
    {
        gunSystem.hasCurveShots = true;
        player.ChangeBuffText("Curvy bullets!");
    }

    public void FreezingShots()
    {
        gunSystem.hasFreezingShots = true;
        player.ChangeBuffText("Freezing bullets!");
    }

    
}
