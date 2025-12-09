using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 camera = new Vector3(_target.position.x, _target.position.y, -10);
            transform.position = camera;
        }
    }
}