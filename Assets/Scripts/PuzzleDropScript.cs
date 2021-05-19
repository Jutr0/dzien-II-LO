using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleDropScript : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            if (eventData.pointerDrag.gameObject.name.Equals("Puzzle " + gameObject.name))
            {
                eventData.pointerDrag.GetComponent<PuzzleDragScript>().isGoodPlace = true;
            }
            else
            {
                eventData.pointerDrag.GetComponent<PuzzleDragScript>().isGoodPlace = false;
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
