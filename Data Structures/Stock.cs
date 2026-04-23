using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class Stock
    {
        private Node Front;
        private Node Rear;
        private int Count;
        private int xPosition = 400;
        private int yPosition = 50;
        public Stock()
        {
            Front = null;
            Rear = null;
            Count = 0;
        }
        public void Enqueue(Card card)
        {
            card.UpdatePosition(xPosition, yPosition);
            Node newNode = new Node(card);
            if (Rear == null)
            {
                Front = Rear = newNode;
            }
            else
            {
                Rear.Next = newNode;
                Rear = newNode;
            }
            Count++;
        }
        public Card Dequeue()
        {
            if (IsStockEmpty())
            {
                return null;
            }
            Card toDequeue = Front.Card;
            Front = Front.Next;
            if (IsStockEmpty())
            {
                Rear = null;
            }
            Count--;
            //xPosition += 150;
            return toDequeue;
        }
        public Card PeekFront()
        {
            if (IsStockEmpty())
            {
                return null;
            }
            return Front.Card;
        }
        public Card PeekRear()
        {
            if (Rear == null)
            {
                return null;
            }
            return Rear.Card;
        }
        public bool IsStockEmpty()
        {
            return Front == null;
        }
        public int GetCount()
        {
            return Count;
        }
    }
}
