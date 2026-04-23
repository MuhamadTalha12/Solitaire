using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class Node
    {
        public Card Card;
        public Node Next;
        public Node(Card card)
        {
            this.Card = card;
            Next = null;
        }
    }
}
