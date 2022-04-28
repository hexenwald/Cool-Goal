using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPathFollower : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    [SerializeField] private float speedModifier;

    private int routeToGo;
    private float tParam;
    private Vector3 ballPosition;
    private bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            routeToGo = 0;
            tParam = 0f;
            speedModifier = 0.8f;
            coroutineAllowed = true;
        }

        if (coroutineAllowed)
            StartCoroutine(GoByTheRoute(routeToGo));
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            ballPosition = Mathf.Pow(1 - tParam, 2) * p0 +
                2 * Mathf.Pow(1 - tParam, 1) * tParam * p1 +
                (1 - tParam) * Mathf.Pow(tParam, 2) * p2;

            transform.position = ballPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 1;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
            routeToGo = 0;

        coroutineAllowed = true;
    }
}
