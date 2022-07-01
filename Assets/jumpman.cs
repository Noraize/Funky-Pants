using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class jumpman : MonoBehaviour
{
    [SerializeField] GameObject icecream;

    [SerializeField] Rigidbody2D icerig;

    [SerializeField] Rigidbody2D playerrig;

    [SerializeField] GameObject player;

    private float thrust = 1f;
    private float pantCount = 3f;

    private int enemyhealth = 5;

    public Vector3 healthstats;
    [SerializeField] GameObject healthbar;

    [SerializeField] Text lifetxt;
    public Vector3 x;
    // Start is called before the first frame update
    void Start()
    {
        x.x = -100f;
        healthstats.x = healthbar.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyhealth < 0 ){
            //icerig.RigidbodyConstraints2D.None;
            //playerrig.constraints.FreezeAll;
            x.x = player.transform.position.x; 
            icecream.transform.position = x;
           
        }
    }

    void endgamewin(){
        Destroy(this.gameObject,0.5f);
        SceneManager.LoadSceneAsync(3);
    }

    void endgameloose(){
        Destroy(this.gameObject,0.5f);
        SceneManager.LoadSceneAsync(0); 
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground"){
            this.GetComponent<Rigidbody2D>().AddForce(transform.up*thrust,ForceMode2D.Impulse);
        }
        if(other.gameObject.tag == "WallR"){
            this.GetComponent<Rigidbody2D>().AddForce((-transform.right)*(thrust+1f),ForceMode2D.Impulse);
        }
        if(other.gameObject.tag == "WallL"){
            this.GetComponent<Rigidbody2D>().AddForce(transform.right*(thrust+1f),ForceMode2D.Impulse);
        }
        if(other.gameObject.tag == "Shell"){
            if(enemyhealth <= 0){
                gameObject.transform.position = x;
                Invoke("endgamewin",3f);
            }
        }    
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Shell"){
            enemyhealth--;
        }
        if(other.gameObject.tag == "Player"){
            if(healthbar.transform.localScale.x > 0.05f){
                healthstats.x = -0.081675067f;
                healthbar.transform.localScale += healthstats;
            }
            else{
                pantCount = pantCount - 1;
                healthstats.x = 0.2450252f;
                healthbar.transform.localScale += healthstats;

            }
            lifetxt.text = "X" + pantCount;
            if(pantCount <= 0){
                Invoke("endgameloose",3f);
            }
        }
    }
}
