using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject bullet;

    [SerializeField] GameObject player;
    [SerializeField] GameObject player1;

    [SerializeField] Transform bulletFace;
    [SerializeField] float movespeed = 5f;
    //bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(bullet,(bulletFace.position),Quaternion.identity);
        //shootInDirection();
        //InvokeRepeating("shootInDirection",2f,5f);
        Debug.Log("ANGLE IS " + parent.transform.rotation.y.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        shootInDirection();
    }
     
    void shootInDirection()
    {
        if (parent.transform.rotation.y == 0)
        {
            movespeed = -Mathf.Abs(movespeed);
            Vector3 newPosition = bullet.transform.position;
            newPosition.x = newPosition.x - (movespeed);
            bullet.transform.position = newPosition;
        }
        else if (parent.transform.rotation.y == 1)
        {
            movespeed = Mathf.Abs(movespeed);
            Vector3 newPosition = bullet.transform.position;
            newPosition.x = newPosition.x - (movespeed);
            bullet.transform.position = newPosition;
        }
        // Vector3 newPosition = bullet.transform.position;
        // newPosition.x = newPosition.x - (movespeed);
        // bullet.transform.position = newPosition;
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
           Destroy(player);
           Destroy(player1);
           Destroy(bullet);
           Instantiate(bullet,(bulletFace.position),Quaternion.identity);
        }
    }
}
