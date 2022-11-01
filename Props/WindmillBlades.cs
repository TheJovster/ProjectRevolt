using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillBlades : MonoBehaviour
{
    [SerializeField] private float turnSpeed;

    void Update()
    {
        transform.Rotate(0f, 0f, turnSpeed * Time.deltaTime);
    }
}
