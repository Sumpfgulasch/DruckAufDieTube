using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanitoryViewUI : MonoBehaviour
{
    public float transitionTime = 3f;
    void Start()
    {
        
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(transitionTime);
        //this.gameObject.SwitchViewTo("GameView");
    }

}
