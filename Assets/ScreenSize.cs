using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSize : MonoBehaviour
{
    // private void Awake()
    // {
    //     // SafeArea Settings
    //     RectTransform safeArea = Screen.safeArea;
    //     float newAnchorMin = safeArea.position;
    //     float newAnchorMax = safeArea.position + safeArea.size;
    //     newAnchorMin.x /= Screen.width;
    //     newAnchorMax.x /= Screen.width;
    //     newAnchorMin.y /= Screen.height;
    //     newAnchorMax.y /= Screen.height;
    //     RectTransform rect = this.safeArea.GetComponent<RectTransform>();
    //     rect.anchorMin = newAnchorMin;
    //     rect.anchorMax = newAnchorMax;
    // }

    private void Awake() {

        RectTransform rt = GetComponent<RectTransform>();
        Rect safeArea = Screen.safeArea;
        Vector2 minAnchor = safeArea.position;
        Vector2 maxAnchor = minAnchor + safeArea.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        rt.anchorMin = minAnchor;
        rt.anchorMax = maxAnchor;
    }

}
