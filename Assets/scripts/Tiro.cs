using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour {

    public float velocidade = 0.3f;    
    private Vector3 pos;

    // Use this for initialization
    void Start () {

        pos = GameObject.Find("jogador").transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        transform.position += ((transform.position - pos).normalized) * velocidade;        
    }
}
