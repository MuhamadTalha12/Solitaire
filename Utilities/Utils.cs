using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solitaire
{
    internal static class Utils
    {
        public static int card_width = 80;
        public static int card_height = 120;
        public static string cards_path = "C:\\Users\\PMLS\\Desktop\\Current Study\\Mid Proj\\Solitaire\\Images\\";


        private static Dictionary<string, int> rankToInt = new Dictionary<string, int>
        { { "2", 2 },{ "3", 3 },{ "4", 4 },{ "5", 5 },{ "6", 6 },{ "7", 7 },{ "8", 8 },{ "9", 9 },{ "10", 10 },
          { "A", 1 }, { "J", 11 },{ "Q", 12 },{ "K", 13 }
        };

        public static int GetRankValue(string rank)
        {
            return rankToInt[rank];
        }


        public static List<Tableau> tableaus = new List<Tableau>();
        public static List<Foundation> foundations = new List<Foundation>();
        public static Stock Stock;
        public static Waste Waste;
        public static void AddFoundations()
        {
            int offsetX = 850;
            int offsetY = 50;
            for (int i = 0; i < 4; i++)
            {
                offsetY = 50;
                Foundation foundation = new Foundation(offsetX,offsetY);
                foundations.Add(foundation);
                offsetX += 150;
            }
        }
        public static void AddTableaus()
        {
            int offsetX = 400;
            int offsetY = 400;
            for (int i = 0; i < 7; i++)
            {
                offsetY = 400;
                Tableau tableau = new Tableau(offsetX, offsetY);
                tableaus.Add(tableau);
                offsetX += 150;
            }
        }

        public static bool IsCardPresentInTableaus(Card card)
        {
            for (int i = 0; i < tableaus.Count; i++)
            {
                if (tableaus[i].IsCardPresentInTableau(card))
                {
                    return true;
                }
            }
            return false;
        }
        public static Tableau GetTableauOfCard(Card card)
        {
            for (int i = 0; i < tableaus.Count; i++)
            {
                if (tableaus[i].IsCardPresentInTableau(card))
                {
                    //Tableau ka number return krwa dega
                    return tableaus[i];
                }
            }
            return null;
        }
        public static int GetCardNo(Card card)
        {
            Tableau temp = GetTableauOfCard(card);
            if (temp != null)
            {
                int tempNo = temp.GetCardPresentIndexInTableau(card);
                if (tempNo != -1)
                {
                    int getNo = temp.GetCount() - tempNo;
                    return getNo;
                }
            }
            return -1;
        }
        public static bool IsGameWon()
        {
            foreach (Foundation foundation in foundations)
            {
                if (foundation.GetCount() == 0 || GetRankValue(foundation.Peek().Rank) != 13)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
