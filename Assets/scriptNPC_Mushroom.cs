using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptNPC_Mushroom : MonoBehaviour
{
    // Start is called before the first frame update
    private float velocity = 3;
    public GameObject eyes;
    public bool isDead = false;
    private Rigidbody2D rbd;
    private CircleCollider2D ccd;
    public LayerMask mapLayer;
    public LayerMask playerLayer;
    public LayerMask npcLayer;
    private Animator anim;

    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ccd = GetComponent<CircleCollider2D>(); 
    }


    // Update is called once per frame
    void Update()
    {       
        if(!isDead){
            rbd.velocity = new Vector2(velocity, rbd.velocity.y);            
        
        
            RaycastHit2D hit;
            RaycastHit2D playerHit;
            hit = Physics2D.Raycast(eyes.transform.position, transform.right, 0.1f, mapLayer);
            if(hit.collider != null)
            {
                velocity = velocity * -1;
                rbd.velocity = new Vector2(velocity, rbd.velocity.y);
                transform.Rotate(new Vector2(0, 180));
            }

            hit = Physics2D.Raycast(eyes.transform.position, transform.right, 0.1f, npcLayer);
            if(hit.collider != null)
            {                
                velocity = velocity * -1;
                rbd.velocity = new Vector2(velocity, rbd.velocity.y);
                transform.Rotate(new Vector2(0, 180));
            }
            
            playerHit = Physics2D.Raycast(eyes.transform.position, transform.right, 0.1f, playerLayer);
            if (playerHit.collider != null)
            {
                Destroy(playerHit.collider.gameObject);
                SceneManager.LoadSceneAsync(2);
            }

            playerHit = Physics2D.Raycast(eyes.transform.position, -transform.right, 0.1f, playerLayer);
            if (playerHit.collider != null)
            {
                Destroy(playerHit.collider.gameObject);
                SceneManager.LoadSceneAsync(2);
            }   
        }
        else{
            ccd.radius = 0.1039155f;
            anim.SetTrigger("isDead");
            rbd.velocity = new Vector2(0, 0);
            gameObject.layer = 9;
            GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("NPC");
            Physics2D.IgnoreLayerCollision(6, 9);
            Physics2D.IgnoreLayerCollision(8, 9);
        }     
    }
}
