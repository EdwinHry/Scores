using System;
using Xamarin.Forms;

namespace Scores
{
    public class Player
    {
        private string name;
        private int points;
        private Label label_Name;
        private Label label_Points;
        private Entry entry_Points;
        private Button button_addPoints;
        private Button button_decPoints;

        //CONSTRUCTORS
        public Player()
        {
        }

        public Player(string t_name, int t_points)
        {
            this.name = t_name;
            this.points = t_points;
        }


        //GETTERS/SETTERS
        public void set_name(string t_name)
        {
            this.name = t_name;
        }

        public string get_name()
        {
            return this.name;
        }

        public void set_points(int t_points)
        {
            this.points = t_points;
        }

        public int get_points()
        {
            return this.points;
        }

        public void set_label_Name(Label t_label)
        {
            this.label_Name = t_label;
        }

        public Label get_label_Name()
        {
            return this.label_Name;
        }

        public void set_label_Points(Label t_label)
        {
            this.label_Points = t_label;
        }

        public Label get_label_Points()
        {
            return this.label_Points;
        }

        public void set_entry_Points(Entry t_entry)
        {
            this.entry_Points = t_entry;
        }

        public Entry get_entry_Points()
        {
            return this.entry_Points;
        }

        public void set_button_addPoints(Button t_button)
        {
            this.button_addPoints = t_button;
        }

        public Button get_button_addPoints()
        {
            return this.button_addPoints;
        }

        public void set_button_decPoints(Button t_button)
        {
            this.button_decPoints = t_button;
        }

        public Button get_button_decPoints()
        {
            return this.button_decPoints;
        }

        //METHODS

        public void Button_AddPoints_Clicked(object sender, EventArgs e)
        {
            if(this.entry_Points.Text == null)
            {
                this.entry_Points.Placeholder = "Must not be empty !";
            }
            else
            {
                this.addPoints(this.get_entry_Points_Text());
                this.label_Points.Text = this.get_points().ToString();
                this.entry_Points.Text = null;
                this.entry_Points.Placeholder = null;
            }

        }

        public void Button_DecPoints_Clicked(object sender, EventArgs e)
        {
            if (this.entry_Points.Text == null)
            {
                this.entry_Points.Placeholder = "Must not be empty !";
            }
            else
            {
                this.decPoints(this.get_entry_Points_Text());
                this.label_Points.Text = this.get_points().ToString();
                this.entry_Points.Text = null;
                this.entry_Points.Placeholder = null;
            }
        }

        public int get_entry_Points_Text()
        {
            return Convert.ToInt16(this.entry_Points.Text);
        }

        public void addPoints(int t_points)
        {
            this.points += t_points;
        }

        public void decPoints(int t_points)
        {
            this.points -= t_points;
        }
    }
}
