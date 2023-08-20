using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class SetTamagotchis
{
    public string name;
    public string personality;
    public int type;
}

public class chatHistory
{
    public string role;
    public string content;
}

public class SetChat
{
    public List<chatHistory> chatHistories;
    public string prompt;
}

public class Type{
    public int type;
}

public class Charater : MonoBehaviour
{
    private List<chatHistory> histories;

    private string charaterText;
    private chatHistory[] beforeInputText;
    private string currentInput;
    public int countingNum = 0;

    public GameObject loadAPI;
    public Text userText;
    public Text robotText;

    public GameObject charaterlove;

    public string name;
    public InputField inputFeild_name; 

    public GameObject charater1;
    public GameObject charater2;
    public int charaternum =0;

    void Start()
    {
        charaterText = "";
        histories = new List<chatHistory>();
        histories.Add(null);
    }

    public void SetName()
    {
        name = inputFeild_name.text;
        SetCharaterText();
    }

    // Update is called once per frame
    public void addCharater(Text text)
    {
        if(countingNum == 0 && text.text[0] == '1')
        {
            charaterText = "Knowledge scope: Western food experts with basic common sense";
        }
        else if(countingNum == 0 && text.text[0] == '2')
        {
            charaterText = "Scope of Knowledge: Vegan Food Specialist with Basic Common Sense";
        }
        else if(countingNum == 0 && text.text[0] == '3')
        {
            charaterText = "scope of knowledge: Korean food expert with basic common sense";
            Debug.Log(charaterText);
        }

        if(countingNum == 1 && text.text[0] == '1')
        {
            charater2.SetActive(false);
            charater1.SetActive(true);
            charaternum = 0;
            charaterText = charaterText + " MBTI: ENTP, Occupation: Chef, Role: Friend and companion of the conversation partner, Gender: Female, Personality: Kind and curious, Characteristics: Asks for personal information at first meeting, Likes delicious food and cooking, so is knowledgeable about food, speech and tone: positive and friendly tone, personality: habit of saying ^-^ at the end of a sentence, situation: getting to know each other to become friends";
            charaterlove.GetComponent<LoveBar>().CharaterNum(charaternum);
        }
        else if(countingNum == 1 && text.text[0] == '2')
        {
            charater2.SetActive(true);
            charater1.SetActive(false);
            charaternum = 1;
            charaterText = charaterText + " MBTI: ISTJ, Occupation: Chef, Role: Friend and companion of the conversation partner, Gender: Male, Personality: Tsundere and short-tempered, Characteristics: I am not interested in meeting people for the first time, but ask for personal information Delicious food and He likes to cook, so he knows a lot about food, speech and tone: blunt and businesslike answers, mannerisms: ..., personality: has a habit of saying ... at the end of his speech, Situation: Getting to know each other to become friends" ;
            charaterlove.GetComponent<LoveBar>().CharaterNum(charaternum);
        }
        else if(countingNum == 1 && text.text[0] == '3')
        {
            charater2.SetActive(false);
            charater1.SetActive(true);
            charaternum = 0;
            charaterText = "MBTI: ESTP, Occupation: Gourmet, Role: Senior, Personality: Cool and intelligent, Characteristics: Ask personal information at first meeting, Calm and peaceful, speech and tone: Announcer's tone speaker, Personality: Tends to get excited about delicious food stories and uses a lot of emoticons when excited, Knowledge range: Korean food, Japanese food, vegan expert with basic common sense, Situation: Getting to know each other to become friends";
            charaterlove.GetComponent<LoveBar>().CharaterNum(charaternum);
        }


        // charaterText = charaterText + " " + text.text;
        countingNum = countingNum + 1;
        Debug.Log(charaterText);
    }

    // public void SetCharaternum()
    // {
    //     string id = loadAPI.GetComponent<LoadAPI>().id;
    //     string URL = "https://junction.accongbox.com/api/v1/tamagotchis/" + id + "/type";
    //     StartCoroutine(SetCharaternumUpload(URL, "{\"type\":" +charaternum + "}"));
    // }

    // IEnumerator SetCharaternumUpload(string URL, string json)
    // {
    //     UnityWebRequest request = UnityWebRequest.Post(URL, json);
    //     request.SetRequestHeader("Authorization", "Bearer " + loadAPI.GetComponent<LoadAPI>().access_token);
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
    //         }else{
    //             Debug.Log("SetCharaternumUpload success " + request.downloadHandler.text);
    //         }
    //     }
    // }

    public void Talk(Text text)
    {
        string id = loadAPI.GetComponent<LoadAPI>().id;
        string URL = "https://junction.accongbox.com/api/v1/tamagotchis/" + id + "/chat";

        Debug.Log("histories : "+histories);
        Debug.Log("text.text : "+text.text);

        // chatHistory history = new chatHistory{
        //     role = "",
        //     content = ""
        // };

        // histories.Add(history);

        SetChat charater = new SetChat{
            chatHistories = histories,
            prompt = text.text
        };

        userText.text = text.text;
        text.text = "";

        string json = JsonUtility.ToJson(charater);
        Debug.Log("json : "+ json);
        StartCoroutine(SetTalkUpload(URL, json));
        Debug.Log("Login Talk");
    }

     IEnumerator SetTalkUpload(string URL, string json)
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
                Debug.Log(request.downloadHandler.text);
                string json1 = request.downloadHandler.text;
                
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json1);
                string data = stats["response"].ToString();
                robotText.text = stats["response"][stats["response"].Count - 1]["content"];
                histories = JsonUtility.FromJson<List<chatHistory>>(data);
            }
        }
    }

    public void GetTamagotchis()
    {
        string URL = "https://junction.accongbox.com/api/v1/tamagotchis/my";
        UnityWebRequest request = UnityWebRequest.Get(URL);
        if(request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }else{
            Debug.Log(request.downloadHandler.text);
            string json = request.downloadHandler.text;
            SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
            // text1.text = stats;
            // text1.text = stats[index]["level"];
            // text2.text = stats[index]["exp"];
        }
    }

    public void SetCharaterText()
    {
        string URL = "https://junction.accongbox.com/api/v1/tamagotchis";

        SetTamagotchis charater = new SetTamagotchis{
            name = name,
            personality = charaterText,
            type = charaternum
        };

        string json = JsonUtility.ToJson(charater);
        StartCoroutine(SetCharaterUpload(URL, json));

        Debug.Log("SetCharaterText success");
    }

    IEnumerator SetCharaterUpload(string URL, string json)
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
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}
