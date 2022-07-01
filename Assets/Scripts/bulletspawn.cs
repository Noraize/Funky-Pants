using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletspawn : MonoBehaviour
{
    int c;
    [SerializeField] GameObject face;
    [SerializeField] GameObject bullet;
    bool flag = true;
    // Start is called before the first frame update
    void Start()
    {
        c = 0;
    }
    // Update is called once per frame
    void Update()
    {
        flag = face.GetComponent<BulletMove>().checkret();
    }

   
    void FixedUpdate()
    {
        c++;
        if (c > 200)
        {
            Instantiate(bullet,(face.transform.position),Quaternion.identity);
            //Instantiate(child, MyLook.position + EnemyStartOffset, Quaternion.identity, this.transform);
            c = 0;
            flag = false;
        }
    }

    public bool flagret()
    {
        return flag;
    }
}
