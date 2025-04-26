using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampScreen : MonoBehaviour
{
    private Vector3 playerPosition;
    public float xMin = -30f;
    public float xMax = 30f;
    public float yMin = -7.5f;
    public float yMax = 20f;

    void Start()
    {
        playerPosition = transform.position;
    }

    void Update()
    {
        playerPosition = transform.position;

        playerPosition.y =  Mathf.Clamp(playerPosition.y, yMin, yMax);

        transform.position = playerPosition;

        Teleport();
    }

    // Teleport player to the other side of the screen when it reaches the screen X limit
    private void Teleport(){
        if (transform.position.x > xMax){
            transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
        }
        if (transform.position.x < xMin){
            transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
        }
    }


}
