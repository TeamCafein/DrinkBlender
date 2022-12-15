using UnityEngine;
using System.Collections;
using Obi;

[RequireComponent(typeof(BoxCollider2D))]
public class DragDrop : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private ObiEmitter emitter;

    // The plane the object is currently being dragged on
    private Plane dragPlane;

    // The difference between where the mouse is on the drag plane and 
    // where the origin of the object is on the drag plane
    private Vector3 offset;

    private Camera myMainCamera;

    private bool isHold = false;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        emitter = GetComponentInChildren<ObiEmitter>();
    }

    void Start()
    {
        myMainCamera = Camera.main; // Camera.main is expensive ; cache it here
    }

    private void OnMouseDown()
    {
        isHold = true;

        dragPlane = new Plane(myMainCamera.transform.forward, transform.position);
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        offset = transform.position - camRay.GetPoint(planeDist);
    }

    private void OnMouseDrag()
    {
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        rigidbody.MovePosition(camRay.GetPoint(planeDist) + offset);
    }

    private void OnMouseUp()
    {
        isHold = false;
    }

    private void Update()
    {
        if (!isHold)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            emitter.speed = 1;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            emitter.speed = 0;
        }
    }

    //private void FixedUpdate()
    //{
    //    if (rigidbody.velocity.magnitude == 1)
    //    {
    //        rigidbody.velocity = rigidbody.velocity.normalized * 1;
    //    }
    //}
}