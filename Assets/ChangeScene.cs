using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public GameObject SplashObj;               //판넬오브젝트
    private Image image;                            //판넬 이미지
    private bool checkbool = false;     //투명도 조절 논리형 변수

    void Awake()
    {
        SplashObj = this.gameObject;                         //스크립트 참조된 오브젝트
        image = SplashObj.GetComponent<Image>();    //판넬오브젝트에 이미지 참조
    }

    public void Change()
    {
        SplashObj.SetActive(true);
        StartCoroutine("MainSplash");                        //코루틴    //판넬 투명도 조절
    }

    IEnumerator MainSplash()
    {
        Color color = image.color;
        color.a = 0;
        image.color = color;                            
        for (int i = 0; i <= 10; i++)                            //for문 100번 반복 0보다 작을 때 까지
        {
            color.a += 23 * 0.01f;               //이미지 알파 값을 타임 델타 값 * 0.01
            image.color = color;                                //판넬 이미지 컬러에 바뀐 알파값 참조
            yield return new WaitForSeconds(0.1f);         
        }
        yield return new WaitForSeconds(0.5f);   
        for (int i = 10; i >= 0; i--)                            //for문 100번 반복 0보다 작을 때 까지
        {
            color.a -= 23 * 0.01f;               //이미지 알파 값을 타임 델타 값 * 0.01
            image.color = color;                                //판넬 이미지 컬러에 바뀐 알파값 참조
            yield return new WaitForSeconds(0.1f);         
        }                                 //코루틴 종료
        SplashObj.SetActive(false);
    }

}
