using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("What's good:")]
    public string suit;

    [Header("nah")]
    public List<CardProspector> stack;
    public bool isFull;

    
    void Start()
    {    
        isFull = false;
        stack = new List<CardProspector>();
    }
                        
    void Update()
    {
        if (stack.Count == 13 && isFull == false) isFull = true;
    }

    public void NestCard(CardProspector card)
    {
        if(stack.Count == 0)
        {
            if(card.rank == 1)
            {
                Debug.Log("stick 1");
                card.transform.position = this.transform.position;
                stack.Add(card);
            }
            else
            {
                Debug.Log("reset target 1");
                card.cardReset();
            }
        }
        else if(stack[stack.Count - 1].rank == card.rank - 1)
        {
            Debug.Log("stick 2");
            card.transform.position = this.transform.position;
            stack.Add(card);
        }
        else
        {
            Debug.Log("reset target");
            card.cardReset();
        }
    }
}
