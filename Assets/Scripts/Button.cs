using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Button : MonoBehaviour
{
    public Sprite idle;
    public Sprite hover;
    public UnityEvent click;

    private bool isDown = false;

    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        renderer.sprite = hover;
    }

    private void OnMouseExit()
    {
        renderer.sprite = idle;
    }

    private void OnMouseDown()
    {
        isDown = true;
    }

    private void OnMouseUp()
    {
        if (isDown)
        {
            this.click.Invoke();
        }
    }
}
