using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptEndgame : MonoBehaviour
{
    public LayerMask playerLayer;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D endgameHit;
        endgameHit = Physics2D.Raycast(transform.position, Vector2.left, 1.4f, playerLayer);
        if (endgameHit.collider != null)
            SceneManager.LoadSceneAsync(3);
        
        endgameHit = Physics2D.Raycast(transform.position, Vector2.up, 1.4f, playerLayer);
        if (endgameHit.collider != null)
            SceneManager.LoadSceneAsync(3);

        endgameHit = Physics2D.Raycast(transform.position, Vector2.right, 1.4f, playerLayer);
        if (endgameHit.collider != null)
            SceneManager.LoadSceneAsync(3);
    
        endgameHit = Physics2D.Raycast(transform.position, Vector2.down, 1.4f, playerLayer);
        if (endgameHit.collider != null)
            SceneManager.LoadSceneAsync(3);
    }
}
