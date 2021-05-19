using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class PuzzleDragScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    //private SortingGroup sortingGroup;

    public bool isGoodPlace = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.8f;
        //sortingGroup.sortingOrder = 1;
        isGoodPlace = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        //sortingGroup.sortingOrder = 0;
        if (GeoMinigame.geoMinigame.IsGameEnd())
        {
            GeoMinigame.geoMinigame.SumUpMinigame();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //sortingGroup = GetComponent<SortingGroup>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetPuzzleDragable(bool activity)
    {

        GetComponent<CanvasGroup>().blocksRaycasts = activity;
    }
}
