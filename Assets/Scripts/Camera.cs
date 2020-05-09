using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    private float speed = 2.0f;
    [SerializeField] private Transform target;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = target.position;
        pos.z = -5.0f;
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
