using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private InventoryItem item;


    public void Awake() {
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<InventoryItem>();
    }

    public void SetData(Sprite sprite) {
        item.SetData(sprite);
    }

    void Update() {
        MousePosition();
    }

    public void Toggle(bool val) {
        // Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }

    public Vector2 MousePosition() {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out position
                );
        transform.position = canvas.transform.TransformPoint(position);
        return transform.position;
    }
}
