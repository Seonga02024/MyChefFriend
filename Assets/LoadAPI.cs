using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class User
{
    public string username;
    public string password;
    public string nickname;
}

public class Request
{
    public string username;
    public string password;
}

public class Rerequest
{
    public string token;
}

public class LoadAPI : MonoBehaviour
{

    public InputField inputFeild_ID; // id
    public InputField inputFeild_PW; // pw

    public InputField Make_ID; // make id
    public InputField Make_PW; // make pw
    public InputField Make_NAME; // make name

    public GameObject splahImg; // splashimg
    public GameObject showDownImg; // show toturial
    public GameObject showMainImg; // show main
    public GameObject failText; // fail login text

    public string access_token = null; // access_token
    public string refresh_token = null; // refresh_token

    public GameObject accountText;
    public string id;
    public string intimacy;
    public string nickname;

    public GameObject loveObj;

    public GameObject Charaterobj1;
    public GameObject Charaterobj2;

    public void LoginButtonClick()
    {
        string URL = "https://junction.accongbox.com/api/v1/accounts/token";

        if(inputFeild_ID != null && inputFeild_PW != null)
        {
            
            Request user = new Request{
                username = inputFeild_ID.text,
                password = inputFeild_PW.text
            };

            inputFeild_ID.text = "";
            inputFeild_PW.text = "";

            string json = JsonUtility.ToJson(user);
            StartCoroutine(Upload(URL, json));
            Debug.Log("Login success");
        }else{
            Debug.Log("Login fail");
        }
    }

    public void MakeAccountButtonClick()
    {
        string URL = "https://junction.accongbox.com/api/v1/members";

        if(Make_ID != null && Make_PW != null && Make_NAME != null)
        {
            
            User user = new User{
                username = Make_ID.text,
                password = Make_PW.text,
                nickname = Make_NAME.text
            };

            Make_ID.text ="";
            Make_PW.text = "";
            Make_NAME.text = "";

            string json = JsonUtility.ToJson(user);
            StartCoroutine(MakeAccountUpload(URL, json));

            Debug.Log("MakeAccountButtonClick success");
        }else{
            Debug.Log("MakeAccountButtonClick fail");
        }
    }

    IEnumerator Upload(string URL, string json)
    {
        UnityWebRequest request = UnityWebRequest.Post(URL, json);
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
                if(failText != null) {
                    failText.SetActive(true);
                    failText.GetComponent<TMP_Text>().text = request.error;
                }
            }else{
                if(failText != null) failText.SetActive(false);
                Debug.Log(request.downloadHandler.text);

                string json1 = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json1);

                // token saving
                if(stats["response"]["accessToken"]) access_token = stats["response"]["accessToken"];
                if(stats["response"]["refreshToken"]) refresh_token = stats["response"]["refreshToken"];

