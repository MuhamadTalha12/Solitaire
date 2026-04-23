using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solitaire
{
    public class Tableau
    {
        public Node Head;
        public int xPosition, yPosition;
        public int Count;
        private int Offset;
        private int Margin;

        public Tableau(int x, int y)
        {
            xPosition = x;
            yPosition = y;
            Offset = 30;
            Margin = 0;
            this.Head = null;
            this.Count = 0;
        }

        public void Push(Card card)
        {
            card.UpdatePosition(xPosition, yPosition + Margin);
            Margin += Offset;
            Node newNode = new Node(card);
            if (Head == null)
            {
                Head = newNode;
            }
            else
            {
                //End tk jayega or ye insert bi end pr krega
                Node currentNode = Head;
                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                currentNode.Next = newNode;
            }
            Count++;
        }

        public Card Pop()
        {
            if (IsTableauEmpty())
            {
                return null;
            }
            Margin -= Offset;
            //incase 1 hi node hay or uska next null ha 
            if (Head.Next == null)
            {
                Card toPop = Head.Card;
                Head = null;
                Count--;
                return toPop;
            }

            //Agar zyada nodes hain too..
            Node currentNode = Head;
            while (currentNode.Next.Next != null)
            {
                currentNode = currentNode.Next;
            }

            Card toPopCard = currentNode.Next.Card;
            currentNode.Next = null;
            Count--;

            return toPopCard;
        }

        public Card Peek()
        {
            if (IsTableauEmpty())
            {
                return null;
            }

            Node currentNode = Head;
            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            }
            return currentNode.Card;
        }

        public bool IsCardPresentInTableau(Card card)
        {
            Node currentNode = Head;
            while (currentNode != null)
            {
                if (currentNode.Card == card)
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }
        public int GetCardPresentIndexInTableau(Card card)
        {
            int index = 0;
            Node currentNode = Head;
            while (currentNode != null)
            {
                if (currentNode.Card == card)
                {
                    return index;
                }
                currentNode = currentNode.Next;
                index++;
            }
            return -1;
        }

        public bool IsTableauEmpty()
        {
            return Head == null;
        }

        public int GetCount()
        {
            return Count;
        }
    }
}
