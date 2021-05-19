using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropInfAnsw : MonoBehaviour, IDropHandler
{

    public bool isGoodPlace = false;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position;
            eventData.pointerDrag.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 30);

            if (eventData.pointerDrag.gameObject.GetComponent<TextMeshProUGUI>().text.Equals(InfMinigame.infMinigame.infTasks[InfMinigame.currTask].needed[Int32.Parse(gameObject.name)-1].correct))
            {
                isGoodPlace = true;
            }else
            {
               isGoodPlace = false;
            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
