using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solitaire
{
    public partial class Card : UserControl
    {
        public string Suit;
        public string Rank;
        public string image;
        public string BackImage;
        public bool IsFaceUp;
        public string Color;
        public int width;
        public int height;

        public Card(string suit, string rank)
        {
            InitializeComponent();
            width = 80;
            height = 120;
            this.Suit = suit;
            this.Rank = rank;
            this.image = GetCardImage(suit, rank);
            this.BackImage = GetCardBackImage();
            this.IsFaceUp = false;
            this.Color = SetColor();

            pic.DoubleClick += Pic_DoubleClick;
        }

        public void UpdatePosition(int X, int Y)
        {
            this.Location = new Point(X, Y);
        }

        public void FlipCard()
        {
            if(IsFaceUp)
            {
                DisplayBackCard();
            }
            else
            {
                DisplayFrontCard();
            }

        }
        private string SetColor()
        {
            if (Suit == "Spades" || Suit == "Clubs")
            {
                return "Black";
            }
            else
            {
                return "Red";
            }
        }

        public void DisplayFrontCard()
        {
            Image frontImage = Image.FromFile(image);
            IsFaceUp = true;
            pic.Image = frontImage;
            pic.Height = height;
            pic.Width = width;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void DisplayBackCard()
        {
            Image backImage = Image.FromFile(BackImage);
            IsFaceUp = false;
            pic.Image = backImage;
            pic.Height = height;
            pic.Width = width;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private string GetCardImage(string suit, string rank)
        {
            return $"{Utils.cards_path}Card{suit}{rank}.png";
        }
        private string GetCardBackImage()
        {
            return $"{Utils.cards_path}cardBack.png";
        }
        public override string ToString()
        {
            return $"Card{Suit}{Rank}";
        }
        private void pic_Click(object sender, EventArgs e)
        {
        }

        private void Pic_DoubleClick(object sender, EventArgs e)
        {
            if (IsFaceUp)
            {
                MovementLogics.MoveCardToFoundation(this);
                MovementLogics.MoveCardToTableaus(this);
                MovementLogics.WasteToFoundation(this);
                MovementLogics.WasteToTableau(this);
                if (Utils.IsGameWon())
                {
                    Form form = new Win();
                    form.Show();
                    //MessageBox.Show("Congratulations! You have won the game!","Victory",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }

        }
    }
}
