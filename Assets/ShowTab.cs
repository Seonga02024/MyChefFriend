using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTab : MonoBehaviour
{
    public GameObject Obj;               //판넬오브젝트
    private float time = 0;
    public float delayTime;
    private bool check = false;

    public void Change()
    {
        Obj.SetActive(true);
        check = true;
    }

    void Update() 
    {
        if(check == true && time < delayTime)
        {
            Debug.Log(time);
            // 절대 좌표 기준으로 이동
            Obj.transform.Translate(Vector3.up * Time.deltaTime * 1000, Space.World);
            time +=  Time.deltaTime;
        }
    }    

    void Reset()
    {

    }
}
