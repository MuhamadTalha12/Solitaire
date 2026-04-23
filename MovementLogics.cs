using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Solitaire
{
    public static class MovementLogics
    {
        static Stack<Card> temp = new Stack<Card>();
        public static void MoveCardToFoundation(Card c1)
        {
            if (Utils.IsCardPresentInTableaus(c1))
            {
                Tableau tab = Utils.GetTableauOfCard(c1);
                int cardNum = tab.GetCardPresentIndexInTableau(c1);
                int get = tab.GetCount() - cardNum;
                if (get == 1)
                {
                    foreach (Foundation foundation in Utils.foundations)
                    {
                        if (foundation.IsFoundationEmpty())
                        {
                            if (Utils.GetRankValue(c1.Rank) == 1)
                            {
                                Card poppedCard = tab.Pop();
                                if ((!tab.IsTableauEmpty()) && tab.Peek().IsFaceUp == false)
                                {
                                    tab.Peek().FlipCard();
                                }
                                foundation.Push(poppedCard);
                                poppedCard.BringToFront();
                                return;
                            }
                        }
                        else
                        {
                            Card c2 = foundation.Peek();
                            if (c2.Suit == c1.Suit)
                            {
                                if (Utils.GetRankValue(c1.Rank) == Utils.GetRankValue(c2.Rank) + 1)
                                {
                                    Card poppedCard = tab.Pop();
                                    if ((!tab.IsTableauEmpty()) && tab.Peek().IsFaceUp == false)
                                    {
                                        tab.Peek().FlipCard();
                                    }
                                    foundation.Push(poppedCard);
                                    poppedCard.BringToFront();
                                    return;
                                }
                            }
                        }
                    }

                }
            }
        }
        public static void MoveCardToTableaus(Card c1)
        {
            if (Utils.IsCardPresentInTableaus(c1))
            {
                Tableau tab = Utils.GetTableauOfCard(c1);
                int cardNum = tab.GetCardPresentIndexInTableau(c1);
                int get = tab.GetCount() - cardNum;
                /////
                if (get == 1)
                {
                    foreach (Tableau tableau in Utils.tableaus)
                    {
                        if (tableau.IsTableauEmpty())
                        {
                            if (Utils.GetRankValue(c1.Rank) == 13)
                            {
                                Card poppedCard = tab.Pop();
                                if ((tab.Peek() != null) && tab.Peek().IsFaceUp == false)
                                {
                                    tab.Peek().FlipCard();
                                }
                                tableau.Push(poppedCard);
                                poppedCard.BringToFront();
                                return;
                            }
                        }
                        else
                        {
                            Card c2 = tableau.Peek();
                            if (Utils.GetRankValue(c2.Rank) == Utils.GetRankValue(c1.Rank) + 1 && c1.Color != c2.Color)
                            {
                                Card poppedCard = tab.Pop();
                                if ((tab.Peek() != null) && tab.Peek().IsFaceUp == false)
                                {
                                    tab.Peek().FlipCard();
                                }
                                tableau.Push(poppedCard);
                                poppedCard.BringToFront();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    foreach (Tableau tableau in Utils.tableaus)
                    {
                        if (tableau.IsTableauEmpty())
                        {
                            if (Utils.GetRankValue(c1.Rank) == 13)
                            {
                                for(int i = 0; i < get; i++)
                                {
                                    if(i == get - 1 && tab.Count > 0)
                                    {
                                        Card nextCard = tab.Peek();
                                        if(nextCard != null  && nextCard.IsFaceUp == false)
                                        {
                                            
                                            nextCard.FlipCard();
                                            
                                        }
                                    }
                                    Card poppedCard = tab.Pop();
                                    temp.Push(poppedCard);
                                }
                                while (temp.Count > 0)
                                {
                                    Card card = temp.Pop();
                                    if (card != null)
                                    {
                                        tableau.Push(card);
                                        card.BringToFront();

                                    }
                                }
                                return;
                            }
                        }
                        else
                        {
                            Card c2 = tableau.Peek();
                            if(Utils.GetRankValue(c2.Rank) == Utils.GetRankValue(c1.Rank) + 1 && c1.Color != c2.Color)
                            {
                                int x = get;
                                for (int i = 0; i < x; i++)
                                {
                                    Card poppedCard = tab.Pop();

                                    if(i == x - 1 && tab.Count > 0)
                                    {
                                        Card nextCard = tab.Peek();
                                        if (nextCard != null && nextCard.IsFaceUp == false)
                                        {
                                            nextCard.FlipCard();
                                        }
                                    }
                                    temp.Push(poppedCard);
                                }
                                while (temp.Count > 0)
                                {
                                    Card card = temp.Pop();
                                    if (card != null)
                                    {
                                        tableau.Push(card);
                                        card.BringToFront();
                                    }
                                }
                                return;
                            }
                        }
                    }
                }
            }
        }
        ////////////////
        public static void WasteToFoundation(Card c1)
        {
            if (Utils.Waste.IsCardPresentInWaste(c1))
            {
                foreach (Foundation foundation in Utils.foundations)
                {
                    if (foundation.IsFoundationEmpty())
                    {
                        if (Utils.GetRankValue(c1.Rank) == 1)
                        {
                            Card poppedCard = Utils.Waste.Pop();
                            foundation.Push(poppedCard);
                            poppedCard.BringToFront();
                            return;
                        }
                    }
                    else
                    {
                        Card c2 = foundation.Peek();
                        if (c2.Suit == c1.Suit && Utils.GetRankValue(c1.Rank) == Utils.GetRankValue(c2.Rank) + 1)//Suppose c1 ka rank 1 and c2 ka rank 2
                        {
                            Card poppedCard = Utils.Waste.Pop();
                            foundation.Push(poppedCard);
                            poppedCard.BringToFront();
                            return;
                        }
                    }
                }
            }
        }
        ///////////
        public static void WasteToTableau(Card c1)
        {
            if (Utils.Waste.IsCardPresentInWaste(c1))
            {
                foreach(Tableau tableau in Utils.tableaus)
                {
                    if (tableau.IsTableauEmpty())
                    {
                        if(Utils.GetRankValue(c1.Rank) == 13)
                        {
                            Card pop = Utils.Waste.Pop();
                            tableau.Push(pop);
                            pop.BringToFront();
                            return;
                        }
                    }
                    else
                    {
                        Card c2 = tableau.Peek();
                        if(Utils.GetRankValue(c1.Rank) + 1 == Utils.GetRankValue(c2.Rank) && c1.Color!= c2.Color)
                        {
                            Card pop = Utils.Waste.Pop();
                            tableau.Push(pop);
                            pop.BringToFront();
                            return;
                        }
                    }
                }
            }

        }
    }


}