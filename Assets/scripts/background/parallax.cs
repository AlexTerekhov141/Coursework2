using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField] private Transform followingTarget;

    [SerializeField, Range(0f, 1f)] private float parallaxStrentgh = 0.1f;

    [SerializeField] private bool disableVerticalParallax;

    private Vector3 targetPreviousposition;
    // Start is called before the first frame update
    void Start()
    {
        if (!followingTarget)
        {
            followingTarget = Camera.main.transform;
        }

        targetPreviousposition = followingTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        var delta = followingTarget.position - targetPreviousposition;
        targetPreviousposition = followingTarget.position;
        transform.position += delta * parallaxStrentgh;
    }
}
