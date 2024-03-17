using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradepointer : MonoBehaviour
{
    private Transform _playerTransform;
    private Camera _camera;
    
    
    [SerializeField] private Transform _pointerIconTransform;

    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerController>().transform;
        _camera = Camera.main;
    }

    void Update()
    {
        Vector3 fromPlayerToUpgrade = transform.position - _playerTransform.position;
        Ray ray = new Ray(_playerTransform.position, fromPlayerToUpgrade);
        Debug.DrawRay(_playerTransform.position, fromPlayerToUpgrade);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        float minDistance = Mathf.Infinity;
        int planeindex = 0;
        for (int i = 0; i < 4; i++)
        {
            if (planes[i].Raycast(ray, out float distance))
            {
                if (distance < minDistance)
                {
                    minDistance = distance;
                    planeindex = i;
                }   
            }
        }

        minDistance = Mathf.Clamp(minDistance, 0, fromPlayerToUpgrade.magnitude);
        Vector3 worldPosition = ray.GetPoint(minDistance);
        _pointerIconTransform.position = _camera.WorldToScreenPoint(worldPosition);
        _pointerIconTransform.rotation = GetIconRotation(planeindex);
    }

    Quaternion GetIconRotation(int planeindex)
    {
        if (planeindex == 0)
        {
            return Quaternion.Euler(0f, 0f, 90f);
        }else if (planeindex == 1)
        {
            return Quaternion.Euler(0f, 0f, -90f);
        }else if (planeindex == 2)
        {
            return Quaternion.Euler(0f, 0f, 180f);
        }else if (planeindex == 3)
        {
            return Quaternion.Euler(0f, 0f, 0f);
        }
        return Quaternion.identity;
    }
}
