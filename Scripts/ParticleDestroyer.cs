using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour {

    public float destroyTimer = 8f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyTimer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
