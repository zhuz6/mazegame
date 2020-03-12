using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter: MonoBehaviour {

    public GameObject projectile;
    public float initialSpeed = 100;
    public GameObject explosionPrefab;
    public AudioClip audio;


    // Update is called once per frame
    void Update () {

        if(Input.GetKeyDown(KeyCode.F) == true)
        {
            // Instantiate the missile at the position and rotation of this object's transform
            GameObject clone = Instantiate(projectile, transform.position + transform.forward * 0.5f, transform.rotation);
            GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            Destroy(explosion, 0.5f);
            AudioSource.PlayClipAtPoint(audio, gameObject.transform.position);


            Rigidbody cloneRB = clone.GetComponent<Rigidbody>();

            cloneRB.velocity = initialSpeed * -transform.forward;

            Destroy(clone, 10.0f);

        }
		
	}
}
//0.58 0.03 -1.41