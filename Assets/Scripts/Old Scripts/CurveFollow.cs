using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveFollow : MonoBehaviour
{
    public Transform[] point;
    public float ballSpeed;
    private int current;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (transform.position != point[current].position)
                {
                    Vector3 pos = Vector3.MoveTowards(transform.position, point[current].position, ballSpeed * Time.deltaTime);
                    GetComponent<Rigidbody>().MovePosition(pos);
                }

            else current = (current + 1) % point.Length;
        }
        
    }
}
