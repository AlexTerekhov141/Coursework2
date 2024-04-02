using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ending : MonoBehaviour
{
    private PlayerController playerController;
    private Enemy EnemyController;
    private UIDocument uiDocument;
    float levelTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        uiDocument = GetComponent<UIDocument>();
        EnemyController = FindObjectOfType<Enemy>();
    }

    void Update()
    {
        VisualElement points = uiDocument.rootVisualElement.Q<VisualElement>("points:");

        if (points != null)
        {
            //points.Q<TextElement>().text = "points: " + playerController.pointsToShow.ToString();
        }
        
    }

}
