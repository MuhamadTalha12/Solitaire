using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Solitaire
{
    public class Waste
    {
        public Node Head;
        public int xPosition = 550;
        public int yPosition = 50;
        public int Count;

        public Waste()
        {
            this.Head = null;
            this.Count = 0;
        }
        public void Push(Card card)
        {
            card.UpdatePosition(xPosition, yPosition);
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
            if (IsWasteEmpty())
            {
                return null;
            }
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
            if (IsWasteEmpty())
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

        public bool IsCardPresentInWaste(Card card)
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
        public int GetCardPresentIndexInWaste(Card card)
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

        public bool IsWasteEmpty()
        {
            return Head == null;
        }

        public int GetCount()
        {
            return Count;
        }
    }
}
