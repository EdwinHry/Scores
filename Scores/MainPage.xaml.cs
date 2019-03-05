using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Scores
{
    public partial class MainPage : ContentPage
    {
        public static StackLayout[] ArrStackLayoutPlayers;
        public MainPage()
        {
            InitializeComponent();
            ArrStackLayoutPlayers = new StackLayout[] { };
        }

        public StackLayout AddPlayerLayout(Player t_player)
        {
            t_player.set_label_Name(
                new Label()
                {
                    Text = t_player.get_name(),
                    FontSize = 30,
                    HorizontalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Center
                }
            );

            t_player.set_label_Points(
                new Label()
                {
                    Text = t_player.get_points().ToString(),
                    FontSize = 20,
                    HorizontalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Center
                }
            );

            t_player.set_entry_Points(
                new Entry()
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 200,
                    Keyboard = Keyboard.Numeric
                }
            );

            t_player.set_button_addPoints(
                new Button()
                {
                    Text = "Add",
                    HorizontalOptions = LayoutOptions.Start,
                    WidthRequest = 100,
                }
            );
            t_player.get_button_addPoints().Clicked += t_player.Button_AddPoints_Clicked;

            t_player.set_button_decPoints(
                new Button()
                {
                    Text = "Dec",
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 100,
                }
            );
            t_player.get_button_decPoints().Clicked += t_player.Button_DecPoints_Clicked;

            Grid gridPlayer = new Grid();

            gridPlayer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridPlayer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Grid gridEntry = new Grid();

            gridEntry.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Grid gridButtons = new Grid();

            gridButtons.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridButtons.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            gridPlayer.Children.Add(t_player.get_label_Name(),0,0);
            gridPlayer.Children.Add(t_player.get_label_Points(), 1, 0);
            gridEntry.Children.Add(t_player.get_entry_Points(), 0, 0);
            gridButtons.Children.Add(t_player.get_button_addPoints(), 0, 0);
            gridButtons.Children.Add(t_player.get_button_decPoints(), 1, 0);

            StackLayout stacklayoutPlayer = new StackLayout()
            {
                Padding = 10,
                BackgroundColor = Color.Beige
            };
            stacklayoutPlayer.Children.Add(gridPlayer);
            stacklayoutPlayer.Children.Add(gridEntry);
            stacklayoutPlayer.Children.Add(gridButtons);

            return stacklayoutPlayer;

        }

        void AddPlayer_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                int error = 0;
                Array.Resize(ref App.ArrayPlayer, App.ArrayPlayer.Length + 1);
                int ArrayPlayerSize = App.ArrayPlayer.Length; //just using a shorter var
                App.ArrayPlayer[ArrayPlayerSize - 1] = new Player();
                App.ArrayPlayer[ArrayPlayerSize - 1].set_name(null);
                if (this.entry_PlayerName.Text == null || this.entry_PlayerName.Text.Trim() == "")
                {
                    error = 1;
                }
                else
                {
                    for (int x = 0; x < ArrayPlayerSize; x++)
                    {
                        if (this.entry_PlayerName.Text == App.ArrayPlayer[x].get_name())
                        {
                            error = 2;
                            break;
                        }
                    }
                }

                if(error == 0)
                {
                    App.ArrayPlayer[ArrayPlayerSize - 1].set_name(this.entry_PlayerName.Text);
                    this.entry_PlayerName.Text = null;
                    this.entry_PlayerName.Placeholder = "Enter Player Name";

                    Array.Resize(ref ArrStackLayoutPlayers, ArrStackLayoutPlayers.Length + 1);
                    ArrStackLayoutPlayers[ArrayPlayerSize - 1] = AddPlayerLayout(App.ArrayPlayer[ArrayPlayerSize - 1]);
                    this.StackPlayerList.Children.Add(ArrStackLayoutPlayers[ArrayPlayerSize -1]);
                    ScrollView_PlayerList.Content = this.StackPlayerList;
                }
                else
                {
                    App.ArrayPlayer[ArrayPlayerSize - 1] = null;
                    Array.Resize(ref App.ArrayPlayer, App.ArrayPlayer.Length - 1);
                    if (error == 1)
                    {
                        this.entry_PlayerName.Placeholder = "Player Name - Must not be empty !";
                    }
                    if (error == 2)
                    {
                        this.entry_PlayerName.Text = null;
                        this.entry_PlayerName.Placeholder = "Player Name already exists !";
                    }
                }            
            }
            catch (OutOfMemoryException)
            {
                DisplayAlert("Erreur", "Mémoire saturée - impossible de continuer", "Fermer");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        void Quit_Clicked(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        async void MenuClearScores_Clicked(object sender, System.EventArgs e)
        {
            if (App.ArrayPlayer.Length != 0)
            {
                var answer = await DisplayAlert("Clear all scores", "Are you sure ?", "Yes", "No");
                if (answer == true)
                {
                    for (int x = 0; x < App.ArrayPlayer.Length; x++) this.StackPlayerList.Children.Remove(ArrStackLayoutPlayers[x]);

                    ArrStackLayoutPlayers = null;
                    Array.Resize(ref ArrStackLayoutPlayers, App.ArrayPlayer.Length);
                    for (int x = 0; x < App.ArrayPlayer.Length; x++)
                    {
                        App.ArrayPlayer[x].set_points(0);
                        ArrStackLayoutPlayers[x] = AddPlayerLayout(App.ArrayPlayer[x]);
                        this.StackPlayerList.Children.Add(ArrStackLayoutPlayers[x]);
                    }
                    ScrollView_PlayerList.Content = this.StackPlayerList;
                }
            }else
            {
                await DisplayAlert("Sorry", "Can't clear if there's nothing to clear!", "OK");
            }
        }

        async void MenuDelete_Clicked(object sender, System.EventArgs e)
        {
            if (App.ArrayPlayer.Length != 0)
            {
                String[] ListName = new string[] { };
                Array.Resize(ref ListName, App.ArrayPlayer.Length);
                int index;

                for (int x = 0; x < App.ArrayPlayer.Length; x++) ListName[x] = App.ArrayPlayer[x].get_name();

                var action = await DisplayActionSheet("Which monkey is to be removed ?", "Cancel", null, ListName);

                index = FindIndexStringArray(ListName, action);
                App.ArrayPlayer = RemoveAt(App.ArrayPlayer, index);
                this.StackPlayerList.Children.Remove(ArrStackLayoutPlayers[index]);
                ArrStackLayoutPlayers = RemoveAt(ArrStackLayoutPlayers, index);
            }else
            {
                await DisplayAlert("Sorry", "No player to remove.", "OK");
            }

        }

        public int FindIndexStringArray(String[] source, string t_string)
        {
            int index = 0;
            for(int x = 0; x < source.Length;x++)
            {
                if (t_string == source[x])
                {
                    index = x;
                    break;
                }
            }
            return index;
        }

        //STOLEN FUNCTIONS

            //Removes an element of an array and resizes it.
        T[] RemoveAt<T>(T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }

    }
}
