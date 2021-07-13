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

    
    public int dmg = 30;
    public float atkRange  = 3f;
    //float curSpeed;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        initPosition = transform.position;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void Update(){
        distance = target.position.x - transform.position.x;
        hrt = animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt");
        run = animator.GetCurrentAnimatorStateInfo(0).IsName("Run");
        atk = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");

        if (distance > 0 && !hrt)
            GetComponent<SpriteRenderer>().flipX = false;

        else if (distance < 0 && !hrt)
            GetComponent<SpriteRenderer>().flipX = true;
            
        bool check = false;

        if(-7f < distance && distance < 7f)
            check = true;
        //if (check)
        if (check && rigid.velocity.x !=0 && check && !run && !atk && !hrt)
            animator.Play("Run");

        float distance2 = Vector2.Distance(target.position, transform.position);
        if (distance2 > 4f)
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (distance2 < atkRange && !hrt && queenHp.hp > 0 && !atk){
            //if(!atk)
            animator.Play("Attack");
            queenHp.hp -= dmg;
        }
    }
    
    void OnCollisionEnter2D(Collision2D target){
        if (target.collider.name == "Jump Queen"){
            animator.Play("Hurt");
            rigid.constraints = RigidbodyConstraints2D.FreezePosition;
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
    }

    /*
    void OnCollisionExit2D(Collision2D target){
        if (target.collider.name == "Jump Queen"){
            rigid.constraints = RigidbodyConstraints2D.None;
        }
    }*/
}
