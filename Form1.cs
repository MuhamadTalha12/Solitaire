using Solitaire.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Solitaire
{
    public partial class Form1 : Form
    {
        private List<Card> cards = new List<Card>();
        private List<PictureBox> tableaupbX = new List<PictureBox>();
        private List<PictureBox> FoundationpbX = new List<PictureBox>();


        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Green;
            CardDeck cardDeck = new CardDeck();
            cards = cardDeck.GetCardsList();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Stock currStock = Utils.Stock;
            Waste currWaste = Utils.Waste;

            if (currStock.GetCount() > 0)
            {
                Card dequeuedCard = currStock.Dequeue();

                currWaste.Push(dequeuedCard);
                //Push k baad flip karwaya kyunke wo facedown tha
                dequeuedCard.FlipCard();
                this.Controls.Add(dequeuedCard);
                dequeuedCard.BringToFront(); ;
            }
            else
            {
                RefillStockPile();
            }
        }
        private void RefillStockPile()
        {
            Waste currWaste = Utils.Waste;
            Stock currStock = Utils.Stock;
            Stack<Card> tempStack = new Stack<Card>();
            if (currStock.IsStockEmpty())
            {
                while (currWaste.GetCount() > 0)
                {
                    tempStack.Push(currWaste.Pop());
                }
                while (tempStack.Count() > 0)
                {
                    Card poppedCard = tempStack.Pop();
                    currStock.Enqueue(poppedCard);
                    poppedCard.FlipCard();
                    this.Controls.Add(poppedCard);
                    poppedCard.BringToFront();
                }
            }
        }




        private void MakeWastePile()
        {
            Utils.Waste = new Waste();
            PictureBox pbx = MakeEmptyPBX(550, 50);
        }
        private void MakeStockPile()
        {
            Utils.Stock = new Stock();
            PictureBox pbx = MakeEmptyPBX(400, 50);
            int leftCards = cards.Count;
            for (int i = 0; i < leftCards; i++)
            {
                if (cards.Count > 0)
                {
                    Utils.Stock.Enqueue(cards[0]);
                    cards[0].DisplayBackCard();
                    this.Controls.Add(cards[0]);
                    cards[0].BringToFront();
                    cards.RemoveAt(0);
                }
            }
        }
        private PictureBox MakeEmptyPBX(int offsetX, int offsetY)
        {
            PictureBox Pbx = new PictureBox();
            Pbx.Location = new Point(offsetX, offsetY);
            Pbx.Size = new Size(80, 120);
            Pbx.BackColor = Color.White;
            Pbx.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(Pbx);
            this.BringToFront();
            ////////////////////////////
            return Pbx;
        }
        private void MakeStackTableaus()
        {
            Utils.AddTableaus();
            CreateEmptyTableau();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (cards.Count > 0)
                    {
                        Utils.tableaus[i].Push(cards[0]);
                        if (j == i)
                        {
                            cards[0].DisplayFrontCard();
                        }
                        else
                        {
                            cards[0].DisplayBackCard();
                        }
                        this.Controls.Add(cards[0]);
                        cards[0].BringToFront();
                        cards.RemoveAt(0);
                    }
                }
            }

        }

        public void CreateEmptyTableau()
        {
            int offsetX = 400;
            int offsetY = 400;
            for (int i = 0; i < 7; i++)
            {
                PictureBox Pbx = MakeEmptyPBX(offsetX, offsetY);
                ///List me add
                tableaupbX.Add(Pbx);
                offsetX += 150;
            }
        }
        public void MakeStackFoundation()
        {
            Utils.AddFoundations();
            ShowEmptyFoundation();
        }
        private void ShowEmptyFoundation()
        {
            int offsetX = 850;
            int offsetY = 50;
            for (int i = 0; i < 4; i++)
            {
                PictureBox Pbx = MakeEmptyPBX(offsetX, offsetY);
                FoundationpbX.Add(Pbx);
                offsetX += 150;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MakeStackTableaus();
            MakeStockPile();
            MakeWastePile();
            MakeStackFoundation();
        }

        


        

        private void Instructions_Click(object sender, EventArgs e)
        {
            string instructions = "Welcome to the Solitaire Game!\n\n" +
                           "1. **Objective of the Game**:\n " + "                            The goal is to move all cards to the foundation piles, organized by suit in ascending order, from Ace to King.\n\n" +
                           "2. **Tableau Piles**:\n" +
                           "                     The tableau consists of 7 piles of cards.\n" +
                           "                     Cards in the tableau must be arranged in descending order and alternating colors.\n" +
                           "                     Example: A red 9 (hearts or diamonds) should be placed on a black 10 (spades or clubs), and a black 8 (spades or clubs) should be placed on a red 9 (hearts or diamonds).\n\n" +
                           "3. **Foundation Piles**:\n" +
                           "                        There are 4 foundation piles, one for each suit (hearts, diamonds, clubs, spades).\n" +
                           "                        Cards must be placed in ascending order starting from Ace to King.\n" +
                           "                        The cards must be sorted by their suit, with the Ace being the first card in each pile and proceeding through to the King.\n" +
                           " Example: A red Ace (hearts or diamonds) should start the foundation, followed by a red 2 (hearts or diamonds), and so on.\n\n" +
                           "4. **Stock Pile**:\n" +
                           "                 The stock pile contains the remaining cards not in play.\n" +
                           "                 You can draw cards from the stock pile when no moves are possible from the tableau or foundation piles.\n" +
                           "                 Once the stock is empty, you can reload it (if the game allows) by drawing from the waste pile or starting from the top of the tableau again.\n\n" +
                           "5. **Moving Cards**:\n" +
                           "                    Cards can be moved between tableau piles and to foundation piles.\n" +
                           "                    To move a card to a tableau pile, ensure it adheres to the alternating color rule and is placed in descending order.\n" +
                           "                    To move a card to a foundation pile, ensure it matches the suit and follows the ascending order (starting with the Ace).\n\n" +
                           "6. **King Placement**:\n" +
                           "                     A King can be placed in an empty tableau pile, and it must start the sequence in descending order.\n" +
                           "                     After placing a King, you can continue to place cards on top of it in alternating colors and descending order.\n\n" +
                           "7. **Winning the Game**:\n" +
                           "                     You win when all the cards are moved to the foundation piles, with each pile being correctly ordered from Ace to King in their respective suits.\n\n" +
                           "8. **Special Moves**:\n" +
                           "                     If you double-click on a tableau card, it may automatically move to the foundation if it's the correct next card for that suit.\n\n" +
                           "\n\n" +
                           "**Card Movement on Double-Click**:\n" +
                           "                                 If a card is not manually dragged or dropped, it will automatically check the position and move to the appropriate tableau or foundation pile on a double-click, if it fits.\n";

            MessageBox.Show(instructions, "Game Instructions", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
