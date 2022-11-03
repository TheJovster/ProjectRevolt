using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermillWheel : MonoBehaviour
{
    [SerializeField] private float turnSpeed;

    void Update()
    {
        transform.Rotate(turnSpeed * Time.deltaTime, 0f, 0f);
    }
}
