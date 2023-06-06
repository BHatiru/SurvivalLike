using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHandler : MonoBehaviour
{
    private Image icon;
    void Start()
    {
        icon = GetComponent<Image>();
    }

    //pass the sprite to the icon
    public void SetIcon(Sprite sprite){
        icon.sprite = sprite;
    }
}
