using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour
{

    [SerializeField] Canvas canvas;
    [SerializeField] Button prefab;

    Button currentButton;

    float offset = 50f;


    void Start()
    {
        GenerateButton();
    }

    
    public void OnButtonClick()
    {
        Destroy(currentButton.gameObject);
        GenerateButton();
    }

    private void GenerateButton()
    {
        currentButton = Instantiate(prefab,canvas.transform);
        RectTransform rectTransform = (RectTransform)currentButton.transform;
        rectTransform.localPosition = RandomPosition();

        currentButton.onClick.AddListener(OnButtonClick);
    }

    private Vector3 RandomPosition()
    {
        RectTransform rectTransform = (RectTransform)canvas.transform;
        float x = Random.Range(-rectTransform.rect.width/2 + offset, rectTransform.rect.width / 2 - offset);
        float y = Random.Range(-rectTransform.rect.height / 2 + offset, rectTransform.rect.height / 2 - offset);

        return new Vector3(x,y,0);
    }
}
