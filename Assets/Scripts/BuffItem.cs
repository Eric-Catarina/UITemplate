using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : MonoBehaviour
{
    public enum Rarity
    {
        Common,
        Rare,
        Legendary
    }
    public Color[] rarityColors = new Color[] {
        new Color(0.5f, 0.5f, 0.5f),   // Gray
        new Color(0f, 1f, 0f),         // Green
        new Color(1f, 0.5f, 0f)        // Orange
    };
    public List<string> possibleBuffs;
    public static class RarityBuffs
    {
        public static List<string> commonPossibleBuffs = new List<string>() { "IncreaseMoveSpeed", "IncreaseBulletSpeed", "IncreaseDamage", "IncreaseAttackSpeed" };
        public static List<string> rarePossibleBuffs = new List<string>() { "BubbleShield", "IncreaseCriticalChance", "IncreaseCritical", "IncreaseRadius", "SlowingShots" };
        public static List<string> legendaryPossibleBuffs = new List<string>() { "TripleShots", "IncreaseHealthRegeneration", "CurveShots", "FreezingShots" };


        private static Dictionary<Rarity, List<string>> buffTable = new Dictionary<Rarity, List<string>>()
        {
            { Rarity.Common, commonPossibleBuffs},
            { Rarity.Rare, rarePossibleBuffs },
            { Rarity.Legendary, legendaryPossibleBuffs }
        };

        public static List<string> GetPossibleBuffs(Rarity rarity)
        {
            return buffTable[rarity];
        }
    }
    [SerializeField] private float movement_speed = 1.1f;
    [SerializeField] private float attackSpeedIncrease = 1.1f;
    private PlayerController playerController;
    public Color emissiveColor;
    public Rarity rarity = Rarity.Common;
    [SerializeField]
    private EmissionController emissionController;
    private Renderer myRenderer;
    public string currentBuff;

    void Start()
    {
          switch (rarity)
        {
            case Rarity.Common:
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case Rarity.Rare:
                transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case Rarity.Legendary:
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
            default:
                break;
        }
        possibleBuffs = RarityBuffs.GetPossibleBuffs(rarity);

        emissionController = GetComponent<EmissionController>();
        emissiveColor = rarityColors[(int)rarity];
        emissionController.SetColorAndIntensity(emissiveColor, 3);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        currentBuff = possibleBuffs[Random.Range(0,possibleBuffs.Count)];

        List<string> buffs = RarityBuffs.GetPossibleBuffs(rarity);

        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            return;
        }
        SoundManager soundManager = GameObject.Find("GameManager").GetComponent<SoundManager>();
        soundManager.PlaySFX(4);
        playerController.Blink(emissiveColor);
        playerController.ChangeBuffText(currentBuff);
        collision.gameObject.GetComponent<BuffApplier>().Invoke(currentBuff,0);

        Destroy(gameObject);

    }
    public Rarity GetRandomRarity(float[] rarities)
    {
        // Get the total sum of rarities percentages
        float totalPercentage = 0;
        foreach (float rarityPercentage in rarities)
        {
            totalPercentage += rarityPercentage;
        }

        // Generate a random number between 0 and the total percentage
        float randomPercentage = Random.Range(0f, totalPercentage);

        // Check the random number against each rarity percentage
        float cumulativePercentage = 0;
        for (int i = 0; i < rarities.Length; i++)
        {
            cumulativePercentage += rarities[i];
            if (randomPercentage <= cumulativePercentage)
            {
                // Return the corresponding rarity
                return (Rarity)i;
            }
        }

        // This line is reached if the random percentage is greater than the total percentage
        // Return the highest rarity as a fallback
        return Rarity.Legendary;
    }

}
