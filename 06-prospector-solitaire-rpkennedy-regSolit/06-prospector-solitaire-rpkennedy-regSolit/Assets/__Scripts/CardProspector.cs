using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCardState
{
    drawpile,
    tableau,
    target,
    discard
}

public class CardProspector : Card
{
    [Header("Set Dynamically: CardProspector")]
    public eCardState state = eCardState.drawpile;
    public List<CardProspector> hiddenBy = new List<CardProspector>();
    public int layoutID;
    public SlotDef slotDef;

    private string archiveLayer;
    private int archiveOrder;
    private Vector3 archivePos;

    private void OnMouseDown()
    {
        archiveLayer = this.GetComponent<SpriteRenderer>().sortingLayerName;
        archiveOrder = this.GetComponent<SpriteRenderer>().sortingOrder;
        archivePos = this.transform.position;
    }

    override public void OnMouseDrag()
    {
        if (!this.faceUp) return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

        SetSortingLayerName("Draw");
        SetSortOrder(5);
        this.transform.position = new Vector3(mousePos.x, mousePos.y, -10);
    }
    public void OnMouseUp()
    {
        Debug.Log("released");

        Ray ray = new Ray(this.transform.position, Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("hit");
            Target target = hit.collider.gameObject.GetComponent<Target>();

            Debug.Log(hit.collider.gameObject);

            if (this.suit == target.suit)
            {
                Debug.Log("released on correct target");
                target.NestCard(this);
            }
            else
            {
                SetSortingLayerName(archiveLayer);
                SetSortOrder(archiveOrder);
                this.transform.position = archivePos;
                Debug.Log("reset 1");
            }
        }
        else
        {
            SetSortingLayerName(archiveLayer);
            SetSortOrder(archiveOrder);
            this.transform.position = archivePos;
            Debug.Log("reset 2");
        }




        
        
    }
}
