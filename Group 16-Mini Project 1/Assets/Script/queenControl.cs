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
    float jmpForce = 4800f;
    float grdRadius = 0.7f;


    public bool grounded = false;
    public Transform grdCheck;
    public LayerMask grdLayer;
    public queenHp queenHp;
    public bool atk, jmp, hrt, die;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        if (!die){
            if (Input.GetKeyDown(KeyCode.Space) && grounded && !queenHp.hpIsDecreasing){
                rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                rigid.AddForce(Vector2.up * jmpForce);    
            }
            else if (Input.GetKey(KeyCode.LeftArrow)){
                curMvSpeed = -mvSpeed;
                rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else{
                if (Input.GetKey(KeyCode.RightArrow)){
                    curMvSpeed = mvSpeed;
                    rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
                else {
                    curMvSpeed = 0;
                    rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
        atk = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        //bool run = animator.GetCurrentAnimatorStateInfo(0).IsName("Run");
        jmp = animator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
        hrt = animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt");
        die = animator.GetCurrentAnimatorStateInfo(0).IsName("Death");
        if (Input.GetKeyDown(KeyCode.Tab) && !die){
            if (!atk)
                animator.Play("Attack");
        }
        else if (!grounded && !atk && !jmp && !die)
            animator.Play("Jump");
        else if (curMvSpeed != 0 && !atk && !jmp && !hrt && !die)
            animator.Play("Run");
        else if (!atk && grounded && !hrt && !die)
            animator.Play("Idle");     
    }

    void FixedUpdate(){   
        if (!die){
            grounded = Physics2D.OverlapCircle(grdCheck.position, grdRadius, grdLayer); 
            if (curMvSpeed < 0){
                GetComponent<SpriteRenderer>().flipX = true;
                rigid.velocity = new Vector2(-mvSpeed, rigid.velocity.y);
                //rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            if (curMvSpeed > 0){
                GetComponent<SpriteRenderer>().flipX = false;
                rigid.velocity = new Vector2(mvSpeed, rigid.velocity.y); 
                //rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        
    }

}
