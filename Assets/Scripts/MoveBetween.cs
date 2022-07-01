using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveBetween : MonoBehaviour
{
    [SerializeField] Vector3 P1;
    [SerializeField] Vector3 P2;
    public float speed = 1.0f;

    void Update()
    {
        transform.position = Vector3.Lerp(P1, P2, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}