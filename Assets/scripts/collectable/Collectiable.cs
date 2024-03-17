using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float exp;
    
    void OnTriggerEnter2D(Collider2D other)
    {

        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.changeExp(exp);
            Destroy(gameObject);

        }
    }

    
}