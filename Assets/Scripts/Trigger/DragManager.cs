using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Zone
{
    public BlockType zoneType;
    public RectTransform rect;
}

public class DragManager : MonoBehaviour
{
    [SerializeField] private GameObject Lightbulb;
    [SerializeField] private GameObject TaskStation;
    [SerializeField] private GameObject TaskUiPanel;
    [SerializeField] private GameObject TriggerZone;

    [SerializeField] private RectTransform _defaultLayer;
    [SerializeField] private RectTransform _dragLayer;

    [SerializeField] private List<Zone> zones = new List<Zone>();

    [Header("Object replacement after completion")]
    [SerializeField] private GameObject objectToHide;
    [SerializeField] private GameObject objectToShow;

    public bool IsCompleted { get; private set; } = false;

    private Rect _boundingBox;
    private DragObject _currentDraggedObject;

    private Vector3[] _cornersA = new Vector3[4];
    private Vector3[] _cornersB = new Vector3[4];

    private void Awake()
    {
        SetBoundingBoxRect(_dragLayer);
    }

    public void RegisterDraggedObject(DragObject drag)
    {
        _currentDraggedObject = drag;
        drag.transform.SetParent(_dragLayer);
    }

    public void UnregisterDraggedObject(DragObject drag)
    {
        drag.transform.SetParent(_defaultLayer);
        _currentDraggedObject = null;
    }

    public bool IsWithinBounds(Vector2 position)
    {
        return _boundingBox.Contains(position);
    }

    private void SetBoundingBoxRect(RectTransform rectTransform)
    {
        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        var position = corners[0];

        Vector2 size = new Vector2(
            rectTransform.lossyScale.x * rectTransform.rect.size.x,
            rectTransform.lossyScale.y * rectTransform.rect.size.y);

        _boundingBox = new Rect(position, size);
    }

    // Called after every drag end
    public void CheckIfAllZonesFilled()
    {
        if (AreAllZonesFilled())
        {
            ValidateFinalPositions();
        }
    }

    private bool AreAllZonesFilled()
    {
        var drags = FindObjectsByType<DragObject>(FindObjectsSortMode.None);

        foreach (var zone in zones)
        {
            bool found = false;

            foreach (var drag in drags)
            {
                if (IsRectOverlapping(drag.GetComponent<RectTransform>(), zone.rect))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                return false;
        }

        return true;
    }

    private void ValidateFinalPositions()
    {
        bool allCorrect = true;
        var drags = FindObjectsByType<DragObject>(FindObjectsSortMode.None);

        foreach (var zone in zones)
        {
            foreach (var drag in drags)
            {
                if (IsRectOverlapping(drag.GetComponent<RectTransform>(), zone.rect))
                {
                    if (drag.blockType != zone.zoneType)
                    {
                        allCorrect = false;
                    }
                }
            }
        }

        if (allCorrect)
        {
            SnapAndLock(drags);
            CompleteTask();
        }
        else
        {
            ResetAll(drags);
        }
    }

    private void SnapAndLock(DragObject[] drags)
    {
        foreach (var zone in zones)
        {
            foreach (var drag in drags)
            {
                if (IsRectOverlapping(drag.GetComponent<RectTransform>(), zone.rect))
                {
                    drag.transform.position = zone.rect.position;
                    drag.locked = true;
                }
            }
        }
    }

    private void ResetAll(DragObject[] drags)
    {
        foreach (var drag in drags)
        {
            drag.ResetToStart();
        }
    }

    private void CompleteTask()
    {
        ReplaceObjects();

        TriggerZone.SetActive(false);
        TaskUiPanel.SetActive(false);

        Lightbulb.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.yellow);
        TaskStation.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);

        IsCompleted = true;
    }

    private void ReplaceObjects()
    {
        if (objectToHide != null)
            objectToHide.SetActive(false);

        if (objectToShow != null)
        {
            objectToShow.transform.position = objectToHide.transform.position;
            objectToShow.transform.rotation = objectToHide.transform.rotation;
            objectToShow.transform.localScale = objectToHide.transform.localScale;

            objectToShow.SetActive(true);
        }
    }

    private bool IsRectOverlapping(RectTransform a, RectTransform b)
    {
        a.GetWorldCorners(_cornersA);
        b.GetWorldCorners(_cornersB);

        Rect rectA = new Rect(_cornersA[0], _cornersA[2] - _cornersA[0]);
        Rect rectB = new Rect(_cornersB[0], _cornersB[2] - _cornersB[0]);

        return rectA.Overlaps(rectB);
    }
}
