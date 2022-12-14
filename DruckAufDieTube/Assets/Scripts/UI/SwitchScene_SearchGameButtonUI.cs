using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;

[RequireComponent(typeof(UIDocument))]

public class SwitchScene_SearchGameButtonUI : MonoBehaviour
{
    private UIDocument UI_Doc;
    private Button searchGameButton;

    [Required] public GameObject ViewSwitchingGameObject;

    public void Start()
    {
        if (ViewSwitchingGameObject == null)
        {
            Debug.LogWarning("ViewSwitchingGameObject darf nicht null sein");
        }

        UI_Doc = this.transform.GetComponent<UIDocument>();
        if (UI_Doc == null)
        {
            Debug.LogWarning("Couldnt find UI Document - Fatal Error");
        }

        var rootElement = UI_Doc.rootVisualElement;

        searchGameButton = rootElement.Q<Button>("SearchGameButton");
        searchGameButton.clickable.clicked += OnButtonClicked;
    }

    private void OnDestroy()
    {
        searchGameButton.clickable.clicked -= OnButtonClicked;
    }

    private void OnButtonClicked()
    {
        this.gameObject.SwitchViewTo(ViewSwitchingGameObject);
    }
}
