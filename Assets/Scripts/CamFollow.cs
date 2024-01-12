using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;

    void Start()
    {

    }

    void Update()
    {
        if(target != null)
            transform.position = target.position;
    }
}
