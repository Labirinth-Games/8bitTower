using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime;

public class Door : MonoBehaviour, IBlackBoxOutput
{
    [Header("References")]
    [SerializeField] private Sprite doorOpenVertical;
    [SerializeField] private Sprite doorCloseVertical;
    [SerializeField] private Sprite doorOpenHorizontal;
    [SerializeField] private Sprite doorCloseHorizontal;

    [SerializeField] private bool isOpen = false;
    [SerializeField] private DoorOrientation doorOrientation = DoorOrientation.Vertical;

    private SpriteRenderer _doorSpriteCurrent;

    public void Unlock()
    {
        if (isOpen) return;

        isOpen = true;
        GetComponent<Collider2D>().enabled = false;
        DoorRender();
    }

    private void CollisionAdapter()
    {
        if(doorOrientation == DoorOrientation.Vertical)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(.25f, 1f);
            GetComponent<BoxCollider2D>().offset = new Vector2(.25f, 0f);
        }

        if (doorOrientation == DoorOrientation.Horizontal)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
            GetComponent<BoxCollider2D>().offset = Vector2.zero;
        }
    }

    private void DoorRender()
    {
        if (isOpen && doorOrientation == DoorOrientation.Vertical)
        {
            _doorSpriteCurrent.sprite = doorOpenVertical;
        }
        else if (!isOpen && doorOrientation == DoorOrientation.Vertical)
        {
            _doorSpriteCurrent.sprite = doorCloseVertical;
        }
        else if (isOpen && doorOrientation == DoorOrientation.Horizontal)
        {
            _doorSpriteCurrent.sprite = doorOpenHorizontal;
        }
        else if (!isOpen && doorOrientation == DoorOrientation.Horizontal)
        {
            _doorSpriteCurrent.sprite = doorCloseHorizontal;
        }
    }

    private void OnValidate()
    {
        if(_doorSpriteCurrent == null)
        {
            _doorSpriteCurrent = gameObject.GetComponentInChildren<SpriteRenderer>();
        }
        
        DoorRender();
        CollisionAdapter();
    }
}

enum DoorOrientation
{
    Vertical,
    Horizontal
}
