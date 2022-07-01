using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 offset;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = target.position + new Vector3(offset.x, offset.y, 0);
        targetPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPos, 10 * Time.deltaTime);
    }
}

