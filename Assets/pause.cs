using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    // Start is called before the first frame update
    bool check = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void btn_pause()
    {
        if(!check)
        {
            Time.timeScale = 0;
            check = true;

        }
        else
        {
            Time.timeScale = 1;
            check = false;
        }
    }
}