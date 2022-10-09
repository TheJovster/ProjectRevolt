using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Mover mover;

    void Start()
    {
        mover = GetComponent<Mover>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mover.MoveToCursor();
        }
    }
}
