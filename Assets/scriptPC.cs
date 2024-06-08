 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptPC : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject foot;
    public GameObject eyes;
    private Rigidbody2D rbd;
    private CircleCollider2D ccd;
    private BoxCollider2D bcd;
    public LayerMask mapLayer;
    public LayerMask hangLayer;
    public LayerMask trapLayer;
    public LayerMask enemyLayer;
    private Animator anim;
    public float character_velocity = 5;
    public float jump_force = 420;
    private bool isGrounded = true;
    private bool isHanging = false;
    private bool right_side_detector = true;
    private int direction = -1;
    public float gravity;

    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ccd = GetComponent<CircleCollider2D>();
        bcd = GetComponent<BoxCollider2D>();
        gravity = rbd.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        float x_axis = Input.GetAxis("Horizontal");
        ccd.radius = 0.0930346f;
        ccd.offset = new Vector2(0, -0.0930346f);
        bcd.size =  new Vector2(0.1591998f, 0.1866529f);
        bcd.offset = new Vector2(0, 0.09443172f);

        if(isHanging){
            anim.SetBool("wall_hang", true);
            anim.SetBool("start_jump", false);
            anim.SetBool("end_jump", false);
            anim.SetBool("crouch_walking", false);
            anim.SetBool("crouch", false);
            anim.SetBool("Idle", false);
            anim.SetBool("running", false);
        }
        else{
            anim.SetBool("wall_hang", false);
            if(isGrounded){                 
            anim.SetBool("start_jump", false);
            anim.SetBool("end_jump", false);
                if(rbd.velocity.x == 0 && x_axis == 0){
                    anim.SetBool("crouch_walking", false);
                    anim.SetBool("running", false);
                    if(!Input.GetKey(KeyCode.LeftControl)){                    
                        anim.SetBool("Idle", true);
                        anim.SetBool("crouch", false);
                    }
                    else{
                        anim.SetBool("crouch", true);
                        anim.SetBool("Idle", false);
                        ccd.radius = 0.0653f;
                        ccd.offset = new Vector2(0, -0.06570961f);
                        bcd.size = new Vector2(0.131f, 0.129f);
                        bcd.offset = new Vector2(0, 0.06682298f);  
                    }
                }
                else{
                    anim.SetBool("Idle", false);
                    anim.SetBool("crouch", false);
                    if(Input.GetKey(KeyCode.LeftControl)){
                        anim.SetBool("crouch_walking", true);
                        anim.SetBool("running", false);
                        ccd.radius = 0.0653f;
                        ccd.offset = new Vector2(0, -0.06570961f);
                        bcd.size = new Vector2(0.131f, 0.129f);
                        bcd.offset = new Vector2(0, 0.06682298f);                      
                    }
                    else{
                        anim.SetBool("running", true);
                        anim.SetBool("crouch_walking", false);
                    }
                }
            }
            else{
                anim.SetBool("Idle", false);
                anim.SetBool("crouch", false);
                anim.SetBool("crouch_walking", false);
                anim.SetBool("running", false);
                if(rbd.velocity.y > 0){
                    anim.SetBool("start_jump", true);
                    anim.SetBool("end_jump", false);
                }
                else if(rbd.velocity.y < 0){
                    anim.SetBool("end_jump", true);
                    anim.SetBool("start_jump", false);
                }
            }
        }
        



        //detects if the character is looking to the right side
        if(right_side_detector && x_axis < 0 || !right_side_detector && x_axis > 0)
        {
            right_side_detector = !right_side_detector;
            direction = direction * -1;
            transform.Rotate(new Vector2(0, 180));
        }
        
        //PC jump       
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && !anim.GetBool("crouch") && !anim.GetBool("crouch_walking"))
            rbd.AddForce(new Vector2(0, jump_force));        
                             
        
        if(isHanging && !Input.GetKeyDown(KeyCode.Space))
        {
            rbd.velocity = new Vector2(0, 0);

        }   
        
        //PC move
                   
        rbd.velocity = new Vector2(x_axis * character_velocity, rbd.velocity.y);
        
            
        
        //detects if PC hits the floor
        RaycastHit2D floorHit;
        floorHit = Physics2D.Raycast(foot.transform.position, Vector2.down, 0.5f, mapLayer);
        if(floorHit.collider != null)
        {
            isGrounded = true;
            transform.parent = floorHit.collider.transform;
        }                                 
        else
        {
            isGrounded = false;
            transform.parent = null;
        }

        //detects if PC hits the wall
        RaycastHit2D hangHit;
        hangHit = Physics2D.Raycast(eyes.transform.position, Vector2.right, 0.1f, hangLayer);
        if(hangHit.collider != null)
        {
            if(Input.GetKeyDown(KeyCode.Space))
                isHanging = false;
            else
                isHanging = true;
        }
        else
        {
            isHanging = false;            
        } 


        floorHit = Physics2D.Raycast(foot.transform.position, Vector2.down, 0.5f, trapLayer);
        if(floorHit.collider != null)
            SceneManager.LoadSceneAsync(2);

        RaycastHit2D enemyHit;
        enemyHit = Physics2D.Raycast(foot.transform.position, Vector2.down, 0.5f, enemyLayer);
        if (enemyHit.collider != null)
        {
            if(enemyHit.collider.gameObject.tag == "Mushroom")
            {
                rbd.AddForce(new Vector2(0, jump_force));
                enemyHit.collider.gameObject.GetComponent<scriptNPC_Mushroom>().isDead = true;
            }
            else if(enemyHit.collider.gameObject.tag == "Goblin")
            {
                rbd.AddForce(new Vector2(0, jump_force));
                enemyHit.collider.gameObject.GetComponent<scriptNPC_Goblin>().isDead = true;
            }
            else if(enemyHit.collider.gameObject.tag == "Skeleton")
            {
                rbd.AddForce(new Vector2(0, jump_force));
                enemyHit.collider.gameObject.GetComponent<scriptNPC_Skeleton>().isDead = true;
            }
            else if(enemyHit.collider.gameObject.tag == "Flying-Eye")
            {
                rbd.AddForce(new Vector2(0, jump_force));
                enemyHit.collider.gameObject.GetComponent<scriptNPC_FlyingEye>().isDead = true;
            }                
        }
    }
}


