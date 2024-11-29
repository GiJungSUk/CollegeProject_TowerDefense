using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSeedState : MonoBehaviour
{
    private Button myButton;
    [SerializeField]
    private GameManager.SeedState state;

    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        GameManager.Instance.seedState = state;
    }
}
