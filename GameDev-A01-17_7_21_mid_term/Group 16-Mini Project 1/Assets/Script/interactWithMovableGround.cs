using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactWithMovableGround : MonoBehaviour
{

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D otherGameObject)
    {
        if (otherGameObject.gameObject.CompareTag("MovableGround"))
        {
            transform.parent = otherGameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D otherGameObject)
    {
        if (otherGameObject.gameObject.CompareTag("MovableGround"))
        {
            transform.parent = null;
        }
    }
    

}