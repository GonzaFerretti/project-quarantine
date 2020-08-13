using UnityEngine;
using TMPro;

public class ActionPreviewer : MonoBehaviour
{
    public ModelPlayable modelPlayable;
    public ActionInteractPreviewWrapper actionPreviewer;
    public Vector3 threshold;

    Vector2 modelPositionOnScreen;

    private void Start()
    {
        actionPreviewer.text = GetComponent<TextMeshProUGUI>();
        actionPreviewer.SetAction();
    }

    private void Update()
    {
        //tentative
        if (!modelPlayable) modelPlayable = FindObjectOfType<ModelPlayable>(); 
        actionPreviewer.action.Do(modelPlayable);
        Movement();
    }

    void Movement()
    {
        modelPositionOnScreen = Camera.main.WorldToScreenPoint(modelPlayable.transform.position);
        Vector3 distance = modelPositionOnScreen - new Vector2(transform.position.x, transform.position.y);
        transform.position = new Vector3(transform.position.x + distance.x, transform.position.y + distance.y) + threshold;
    }
}
