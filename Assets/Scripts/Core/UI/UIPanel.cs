using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public abstract class UIPanel : MonoBehaviour
{
    protected CanvasGroup CanvasGroup { get; private set; }

    protected virtual void Awake() => CanvasGroup = GetComponent<CanvasGroup>();

    public virtual void Show()
    {
        CanvasGroup.alpha = 1.0f;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }

    public virtual void Hide()
    {
        CanvasGroup.alpha = 0.0f;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }
}