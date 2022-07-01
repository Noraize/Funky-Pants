using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMove : MonoBehaviour
{
    [SerializeField] Vector3 P1;
    [SerializeField] Vector3 P2;
    public bool flag = false;
    [SerializeField] float speed = 0.5f;
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer rend;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("startmove",2f);
    }

    void startmove()
    {
        transform.position = Vector3.Lerp(P1, P2, Mathf.PingPong(Time.time * speed, 1.0f));
        if (Mathf.Abs(transform.position.x - P1.x) < 0.05f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (Mathf.Abs(transform.position.x - P2.x) < 0.05f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // public Transform GetTransform(){
    //     return this.transform;
    // }
    // public bool GetDead(){
    //     return flag;
    // }
    // private void OnTriggerEnter2D(Collider2D other) {
    //     //speed = 0f;
    //     //flag = true;
    //     GetTransform();
    // }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Debug.Log("HIT");
            rend.enabled = true;
            Destroy(this.gameObject,0.5f);
            transform.DetachChildren();
        }
    }
}
