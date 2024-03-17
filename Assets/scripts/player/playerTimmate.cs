using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class playerTeammate : MonoBehaviour
{
    public float beamDamage = 10f;
    public float beamDuration = 0.5f;
    public float cooldownTime = 1f;
    
    private bool isCoolingDown = false;

    //move
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 10.0f;
    //
    private Transform _enemyTransform;
    //
    private Ray ray = new Ray();
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        _enemyTransform = FindObjectOfType<Enemy>().transform;
    }

    void Update()
    {
        
        ////mouse control
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        //////
        /////move
        Vector2 move = MoveAction.ReadValue<Vector2>();
        
        Vector2 position = (Vector2)transform.position + move * (speed * Time.deltaTime); ;
        transform.position = position;
        
    }
   
    
}
