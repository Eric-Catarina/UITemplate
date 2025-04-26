using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : EventTrigger
{


    // Play sound on hover
    public override void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager soundManager = GameObject.Find("GameManager").GetComponent<SoundManager>();
        soundManager.PlaySFX(9);
        
    }

}
