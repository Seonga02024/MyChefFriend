using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class Ranking : MonoBehaviour
{
    public GameObject tamagotchi;
    public GameObject loadAPI;
    public TMP_Text[] ranks = new TMP_Text[8];
    
    public void Start()
    {
        Debug.Log("showRank");
        string URL = "https://junction.accongbox.com/api/v1/tamagotchis/ranking";
        StartCoroutine(showRankUpload(URL));
    }

    IEnumerator showRankUpload(string URL)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);
        request.SetRequestHeader("Authorization", "Bearer " + loadAPI.GetComponent<LoadAPI>().access_token);
        yield return request.SendWebRequest();
        if(request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }else{
            Debug.Log(request.downloadHandler.text);
            string json = request.downloadHandler.text;
            SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
            for(int i=0; i<8; i++)
            {
                Debug.Log(stats["response"][0]["tamagotchis"][i]["account"]["username"]);
                if(stats["response"][0]["tamagotchis"][i]["account"]["username"]){
                    string nick = stats["response"][0]["tamagotchis"][i]["account"]["username"];
                    ranks[i].text = nick;
                }
            }
            //Debug.Log(stats["response"]["tamagotchis"][0]);

        }
    }
}

