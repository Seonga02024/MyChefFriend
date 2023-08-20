using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowName : MonoBehaviour
{
    public TMP_Text text;
    public GameObject apiObj;
    // Start is called before the first frame update
    void Start()
    {
        if(apiObj.GetComponent<LoadAPI>().nickname != null){
            text.text = apiObj.GetComponent<LoadAPI>().nickname;
        }
    }

    public void CheckName(GameObject gameObject)
    {
        text.text = gameObject.GetComponent<Charater>().name;
    }
}
