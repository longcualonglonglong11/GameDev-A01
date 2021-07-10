using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    Rigidbody2D rigid;
    public Transform target;
    Vector2 initPosition;
    public float distance;
    public bool hrt, run, atk;
    public queenHp queenHp;
    //float curSpeed;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        initPosition = transform.position;
    }
    void Update(){
        distance = target.position.x - transform.position.x;
        hrt = animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt");
        run = animator.GetCurrentAnimatorStateInfo(0).IsName("Run");
        atk = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        if (distance > 0 && !hrt)
            GetComponent<SpriteRenderer>().flipX = false;

        else if (distance < 0 && !hrt && !run)
            GetComponent<SpriteRenderer>().flipX = true;
        bool check = false;
        if(-7f < distance && distance < 7f)
            check = true;
        if (check)
        if (rigid.velocity.x !=0 && check && !run && !atk && !hrt)
            animator.Play("Run");
        float distance2 = Vector2.Distance(target.position, transform.position);
        if (distance2 > 4f)
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (distance2 < 3f && !hrt && queenHp.hp > 0){
            animator.Play("Attack");
            queenHp.hp -=1;
        }
    }
    
    void OnCollisionEnter2D(Collision2D target){
        if (target.collider.name == "Jump Queen"){
            animator.Play("Hurt");
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }

    /*
    void OnCollisionExit2D(Collision2D target){
        if (target.collider.name == "Jump Queen"){
            rigid.constraints = RigidbodyConstraints2D.None;
        }
    }*/
}
