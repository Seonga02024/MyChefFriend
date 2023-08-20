using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDown : MonoBehaviour
{
    public GameObject currentObj;
    public GameObject nextObj;
    
    public void showDownObj()
    {
        StartCoroutine("ShowDownStart");    
    }

    IEnumerator ShowDownStart()
    {
        yield return new WaitForSeconds(1);
        currentObj.SetActive(false);
        nextObj.SetActive(true);
    }
}
