using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movebullet : MonoBehaviour
{
    [SerializeField] float movespeed = 0.03f;
    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("flagg",2f);
        if(flag == true){
            Vector3 newPosition = this.transform.position;
            newPosition.x = newPosition.x - (movespeed);
            this.transform.position = newPosition;
        }
    }

    void flagg(){
        flag = true;
    }        
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
           Destroy(other);
           //Destroy(player1);
           Destroy(this.gameObject);
           flag = false;
           //Instantiate(bullet,(bulletFace.position),Quaternion.identity);
        }
    }   
}
