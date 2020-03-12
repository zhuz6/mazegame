﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour {


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("enemy"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);

        }

    }
}
