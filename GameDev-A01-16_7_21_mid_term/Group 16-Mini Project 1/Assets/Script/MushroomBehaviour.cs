using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBehaviour : MonoBehaviour
{
 
    Animator animator;
    Rigidbody2D rigid;
    public Transform target;
    Vector2 initPosition;

    public float distance;
    public bool hrt, run, atk;
    public queenHp queenHp;
    public queenControl queenControl;

    int dmg = 300;
    float atkRange  = 3f;
    
    public int hp;
    int maxHp = 3;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        initPosition = transform.position;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        hp = maxHp;
    }
    void Update(){
        if (hp > 0){
            distance = target.position.x - transform.position.x;
            hrt = animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt");
            run = animator.GetCurrentAnimatorStateInfo(0).IsName("Run");
            atk = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");

            if (distance > 0 && !hrt)
                GetComponent<SpriteRenderer>().flipX = true;

            else if (distance < 0 && !hrt)
                GetComponent<SpriteRenderer>().flipX = false;
                
            if (rigid.velocity.x !=0 && !atk && !hrt)
                animator.Play("Run");

            float distance2 = Vector2.Distance(target.position, transform.position);
            if (distance2 > 2f)
                rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (distance2 < atkRange && !hrt && queenHp.hp > 0 && !atk){
                //if(!atk)
                animator.Play("Attack");
                queenHp.hp -= dmg;
            }

            if (queenControl.atk && queenControl.inAtk && !hrt){
                animator.Play("Hurt");
                hp -= 1;
            }

        } else {
            animator.Play("Death");
            Destroy(gameObject, 2);
        }
    }
    
    void OnCollisionEnter2D(Collision2D target){
        if (target.collider.name == "Jump Queen" && hp > 0){
            animator.Play("Hurt");
            rigid.constraints = RigidbodyConstraints2D.FreezePosition;
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            queenHp.hp -= dmg;
        }
    }

}
