using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptNPC_FlyingEye : MonoBehaviour
{
    // Start is called before the first frame update
    private float velocity = 3.3f;
    public GameObject eyes;
    private Rigidbody2D rbd;
    private CircleCollider2D ccd;
    public LayerMask mapLayer;
    public LayerMask playerLayer;
    private Animator anim;
    public bool isDead = false;
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ccd = GetComponent<CircleCollider2D>();
        Physics2D.IgnoreLayerCollision(8, 10);
    }



    // Update is called once per frame
    void Update()
    {       
        if(!isDead){
            rbd.velocity = new Vector2(velocity, rbd.velocity.y);            
           
        
        
            RaycastHit2D hit;
            
            hit = Physics2D.Raycast(eyes.transform.position, transform.right, 0.6f, mapLayer);
            if(hit.collider != null)
            {
                velocity = velocity * -1;
                rbd.velocity = new Vector2(velocity, rbd.velocity.y);
                transform.Rotate(new Vector2(0, 180));
            }
            RaycastHit2D playerHit;
            playerHit = Physics2D.Raycast(transform.position, -transform.right, 0.7f, playerLayer);
            if (playerHit.collider != null)
            {
                Destroy(playerHit.collider.gameObject);
                SceneManager.LoadSceneAsync(2);
            }

            playerHit = Physics2D.Raycast(transform.position, transform.right, 0.7f, playerLayer);
            if (playerHit.collider != null)
            {
                Destroy(playerHit.collider.gameObject);
                SceneManager.LoadSceneAsync(2);
            }
            
            playerHit = Physics2D.Raycast(transform.position, -transform.up, 0.5f, playerLayer);
            if (playerHit.collider != null)
            {
                Destroy(playerHit.collider.gameObject);
                SceneManager.LoadSceneAsync(2);
            }            
        }
        else{
            ccd.radius = 0.1039155f;
            ccd.offset = new Vector2(0, 0.025f);
            anim.SetTrigger("isDead");
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            rbd.velocity = new Vector2(0, -5);
            gameObject.layer = 9;
            GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("NPC");
            Physics2D.IgnoreLayerCollision(6, 9);
            Physics2D.IgnoreLayerCollision(8, 9);
        }     
    }
}
