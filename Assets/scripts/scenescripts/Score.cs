using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Score : MonoBehaviour
{
    private PlayerController playerController;
    private UIDocument uiDocument;
    float levelTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        uiDocument = GetComponent<UIDocument>();
        
    }
   
    // Update is called once per frame
    void Update()
    {
        VisualElement hits = uiDocument.rootVisualElement.Q<VisualElement>("Hits");
        VisualElement time = uiDocument.rootVisualElement.Q<VisualElement>("Time");
        VisualElement progress = uiDocument.rootVisualElement.Q<VisualElement>(className:"unity-progress-bar__title");
        VisualElement progressBar = uiDocument.rootVisualElement.Q<VisualElement>(className: "unity-progress-bar__progress");
        VisualElement slider = progressBar.Q<VisualElement>(className: "unity-slider");
        VisualElement progressBack = uiDocument.rootVisualElement.Q<VisualElement>(className: "unity-progress-bar__background");
        VisualElement points = uiDocument.rootVisualElement.Q<VisualElement>("Points");
        levelTimer += Time.deltaTime;
        if (progressBar != null)
        {
            progressBar.style.backgroundColor = Color.yellow;

            // ������������ �������� � ��������� �� �������� ����� � ������������ �����
            float progressa = (float)playerController.ExpToShow / playerController.ExpToNeed * 100f;

            // ������������� ������ ������ ��������� � ���������
            progressBar.style.width = Length.Percent(progressa);

            // ���������, ������ �� ����� ������������� ������
            if (progressa >= 100f)
            {
                // ���� ��, ������������� ������ ������ � 0% (��� ����� ������ ��������)
                progressBar.style.width = Length.Percent(0);
            }
        }
        if (progressBack != null)
        {
            progressBack.style.backgroundColor = new Color(1f, 0f, 0f, 0.5f); // ������������ 0.5 (50%)
            
        }
        if(progress != null)
        {
            progress.Q<TextElement>().text = "Level: " + playerController.LvlToShow.ToString();
        }
        if (hits != null)
        {
            hits.Q<TextElement>().text = "Health: " + playerController.Health.ToString();
        }
        if (time != null)
        {
            time.Q<TextElement>().text = "Time: " + levelTimer.ToString("00.00");
        }

        if (points != null)
        {
            points.Q<TextElement>().text = "Points:" + playerController.PointsToShow.ToString();
        }

        // Additional logic for updating the score text goes here
    }
}