using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTUT : MonoBehaviour
{
    string currentAnimaton;
    [SerializeField] Animator animator;
    [SerializeField] GameObject player;

    [SerializeField] GameObject Scene;
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = player.GetComponent<SpriteRenderer>();
        Invoke("changelayer",1f);
    }

    void changelayer(){
        Scene.GetComponent<SFX>().OpenDoorFunction();
        ChangeAnimationState("open",animator);
        rend.sortingOrder = 4;
        Invoke("trans",1f);
    }
    public void trans()
    {
        Scene.GetComponent<SFX>().CloseDoorFunction();
        ChangeAnimationState("close", animator);
    }
    void ChangeAnimationState(string newAnimation, Animator anim)
    {
        if (currentAnimaton == newAnimation) return;

        anim.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
    
}
