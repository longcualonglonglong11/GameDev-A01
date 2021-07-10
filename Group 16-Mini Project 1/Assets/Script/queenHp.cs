using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queenHp : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp;
    int maxHp = 2000;
    Animator animator;
    public queenControl control;
    int preHp;
    public bool hpIsDecreasing = false;
    public healthbarUI healthbar; 

    void Start()
    {
        hp = maxHp;
        preHp = hp;
        animator = GetComponent<Animator>();
        healthbar.SetMaxHealth(maxHp);
    }

    void Update(){
        if (hp <= 0){
            animator.Play("Death");
        }
        if (hp < preHp){
            if (!control.hrt)
                animator.Play("Hurt");
            preHp = hp;
            hpIsDecreasing = true;
        }
        else hpIsDecreasing = false;
        healthbar.SetHealth(hp);
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.collider.tag == "Monster" && !control.die){
            hp -= 100;
            animator.Play("Hurt");
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else if (other.collider.tag == "DeadZone"){
            hp = 0;
        }
        
    }
}
