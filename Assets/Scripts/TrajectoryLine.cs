using UnityEngine;
using UnityEngine.UI.Extensions;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField] private UILineRenderer uiLine;
    public RectTransform dynamicStartPoint; // текущая позиция кота в UI
    [SerializeField] private float trajectoryLength = 300f;

    private bool isAiming = false;

    void Update()
    {
        if (isAiming && dynamicStartPoint != null)
        {
            UpdateLine();
        }
        else
        {
            uiLine.enabled = false;
        }
    }

    public void StartAiming()
    {
        isAiming = true;
        uiLine.enabled = true;
    }

    public void StopAiming()
    {
        isAiming = false;
        uiLine.enabled = false;
    }

    private void UpdateLine()
    {
        Vector2 start = dynamicStartPoint.anchoredPosition;
        Vector2 end = start + Vector2.down * trajectoryLength;

        uiLine.Points = new Vector2[] { start, end };
        uiLine.SetAllDirty();
    }
}