using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CardVisuals : MonoBehaviour
{
    public Card card;
    
    [Header("References")]
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text cost;
    public SpriteRenderer icon;
    
    [Header("State")]
    public Transform target;
    public Transform slot;
    public bool isHovered;
    public bool isDragged;
    public bool isSelected;
    SortingGroup sortingGroup;

    private void Start()
    {
        sortingGroup = GetComponent<SortingGroup>();
        target = slot;
        Init(card);
    }

    public void Init(Card card)
    {
        this.card = card;
        title.text = card.title;
        description.text = card.description;
        cost.text = card.cost.ToString();
        icon.sprite = card.image;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) && isDragged)
        {
            Drop();
        }
        
        transform.position = Vector3.Lerp(transform.position, target.position, 0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, 0.1f);
        
        transform.localScale = Vector3.Lerp(transform.localScale, isHovered || isDragged || isSelected ? Vector3.one * 1.5f : Vector3.one, 0.1f);
    }

    private void OnMouseDown()
    {
        Pick();
    }

    private void OnMouseEnter()
    {
        isHovered = true;
    }

    private void OnMouseExit()
    {
        isHovered = false;
    }

    public void Pick()
    {
        isDragged = true;
        sortingGroup.sortingOrder = 1;

        if (card.canChooseTarget)
        {
            // draw line
            isSelected = true;
            FindObjectOfType<SelectionLine>().startPoint = transform;
            FindObjectOfType<SelectionLine>().endPoint = WorldCursor.Inst.transform;
        }
        else
        {
            target = WorldCursor.Inst.transform;
        }
    }

    public void Drop()
    {
        isDragged = false;
        target = slot;
        sortingGroup.sortingOrder = 0;

        if (card.canChooseTarget)
        {
             // point cast
             FindObjectOfType<SelectionLine>().startPoint = GameObject.Find("OutOfScreen").transform;
             FindObjectOfType<SelectionLine>().endPoint = GameObject.Find("OutOfScreen").transform;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            isSelected = false;
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform.gameObject.name == "Enemy")
                {
                    Use(hit.transform.gameObject.GetComponent<Entity>());
                }
            }
        }
    }

    public void Use(Entity target)
    {
        card.Use(GameObject.Find("Player").GetComponent<Entity>(),target);
        FindObjectOfType<CardHandVisuals>().RemoveCard(this);
        Destroy(gameObject);
    }
}
