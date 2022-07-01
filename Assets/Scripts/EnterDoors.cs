using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoors : MonoBehaviour
{
    [SerializeField] GameObject StartScreen;
    //[SerializeField] GameObject Door;
    [SerializeField] Animator animdoor;
    [SerializeField] Animator animdoor2;
    [SerializeField] Animator animdoor3;
    public string currentAnimaton;
    float LevelL = 0, LevelR = 0, TutorialL = 0, TutorialR = 0, CreditsL = 0, CreditsR = 0;
    [SerializeField] Transform Player;
    public Rigidbody2D PlayerRB;

    // Start is called before the first frame update
    void Start()
    {
        //anim = Door.GetComponent<Animation>();
        PlayerRB = GetComponent<Rigidbody2D>();
        LevelL = StartScreen.GetComponent<EntryPoints>().E1.transform.position.x + StartScreen.transform.position.x;
        LevelR = StartScreen.GetComponent<EntryPoints>().E2.transform.position.x + StartScreen.transform.position.x;
        TutorialL = StartScreen.GetComponent<EntryPoints>().E3.transform.position.x + StartScreen.transform.position.x;
        TutorialR = StartScreen.GetComponent<EntryPoints>().E4.transform.position.x + StartScreen.transform.position.x;
        CreditsL = StartScreen.GetComponent<EntryPoints>().E5.transform.position.x + StartScreen.transform.position.x;
        CreditsR = StartScreen.GetComponent<EntryPoints>().E6.transform.position.x + StartScreen.transform.position.x;
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
            if (Player.position.x > LevelL && Player.position.x < LevelR)
            {
                //Player Animation.
                //Door Open Animation.
                Debug.Log("Level 1");
                ChangeAnimationState("open", animdoor);
                StartScreen.GetComponent<SFX>().OpenDoorFunction();
                PlayerRB.constraints = RigidbodyConstraints2D.FreezeAll;
                Invoke("trans", 2f);
                SceneManager.LoadSceneAsync(2);

            }
            else if (Player.position.x > TutorialL && Player.position.x < TutorialR)
            {
                //Player Animation.
                //Door Open Animation.
                Debug.Log("Tutorial");
                ChangeAnimationState("open", animdoor2);
                StartScreen.GetComponent<SFX>().OpenDoorFunction();
                PlayerRB.constraints = RigidbodyConstraints2D.FreezeAll;
                Invoke("trans2", 2f);
                SceneManager.LoadSceneAsync(1);
            }
            else if (Player.position.x > CreditsL && Player.position.x < CreditsR)
            {
                //Player Animation.
                //Door Open Animation.
                Debug.Log("Do Not Enter");
                ChangeAnimationState("open", animdoor3);
                StartScreen.GetComponent<SFX>().OpenDoorFunction();
                PlayerRB.constraints = RigidbodyConstraints2D.FreezeAll;
                Invoke("trans3", 2f);
                SceneManager.LoadSceneAsync(3);
            }
        }
    }

    public void trans()
    {
        ChangeAnimationState("close", animdoor);
        StartScreen.GetComponent<SFX>().CloseDoorFunction();
    }

    public void trans2()
    {
        ChangeAnimationState("close", animdoor2);
        StartScreen.GetComponent<SFX>().CloseDoorFunction();
    }
    public void trans3()
    {
        ChangeAnimationState("close", animdoor3);
        StartScreen.GetComponent<SFX>().CloseDoorFunction();
    }
    void ChangeAnimationState(string newAnimation, Animator anim)
    {
        if (currentAnimaton == newAnimation) return;

        anim.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}
