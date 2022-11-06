using System.Collections;
using System.Collections.Generic;
using Obi;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Controller : MonoBehaviour
{
    public ObiEmitter coffee;
    public ObiEmitter milk;

    void Update()
    {
        var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //this.coffee.transform.position = point;
        //this.milk.transform.position = point;

        if (Input.GetMouseButtonDown(0))
        {
            coffee.speed = 1;
        }

        if (Input.GetMouseButtonUp(0))
        {
            coffee.speed = 0;
        }

        if (Input.GetMouseButtonDown(1))
        {
            milk.speed = 1;
        }

        if (Input.GetMouseButtonUp(1))
        {
            milk.speed = 0;
        }
    }
}
