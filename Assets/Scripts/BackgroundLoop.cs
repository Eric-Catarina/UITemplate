using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public GameObject spriteRendererPrefab;     // Prefab of the sprite renderer
    public float speed = 2f;
    private List<GameObject> spriteRenderers;   // List to store the sprite renderers
    private float distance;                     // Distance the sprite renderer needs to travel before teleporting
    private Vector3 initialPosition;            // Initial position of the sprite renderers

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderers = new List<GameObject>();    // Initialize the list

        spriteRenderers.Add(gameObject);

        // Get the height of the sprite renderer
        float height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;

        // Instantiate the second sprite renderer above the first one
        GameObject spriteRenderer2 = Instantiate(spriteRendererPrefab, transform.position + Vector3.up * height, Quaternion.identity);
        Destroy(spriteRenderer2.GetComponent<BackgroundLoop>());
        spriteRenderers.Add(spriteRenderer2);

        distance = height;      // Set the distance to the height of the sprite renderer
        initialPosition = transform.position;   // Set the initial position

        // Set the scrolling speed of the sprite renderers
        gameObject.GetComponent<Rigidbody>().velocity = new Vector2(0, -speed);
        spriteRenderer2.GetComponent<Rigidbody>().velocity = new Vector2(0, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the first sprite renderer has travelled the distance
        if (spriteRenderers[0].transform.position.y - initialPosition.y <= -distance)
        {
            // Teleport the sprite renderers back to the initial position
            spriteRenderers[0].transform.position = initialPosition;
            spriteRenderers[1].transform.position = initialPosition + Vector3.up * distance;
        }
    }
}
