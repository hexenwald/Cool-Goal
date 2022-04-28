using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovement : MonoBehaviour
{
    [SerializeField] float mouseXClamp = 5f;

    Vector3 first, second;
    public float speed = 15;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            first = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
        }

        var p = transform.position;
        p.x += Input.GetAxis("Mouse X") * Time.deltaTime;
        p.x = Mathf.Clamp(p.x, -mouseXClamp, mouseXClamp);
        transform.position = p;

        if (Input.GetMouseButton(0))
        {
            second = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
            Vector3 diff = second - first;
            transform.position += diff * speed;
            first = second;
        }
    }
}
