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

    //atk param
    public LayerMask monLayer;
    public bool inAtk = false;
    float atkRadius = 2f;

    public bool grounded = false;
    public Transform grdCheck;
    public LayerMask grdLayer;

    public Transform wllCheck;
    public Transform wllCheck2;
    float wllRadius = 1f;
    public bool walled = false;

    public queenHp queenHp;
    public bool atk, jmp, hrt, die;
    private AudioSource[] soundEffects;
    private int audioNum;
    private AudioSource jumpSound;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        soundEffects = GetComponents<AudioSource>();
        audioNum = 0;
        jumpSound = soundEffects[4];
    }

    void Update(){
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        if (!die){
            if (Input.GetKeyDown(KeyCode.Space) && grounded && !queenHp.hpIsDecreasing)
                rigid.AddForce(Vector2.up * jmpForce);    
            
            else if (Input.GetKey(KeyCode.LeftArrow))
                curMvSpeed = -mvSpeed;

            else{
                if (Input.GetKey(KeyCode.RightArrow))
                    curMvSpeed = mvSpeed;

                else 
                    curMvSpeed = 0;
                    
            }
        }
        atk = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        //bool run = animator.GetCurrentAnimatorStateInfo(0).IsName("Run");
        jmp = animator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
        hrt = animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt");
        die = animator.GetCurrentAnimatorStateInfo(0).IsName("Death");
        if (Input.GetKeyDown(KeyCode.Tab) && !die){
            if (!atk)
            {
                animator.Play("Attack");
                soundEffects[audioNum++].Play();
                if (audioNum > 3) audioNum = 0;
            }
        }
        else if (walled && !die)
            animator.Play("Wall");
        else if (!grounded && !atk && !jmp && !die)
        {
            animator.Play("Jump");
            if (rigid.velocity.y > 0)
                jumpSound.Play();
        }
        else if (curMvSpeed != 0 && !atk && !jmp && !hrt && !die)
            animator.Play("Run");
        else if (!atk && grounded && !hrt && !die)
            animator.Play("Idle");
    }

    void FixedUpdate(){   
        if (!die){

            grounded = Physics2D.OverlapCircle(grdCheck.position, grdRadius, grdLayer); 
            if(Physics2D.OverlapCircle(wllCheck.position, wllRadius, grdLayer) 
            || Physics2D.OverlapCircle(wllCheck2.position, wllRadius, grdLayer))
                walled = true;
            else walled = false;

            if(Physics2D.OverlapCircle(wllCheck.position, atkRadius,monLayer) 
            || Physics2D.OverlapCircle(wllCheck2.position, atkRadius, monLayer))
                inAtk = true;
            else inAtk = false;

            if (curMvSpeed < 0){
                if (!atk) GetComponent<SpriteRenderer>().flipX = true;
                rigid.velocity = new Vector2(-mvSpeed, rigid.velocity.y);
                
            }
            if (curMvSpeed > 0){
                if (!atk) GetComponent<SpriteRenderer>().flipX = false;
                rigid.velocity = new Vector2(mvSpeed, rigid.velocity.y); 
                
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        if (other.collider.tag == "Monster" && !die)
            rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

}
