using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerStatus
{
    Idle,
    Waiting,
}

public class CustomerController : MonoBehaviour
{
    public CustomerStatus status = CustomerStatus.Idle;
    public Drink drink;
    public float speed = 0.1f;
    public Transform target;

    public SpriteRenderer iconRenderer;

    public Sprite espressoIcon;
    public Sprite americanoIcon;
    public Sprite latteIcon;

    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        switch (drink)
        {
            case Drink.None:
                iconRenderer.sprite = null;
                break;
            case Drink.Espresso:
                iconRenderer.sprite = espressoIcon;
                break;
            case Drink.Americano:
                iconRenderer.sprite = americanoIcon;
                break;
            case Drink.Latte:
                iconRenderer.sprite = latteIcon;
                break;
        }

        if (status == CustomerStatus.Waiting)
        {
            renderer.flipX = true;

            var iconTransform = iconRenderer.transform;

            iconTransform.localPosition = new Vector3(0.1399999f, iconTransform.localPosition.y, iconTransform.localPosition.z);

            transform.position = Vector3.Lerp(transform.position, target.position, 0.1f * speed);
        }
    }
}
