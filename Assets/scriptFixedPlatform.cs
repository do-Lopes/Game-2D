using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixedPlatform : MonoBehaviour
{
    private BoxCollider2D bcd;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        bcd = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D PlayerHit;
        PlayerHit = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, playerLayer);
        if(PlayerHit.collider != null)
        {
            if(Input.GetKey(KeyCode.LeftControl)){
                StartCoroutine(GetOffPlatform());
            }
        }
    }

    private IEnumerator GetOffPlatform()
    {        
        bcd.enabled = false; 
        yield return new WaitForSeconds(0.7f);
        bcd.enabled = true;
    }
}
