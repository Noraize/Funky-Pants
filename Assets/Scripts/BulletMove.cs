using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject face;
    [SerializeField] GameObject self;
    [SerializeField] float movespeed;
    bool check;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    public bool checkret(){
        return check;
    }

    void FixedUpdate()
    {
         check = face.GetComponent<bulletspawn>().flagret();
        //GetComponent<EntryPoints>()  

        //Instantiate(child, MyLook.position + EnemyStartOffset, Quaternion.identity, this.transform);
        if(check == true){
            shootInDirection();
            check = false;
        }
    }


    void shootInDirection()
    {
        if (parent.transform.rotation.y == 0)
        {
            movespeed = -Mathf.Abs(movespeed);
            // Vector3 newPosition = this.transform.position;
            // newPosition.x = newPosition.x - (movespeed);
            // this.transform.position = newPosition;
        }
        else if (parent.transform.rotation.y == 1)
        {
            movespeed = Mathf.Abs(movespeed);
            // Vector3 newPosition = this.transform.position;
            // newPosition.x = newPosition.x - (movespeed);
            // this.transform.position = newPosition;
        }
        Vector3 newPosition = this.transform.position;
        newPosition.x = newPosition.x - (movespeed);
        this.transform.position = newPosition;
    }


    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.ToString());
        if(other.gameObject.tag == "Player")
        {
            //Destroy(other.gameObject);
            Destroy(this.gameObject);
            Destroy(self.gameObject);
            check = true;
        }
    }

}
