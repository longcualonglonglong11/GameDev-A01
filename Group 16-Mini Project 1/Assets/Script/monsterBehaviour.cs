using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    Rigidbody2D rigid;
    float mvSpeed = 10f;
    Vector2 initPosition;
    bool turn = false;
    int step = 0;
    int maxStep = 3;
    //float curSpeed;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        initPosition = transform.position;
    }
    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.collider.name == "Jump Queen")
            animator.Play("Hurt");
    }
}
