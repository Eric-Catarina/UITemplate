using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemGenerator : MonoBehaviour
{
    public GameObject itemPrefab;
    [Range(0.0f, 5f)]
    public float rangeFloat = 1;
    private int currentRarityLevel = 0;
    void Start()
    {
        StartCoroutine(SpawnItem());
    }

    private int Choose(float[] probabilities)
    {
        float total = 0;

        foreach (float elem in probabilities)
        {
            total += elem;
        }

        float randomPoint = UnityEngine.Random.value * total;

        for (int i = 0; i < probabilities.Length; i++)
        {
            if (randomPoint < probabilities[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probabilities[i];
            }
        }

        return probabilities.Length - 1;
    }



    // Spawn a item every 1 second
    IEnumerator SpawnItem()
    {
        while (true)
        {

            GameObject itemInstance = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            currentRarityLevel = Random.Range(1, 4);
            BuffItem.Rarity rarity;
            float[] raritiesPercentages = { 70f, 20f, 10f };


            rarity = GetRandomRarity(raritiesPercentages);
            itemInstance.GetComponent<BuffItem>().rarity = rarity;
            yield return new WaitForSeconds(rangeFloat);

        }
    }
    public BuffItem.Rarity GetRandomRarity(float[] rarities)
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
                return (BuffItem.Rarity)i;
            }
        }

        // This line is reached if the random percentage is greater than the total percentage
        // Return the highest rarity as a fallback
        return BuffItem.Rarity.Legendary;
    }

}
