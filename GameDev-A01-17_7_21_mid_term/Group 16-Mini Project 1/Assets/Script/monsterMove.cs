using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid;
    Animator animator;
    monsterBehaviour script;
    public float mvRange = 10f;
    public float mvSpeed = 4f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        script = GetComponent<monsterBehaviour>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector2.Distance(script.target.position, transform.position);
        if (distance > -mvRange && distance < mvRange){
            if (!GetComponent<SpriteRenderer>().flipX)
                rigid.velocity = new Vector2(mvSpeed, rigid.velocity.y);
            if (GetComponent<SpriteRenderer>().flipX)
                rigid.velocity = new Vector2(-mvSpeed, rigid.velocity.y);
        }
        
    }
}
