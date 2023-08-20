using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickCooks : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[5];
    public GameObject imageObj;
    private Image img;

    // Start is called before the first frame update
   public void Change()
   {
        img = imageObj.GetComponent<Image>();
        int num = Random.Range(0,5);
        img.sprite = sprites[num];
   }
}