                if(access_token != null && refresh_token != null) GetTamagotchis();
            }
        }
    }

    IEnumerator MakeAccountUpload(string URL, string json)
    {
        UnityWebRequest request = UnityWebRequest.Post(URL, json);
        using(request)
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            if(request.isNetworkError || request.isHttpError)
            {
                //accountText.GetComponent<TMP_Text>().text = request.error;
                Debug.Log(request.error);
            }else{
                Debug.Log(request.downloadHandler.text);
                //accountText.GetComponent<TMP_Text>().text = "success";
                string json1 = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json1);
            }
        }
    }

    public void GetTamagotchis()
    {
        Debug.Log("GetTamagotchis");
        string URL = "https://junction.accongbox.com/api/v1/tamagotchis/my";
        StartCoroutine(GetTamagotchisUpload(URL));
    }

    IEnumerator GetTamagotchisUpload(string URL)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);
        request.SetRequestHeader("Authorization", "Bearer " + access_token);
        yield return request.SendWebRequest();
        if(request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }else{
            Debug.Log(request.downloadHandler.text);
            string json = request.downloadHandler.text;
            SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);

            Debug.Log(stats["response"]["tamagotchis"][0]);

            if(stats["response"]["tamagotchis"][0] == null){
                if(splahImg != null) splahImg.GetComponent<ChangeScene>().Change();
                if(showDownImg != null) showDownImg.GetComponent<ShowDown>().showDownObj();
            }else{
                Debug.Log(stats["response"]["tamagotchis"][0]["id"]);
                id = stats["response"]["tamagotchis"][0]["id"];
                intimacy = stats["response"]["tamagotchis"][0]["intimacyRate"];
                nickname = stats["response"]["tamagotchis"][0]["account"]["nickname"];
                Debug.Log(stats["response"]["tamagotchis"][0]["type"]);
                if(stats["response"]["tamagotchis"][0]["type"] == 0){
                    Charaterobj2.SetActive(false);
                }else{
                    Charaterobj1.SetActive(false);
                }
                loveObj.GetComponent<LoveBar>().CharaterNum(stats["response"]["tamagotchis"][0]["type"]);
                loveObj.GetComponent<LoveBar>().checkLove(intimacy);
                if(splahImg != null) splahImg.GetComponent<ChangeScene>().Change();
                if(showMainImg != null) showMainImg.GetComponent<ShowDown>().showDownObj();
                
            }
        }
    }

    // IEnumerator UploadMain(string URL, string json)
    // {
    //     UnityWebRequest request = UnityWebRequest.Post(URL, json);
    //     using(request)
    //     {
    //         byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
    //         request.uploadHandler = new UploadHandlerRaw(jsonToSend);
    //         request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    //         request.SetRequestHeader("Content-Type", "application/json");

    //         yield return request.SendWebRequest();
    //         if(request.isNetworkError || request.isHttpError)
    //         {
    //             Debug.Log(request.error);
    //             if(failText != null) failText.SetActive(true);
    //         }else{
    //             Debug.Log(request.downloadHandler.text);
    //             if(request.downloadHandler.text == "null"){
    //                 if(splahImg != null) splahImg.GetComponent<ChangeScene>().Change();
    //                 if(showDownImg != null) showDownImg.GetComponent<ShowDown>().showDownObj();
    //             }else{
    //                 if(splahImg != null) splahImg.GetComponent<ChangeScene>().Change();
    //                 if(showMainImg != null) showMainImg.GetComponent<ShowDown>().showDownObj();
    //             }
    //         }
    //     }
    // }

        //UnityWebRequest request = UnityWebRequest.Put(URL, json);
        //request.SetRequestHeader("Authorization", "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiIxMDAwMDAwMDAwMCIsImF1dGgiOiJST0xFX01FTUJFUiIsImV4cCI6MTY5MjM3NTE1Nn0._9oqTOOElkvq72EoOlbslw1c6QeKQbSuGefEHrnK3jhhy-DZrYzCq9sd7IkowD0YOI85KcqvmFm_VeC_mATLdQ");
        

    // IEnumerator GetDatas()
    // {
    //     using(UnityWebRequest request = UnityWebRequest.Get(URL))
    //     {
    //         yield return request.SendWebRequest();
    //         if(request.result == UnityWebRequest.Result.ConnectionError){
    //             Debug.LogError(request.error);
    //         }else{
    //             string json = request.downloadHandler.text;
    //             SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
    //             // text1.text = stats;
    //             // text1.text = stats[index]["level"];
    //             // text2.text = stats[index]["exp"];
    //         }
    //     }
    // }
        
}

/*
    string jsonResult;
    bool isOnLoading = true;

    void Start()
    {
        StartCoroutine(LoadData());
    }


    IEnumerator LoadData() //json 문자열 받아오기
    {
        string GetDataUrl = "http://openapi.airkorea.or.kr/openapi/services/rest/ArpltnInforInqireSvc/getCtprvnMesureSidoLIst?sidoName=%EC%84%9C%EC%9A%B8&searchCondition=DAILY&pageNo=1&numOfRows=25&ServiceKey=(내 인증키 입력하는 칸)&_returnType=json";
        using (UnityWebRequest www = UnityWebRequest.Get(GetDataUrl))
        {
            //www.chunkedTransfer = false;
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError) //불러오기 실패 시
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    isOnLoading = false;
                    jsonResult =
                        System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(jsonResult);
                }
            }
        }
    }
*/