using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite doorOpenVertical;
    [SerializeField] private Sprite doorCloseVertical;
    [SerializeField] private Sprite doorOpenHorizontal;
    [SerializeField] private Sprite doorCloseHorizontal;

    [SerializeField] private bool isOpen = false;
    [SerializeField] private DoorOrientation doorOrientation = DoorOrientation.Vertical;

    [Header("Conditions")]
    [SerializeField] private DoorCondition[] doorConditions;

    [Header("Callbacks")]
    public UnityEvent OnStepWrong;

    private SpriteRenderer _doorSpriteCurrent;

    public void TestConditonsToOpen()
    {
        if(doorConditions.Length > 0 && !isOpen)
        {
            bool condition = false;

            for(var i = 0; i < doorConditions.Length; i++)
            {
                if(doorConditions[i].condition != doorConditions[i].lever?.GetState())
                {
                    condition = false;
                    break;
                }

                condition = true;
            }

            if(condition)
                Open();
        }
    }

    private void Open()
    {
        isOpen = !isOpen;
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

    private void FixedUpdate()
    {
        TestConditonsToOpen();
    }

    private void Start()
    {
        DoorRender();
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

[System.Serializable]
public class DoorCondition
{
    public Lever lever;
    public bool condition;
}

enum DoorOrientation
{
    Vertical,
    Horizontal
}
