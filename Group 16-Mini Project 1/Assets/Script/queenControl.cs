using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queenControl : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    
    //move param
    float mvSpeed = 13f;
    float curMvSpeed = 0;

    //jump param
    float jmpForce = 1000f;
    float grdRadius = 0.2f;
    bool grounded = false;
    public Transform grdCheck;
    public LayerMask grdLayer;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            rigid.AddForce(Vector2.up * jmpForce);
        else if (Input.GetKey(KeyCode.LeftArrow))
            curMvSpeed = -mvSpeed;
        else{
            if (Input.GetKey(KeyCode.RightArrow))
                curMvSpeed = mvSpeed;
            else curMvSpeed = 0;
        }
        bool atk = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        //bool run = animator.GetCurrentAnimatorStateInfo(0).IsName("Run");
        bool jmp = animator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
        if (Input.GetKeyDown(KeyCode.Tab)){
            if (!atk)
                animator.Play("Attack");
        }
        else if (!grounded && !atk && !jmp)
            animator.Play("Jump");
        else if (curMvSpeed != 0 && !atk && !jmp)
            animator.Play("Run");
        else if (!atk && grounded)
            animator.Play("Idle");     
    }

    void FixedUpdate(){   
        grounded = Physics2D.OverlapCircle(grdCheck.position, grdRadius, grdLayer); 
        if (curMvSpeed < 0){
            GetComponent<SpriteRenderer>().flipX = true;
            rigid.velocity = new Vector2(-mvSpeed, rigid.velocity.y);
        }
        if (curMvSpeed > 0){
            GetComponent<SpriteRenderer>().flipX = false;
            rigid.velocity = new Vector2(mvSpeed, rigid.velocity.y); 
        }
    }
}
