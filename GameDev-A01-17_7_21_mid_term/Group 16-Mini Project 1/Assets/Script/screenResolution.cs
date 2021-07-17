using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(3000, 3200, false);
    }

}
