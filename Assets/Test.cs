using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{
    public Text testText;

    public void MakeAccountButtonClick()
    {
        string URL = "http://43.201.255.0:8080/api/v1/members";

        User user = new User{
            username = "11122",
            password = "11122",
            nickname = "11122"
        };

        string json = JsonUtility.ToJson(user);
        StartCoroutine(Upload(URL, json));

        testText.text = "sccess MakeAccountButtonClick";
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
                testText.text =request.error;
            }else{
                testText.text = request.downloadHandler.text;
            }
        }
    }
}
