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
    public class Foundation
    {
        public Node Head;
        public int xPosition, yPosition;
        public int Count;

        public Foundation(int x, int y)
        {
            xPosition = x;
            yPosition = y;
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
            if (IsFoundationEmpty())
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
            if (IsFoundationEmpty())
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

        public bool IsFoundationEmpty()
        {
            return Head == null;
        }
        public int GetCount()
        {
            return Count;
        }

    }
}