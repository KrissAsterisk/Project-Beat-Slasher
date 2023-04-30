using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemies : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public Sprite enemy;
    public SpriteRenderer sr;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.AddForce(-transform.right*speed);
    }
}
