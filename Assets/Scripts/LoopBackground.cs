using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBackground : MonoBehaviour
{
    public GameObject Background;
    private Camera MainCamera;
    private Vector2 Bounds;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = this.GetComponent<Camera>();
        Bounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        LoadBack(Background);

    }

    // Update is called once per frame  
    void Update()
    {

    }
    void LoadBack(GameObject obj)
    {
        //Debug.Log(obj.name);
        float ObjWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int ChildsNeeded = (int)Mathf.Ceil(Bounds.x * 2 / ObjWidth);
        GameObject Clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= ChildsNeeded; i++)
        {
            GameObject B = Instantiate(Clone) as GameObject;
            B.transform.SetParent(obj.transform);
            B.transform.position = new Vector3(ObjWidth * i, obj.transform.position.y, obj.transform.position.z);
        }
        Destroy(Clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }
}
