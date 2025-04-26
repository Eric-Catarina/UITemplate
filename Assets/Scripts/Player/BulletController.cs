using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class BulletController : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifeTime;
    public float lifeTimeCounter;
    public bool isEnemyBullet;
    public bool isPlayerBullet;
    public bool isBossBullet;
    public bool isBurstBullet;

    public float rotationSpeed;
    public GameObject player;
    private GameManager gameManager;
    private CinemachineImpulseSource impulseSource;
    private 
    void Start()
    {
        lifeTimeCounter = lifeTime;
        player = GameObject.Find("Player");  
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));  
        impulseSource = GetComponent<CinemachineImpulseSource>();
        if (isBossBullet){
            transform.Rotate(0, 0, Random.Range(-4f, 4f));
        }
        if (isBurstBullet){
            // Local scale is lower
            transform.localScale = transform.localScale /2;
            speed = speed * 1.5f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // Move Player bullet up
         if (isPlayerBullet)
         {
            transform.position += transform.up * speed * Time.deltaTime;

         }
        
        // Move Enemy bullet in player direction
        if (isEnemyBullet)
        {
            LookAtPlayer();
            transform.position += transform.up * speed * Time.deltaTime;

        }

        if (isBossBullet){
            transform.position -= transform.up * speed * Time.deltaTime;
            if(isBurstBullet){
                speed = Mathf.Lerp(speed, 0, 0.01f);
            }

        }

        lifeTimeCounter -= Time.deltaTime;
        if (lifeTimeCounter <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.CompareTag("Player") && isEnemyBullet ||
            collider.gameObject.CompareTag("Player") && isBossBullet ){
            player.GetComponent<PlayerController>().TakeDamage(damage);
            gameManager.gameObject.GetComponent<CameraShakeManager>().CameraShake(impulseSource, 0.25f);   
        }

        // Check if collider have tag Item
        if (collider.gameObject.CompareTag("Item") && !isEnemyBullet) return;

        gameManager.GetComponent<SoundManager>().PlaySFX(8);
        gameManager.SpawnHitEffect(transform.position);
        gameManager.gameObject.GetComponent<CameraShakeManager>().CameraShake(impulseSource, 0.05f);   
        Destroy(gameObject);

    }

        private void LookAtPlayer(){
            Vector3 playerPosition = player.transform.position;
            Vector3 direction = (playerPosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    

}
