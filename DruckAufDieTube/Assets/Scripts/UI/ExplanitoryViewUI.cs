using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ExplanitoryViewUI : MonoBehaviour
{
    public float transitionTime = 3f;
    [Required] public GameObject ViewSwitchingGameObject;

    void Start()
    {
        if (ViewSwitchingGameObject == null)
        {
            Debug.LogWarning("ViewSwitchingGameObject darf nicht null sein");
        }
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(transitionTime);
        this.gameObject.SwitchViewTo(ViewSwitchingGameObject);
    }

}
