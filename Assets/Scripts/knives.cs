using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knives : MonoBehaviour
{
    [SerializeField] Rigidbody2D knivess;
    [SerializeField] float shootingforce;
    [SerializeField] float shootingrange;
    [SerializeField] Transform shootingdirection;

    // Start is called before the first frame update
    void Start()
    {
        //shootingdirection = 
        knivess.AddForce((shootingdirection.position.normalized)*shootingforce*3,ForceMode2D.Impulse);
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Ground"){
            Destroy(this.gameObject);
        }
    }
}
