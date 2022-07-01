using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoMain : MonoBehaviour
{
    string currentAnimaton;

    [SerializeField] GameObject StartScreen;
    [SerializeField] Animator animator;
    [SerializeField] GameObject Player;

    [SerializeField] private Rigidbody2D PlayerRB;

    [SerializeField] Transform playertrans;

    [SerializeField] GameObject doorobj;

    [SerializeField] Transform Doorparent;
    [SerializeField] Transform Doorchild1;
    [SerializeField] Transform Doorchild2;

    float CreditsL, CreditsR;

    //[SerializeField] float CreditsR;
    // Start is called before the first frame update
    void Start()
    {
        CreditsL = Doorchild1.position.x;
        CreditsR = Doorchild2.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        EnterDoor();
    }



    void EnterDoor()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("pressed up key ");
            if (playertrans.position.x > CreditsL && playertrans.position.x < CreditsR)
            {
                //Player Animation.
                //Door Open Animation.
                Debug.Log("Take me back...");
                ChangeAnimationState("open", animator);
                StartScreen.GetComponent<SFX>().OpenDoorFunction();
                PlayerRB.constraints = RigidbodyConstraints2D.FreezeAll;

                Debug.Log("TAG IS " + doorobj.tag.ToString());
                if (doorobj.CompareTag("L1"))
                {
                    Invoke("trans2", 2f);
                    Invoke("scenechange1", 3f);
                }
                else if (doorobj.CompareTag("T1"))
                {
                    Invoke("trans2", 2f);
                    Invoke("scenechange2", 3f);
                }
                else if (doorobj.CompareTag("D1"))
                {
                    Invoke("trans2", 2f);
                    Invoke("scenechange3", 3f);
                }
                else if (doorobj.CompareTag("M1"))
                {
                    Invoke("trans2", 2f);
                    Invoke("scenechange0", 3f);
                }
            }
        }
    }
    public void scenechange0()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void scenechange1()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void scenechange2()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void scenechange3()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void trans2()
    {
        
        //Destroy(Player);
        Player.SetActive(false);
        ChangeAnimationState("close", animator);
        StartScreen.GetComponent<SFX>().CloseDoorFunction();
    }

    void ChangeAnimationState(string newAnimation, Animator anim)
    {
        if (currentAnimaton == newAnimation) return;

        anim.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}