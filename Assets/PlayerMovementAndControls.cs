﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAndControls : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private bool pressA, pressD;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        {
            pressA = Input.GetKey(KeyCode.A);
            pressD = Input.GetKey(KeyCode.D);
        }
        if ((pressA || pressD) && !(pressD && pressA))
        {
            if (pressD)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime );
            }
            if (pressA)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
    }
}
