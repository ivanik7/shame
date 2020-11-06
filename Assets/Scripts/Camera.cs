using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    private float speed = 3.5f;
    [SerializeField] private Transform target;

    void Update()
    {
        Vector3 pos = target.position;
        pos.z = -5.0f;
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
