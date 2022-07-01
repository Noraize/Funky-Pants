using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameObject Scene;
    [SerializeField] Text Score;
    [SerializeField] Text PantsCount;

    int Total = 0, PantsTotal = 3;
    // public Text Score;

    // Start is called before the first frame update
    void Start()
    {
        PantsCount.text = "X" + PantsTotal;
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "X" + Total;
        
    }
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("SmallCollect"))
    //     {
    //         // float size = Physics2D.Distance(GetComponent<Collider2D>(),other.collider);
    //         // Debug.Log("DISTANCE B/W them two " + size.ToString());

    //         // Physics2D.IgnoreCollision(GetComponent<Collider2D>(),other.otherCollider,true);
    //         Debug.Log("hit a squiggle ");
    //         Destroy(other.gameObject);
    //         Total = Total + 1;
    //     }
    //     else if (other.gameObject.CompareTag("LargeCollect"))
    //     {
    //         Destroy(other.gameObject);
    //         Total = Total + 5;
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SmallCollect"))
        {
            // float size = Physics2D.Distance(GetComponent<Collider2D>(),other.collider);
            // Debug.Log("DISTANCE B/W them two " + size.ToString());

            // Physics2D.IgnoreCollision(GetComponent<Collider2D>(),other.otherCollider,true);
            //Debug.Log("hit a squiggle ");
            Scene.GetComponent<SFX>().SquiggleStart();
            Destroy(other.gameObject);
            Total = Total + 1;
        }
        else if (other.gameObject.CompareTag("LargeCollect"))
        {
            Destroy(other.gameObject);
            Scene.GetComponent<SFX>().SquiggleStart();
            Total = Total + 5;
        }
    }
}
