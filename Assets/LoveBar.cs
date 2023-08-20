using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoveBar : MonoBehaviour
{
    public Slider slider;
    public GameObject tamagotchi;
    public GameObject loadAPI;
    int hp;

    public GameObject chara1;
    public GameObject chara2;
    public GameObject chara3;
    public GameObject chara4;

    public GameObject chara21;
    public GameObject chara22;
    public GameObject chara23;
    public GameObject chara24;

    public int settingCharaterNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        hp = 0;
        slider.value = hp;
    }

    public void UpdateLove()
    {
        hp = hp + 1;
        slider.value = hp;
        SetLove();
        checkChara();
    }

    public void CharaterNum(int num){
        settingCharaterNum = num;
        if(num == 0){
            chara1.SetActive(true);
            chara21.SetActive(false);
        }else{
            chara1.SetActive(false);
            chara21.SetActive(true);
        }
    }

    public void checkLove(string num)
    {
        hp = int.Parse(num);
        slider.value = hp;
        checkChara();
    }

    public void checkChara()
    {
        if(hp < 25){
            if(settingCharaterNum == 0){
                chara1.SetActive(true);
                chara2.SetActive(false);
                chara3.SetActive(false);
                chara4.SetActive(false);
            }
            if(settingCharaterNum == 1){
                chara21.SetActive(true);
                chara22.SetActive(false);
                chara23.SetActive(false);
                chara24.SetActive(false);
            }
        }else if(hp < 50){
            if(settingCharaterNum == 0){
                chara1.SetActive(false);
                chara2.SetActive(true);
                chara3.SetActive(false);
                chara4.SetActive(false);
            }
            if(settingCharaterNum == 1){
                chara21.SetActive(false);
                chara22.SetActive(true);
                chara23.SetActive(false);
                chara24.SetActive(false);
            }
        }else if(hp < 75){
            if(settingCharaterNum == 0){
                chara1.SetActive(false);
                chara2.SetActive(false);
                chara3.SetActive(true);
                chara4.SetActive(false);
            }
            if(settingCharaterNum == 1){
                chara21.SetActive(false);
                chara22.SetActive(false);
                chara23.SetActive(true);
                chara24.SetActive(false);
            }
            
        }else{
            if(settingCharaterNum == 0){
                chara1.SetActive(false);
                chara2.SetActive(false);
                chara3.SetActive(false);
                chara4.SetActive(true);
            }
            if(settingCharaterNum == 1){
                chara21.SetActive(false);
                chara22.SetActive(false);
                chara23.SetActive(false);
                chara24.SetActive(true);
            }
        }
    }

    public void SetLove()
    {
        string id = loadAPI.GetComponent<LoadAPI>().id;
        string URL = "https://junction.accongbox.com/api/v1/tamagotchis/" + id + "/increase-intimacy";

        string json = JsonUtility.ToJson(id);
        StartCoroutine(SetLoveUpload(URL, json));
    }

    IEnumerator SetLoveUpload(string URL, string json)
    {
        UnityWebRequest request = UnityWebRequest.Post(URL, json);
        request.SetRequestHeader("Authorization", "Bearer " + loadAPI.GetComponent<LoadAPI>().access_token);
        using(request)
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            if(request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }else{
                Debug.Log("SetCharaterText success " + request.downloadHandler.text);
            }
        }
    }
}
