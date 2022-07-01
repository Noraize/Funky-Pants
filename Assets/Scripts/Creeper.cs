using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Creeper : MonoBehaviour
{
    string currentAnimaton;

    //string IDLE_ = "idle";
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Vector3 P1;
    [SerializeField] Vector3 P2;
    

    [SerializeField] GameObject healthbar;
    public Vector3 healthstats;
    [SerializeField] Collider2D enemcol;
    [SerializeField] Collider2D playercol;


    [SerializeField] Text lifetxt;

    int pantCount = 3;
    public float speed = 1.0f;

    private bool flag = true;
    void start()
    {
        healthstats = healthbar.transform.localScale;
        //healthstats.x = -0.16335f;
    }
    void Update()
    {
        if (flag == true)
        {
            transform.position = Vector3.Lerp(P1, P2, Mathf.PingPong(Time.time * speed, 1.0f));
            if (Mathf.Abs(transform.position.x - P1.x) < 0.05f)
            {
                Vector3 temp = transform.rotation.eulerAngles;
                temp.y = 180.0f;
                transform.rotation = Quaternion.Euler(temp);
            }
            if (Mathf.Abs(transform.position.x - P2.x) < 0.05f)
            {
                Vector3 temp = transform.rotation.eulerAngles;
                temp.y = 0.0f;
                transform.rotation = Quaternion.Euler(temp);
            }
        }
        if(pantCount == 0){
            pantCount = 3;
            SceneManager.LoadSceneAsync(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("HIT ");
            flag = false;
            //rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke("destroy", 0.01f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("PLAYER CONTACT ");
            // Physics2D.IgnoreCollision(enemcol,playercol,true);
            // Invoke("life",1f);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //corret code
            Debug.Log("END PLAYER CONTACT ");
            // pantCount = pantCount - 1;
            if(healthbar.transform.localScale.x > 0.000005f){
                Debug.Log("I AM IN");
                healthstats.x = -0.16335f;
                healthbar.transform.localScale += healthstats;
                //healthbar.transform.localScale = healthstats;

            }
            else{
                pantCount = pantCount - 1;
                healthstats.x = 0.49f;
                healthbar.transform.localScale += healthstats;

            }
            //pantCount = -1;  may itna barra chutiyaa hun 
            lifetxt.text = "X" + pantCount;
        }
    }

    // private void OnCollisionExit2D(Collision2D other) {
    //     Physics.IgnoreLayerCollision( 0, 0 , false );

    // }
    // private void life(){
    //     pantCount =- 1;
    //     lifetxt.text = "X" + pantCount;
    //     Invoke("ignore_off",3f);
    // }
    // private void ignore_off(){
    //     Physics2D.IgnoreCollision(enemcol,playercol,false);
    // }
    private void destroy()
    {
        speed = 0f;
        ChangeAnimationState("idle", anim);
        Destroy(this.gameObject, 2f);
    }

    void ChangeAnimationState(string newAnimation, Animator anim)
    {
        if (currentAnimaton == newAnimation) return;

        anim.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}