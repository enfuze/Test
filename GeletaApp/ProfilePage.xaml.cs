using GeletaApp.Logic;
using GeletaApp.Model;
using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GeletaApp
{
    [DesignTimeVisible(true)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            var xamarinHeight = height / mainDisplayInfo.Density;
            // BackImgButton.WidthRequest = xamarinWidth * 6.85185 / 100;
            //profile_cp.HeightRequest = xamarinHeight;
            //profile_cp.WidthRequest = xamarinWidth;
            profile_m_stc.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 2.03 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885416 / 100);

            BackImgButton.WidthRequest = xamarinWidth * 6.85185 / 100;

            // virsutineJuosta.HeightRequest = xamarinHeight * 10.416 / 100;
            grybas.HeightRequest = xamarinHeight * 23 / 100;
            grybas.Margin = new Thickness(0, xamarinHeight * 2 / 100, 0, xamarinHeight * 1.09 / 100);
            vardas_pavarde.HeightRequest = xamarinHeight * 2.604 / 100;
            duomenuStack.Padding = new Thickness(xamarinWidth * 16.296 / 100, 0, xamarinWidth * 15.833 / 100, 0);

            //telNRstack.HeightRequest = xamarinHeight * 15.833 / 200;
            nameEditor.HeightRequest = xamarinHeight * 6 / 100;


            telNr.WidthRequest = xamarinWidth * 58.61 / 100;
            telNr.HeightRequest = xamarinHeight * 6 / 100;
            elPastas.HeightRequest = xamarinHeight * 6 / 100;
            adresas.HeightRequest = xamarinHeight * 6 / 100;
            miestas.HeightRequest = xamarinHeight * 6 / 100;
            pastoKodas.HeightRequest = xamarinHeight * 6 / 100;
            nuolaidosKodas.HeightRequest = xamarinHeight * 6 / 100;
            pagr_info_label.Padding = new Thickness(xamarinWidth * 8.888 / 100, 0, 0, 0);
            issaugoti.HeightRequest = xamarinHeight * 5.260 / 100;
            issaugoti.WidthRequest = xamarinHeight * 60 / 100;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<UserPost>();

                var user = conn.Table<UserPost>().ToList();
                if (user[0].Name != null)
                {
                    nameEditor.Text = user[0].Name;
                }
                else
                {
                    nameEditor.Placeholder = "Įveskite vardą ir pavardę";
                }

                if (user[0].Phone_number != 0)
                {
                    telNr.Text = user[0].Phone_number.ToString();
                }
                else
                {
                    telNr.Placeholder = "Įveskite telefono numerį";
                }

                if (user[0].Email != "")
                {
                    elPastas.Text = user[0].Email;
                }
                else
                {
                    elPastas.Placeholder = "Įveskite el. paštą";
                }

                if (user[0].City != null)
                {
                    miestas.Text = user[0].City;
                }
                else
                {
                    miestas.Placeholder = "Įveskite miestą";
                }

                if (user[0].Postal_code != 0)
                {
                    pastoKodas.Text = user[0].Postal_code.ToString();
                }
                else
                {
                    pastoKodas.Placeholder = "Įveskite pašto kodą";
                }

                if (user[0].Discount_code != null)
                {
                    nuolaidosKodas.Text = user[0].Discount_code;
                }
                else
                {
                    nuolaidosKodas.Placeholder = "Įveskite nuolaidos kodą";
                }
                if (user[0].Address != null)
                {
                    adresas.Text = user[0].Address;
                }
                else
                {
                    adresas.Placeholder = "Įveskite adresa";
                }
                // pastoKodas.Text = "pastas";
                // nameEditor.Text = "vardas";
            }
        }

        private void editName_Clicked(object sender, EventArgs e)
        {
            if (nameEditor.IsEnabled == false)
                nameEditor.IsEnabled = true;
            else if (nameEditor.IsEnabled == true)
                nameEditor.IsEnabled = false;

        }
        private async void BackImgButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            Functions tools = new Functions();
            tools.BackButtonWithExit("pop_page");
            return true;
        }

        private async void issaugoti_Clicked(object sender, EventArgs e)
        {
            string vardas = nameEditor.Text;
            int numeris = (int)Convert.ToInt64(telNr.Text);
            int pastKodas = (int)Convert.ToInt64(pastoKodas.Text);
            string nuolaida = nuolaidosKodas.Text;
            string address = adresas.Text;
            string miest = miestas.Text;

            bool atnaujinti = false;
            string vardasDB;
            int numerisDB;
            int pastKodasDB;
            string miestDB;
            string nuolaidaDB;
            string addressDB;
            string token;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<UserPost>();
                User vartotojas = new User();
                var user = conn.Table<UserPost>().ToList();
                token = user[0].Token;
                vardasDB = user[0].Name;
                numerisDB = user[0].Phone_number;
                pastKodasDB = user[0].Postal_code;
                nuolaidaDB = user[0].Discount_code;
                addressDB = user[0].Address;
                miestDB = user[0].City;

                vartotojas.userToken = token;
                string query = "UPDATE UserPost SET";
                if (vardas != vardasDB)
                {
                    atnaujinti = true;
                    if (query != "UPDATE UserPost SET")
                    {
                        query += ",";
                    }
                    query = string.Format(query + " Name = '" + vardas + "'");
                    vartotojas.name = vardas;
                    //Pakeisti
                }
                if (miest != miestDB)
                {
                    atnaujinti = true;
                    if (query != "UPDATE UserPost SET")
                    {
                        query += ",";
                    }
                    query = string.Format(query + " City = '" + miest + "'");
                    vartotojas.city = miest;
                    //Pakeisti
                }
                if (numeris != numerisDB)
                {
                    atnaujinti = true;
                    if (query != "UPDATE UserPost SET")
                    {
                        query += ",";
                    }
                    query = string.Format(query + " Phone_number = '" + numeris + "'");
                    vartotojas.phone_number = numeris.ToString();
                    //Pakeisti
                }

                if (pastKodas != pastKodasDB)
                {
                    atnaujinti = true;
                    if (query != "UPDATE UserPost SET")
                    {
                        query += ",";
                    }
                    vartotojas.postal_code = pastKodas.ToString();
                    query = string.Format(query + " Postal_code = '" + pastKodas + "'");
                    //Pakeisti
                }
                if (nuolaida != nuolaidaDB)
                {
                    atnaujinti = true;
                    if (query != "UPDATE UserPost SET")
                    {
                        query += ",";
                    }
                    if (nuolaida == "")
                    {
                        vartotojas.discount_code = "tuscia";
                    }
                    else
                    {
                        vartotojas.discount_code = nuolaida;
                    }
                    query = string.Format(query + " Discount_code = '" + nuolaida + "'");
                    //Pakeisti
                }
                if (address != addressDB)
                {
                    atnaujinti = true;
                    if (query != "UPDATE UserPost SET")
                    {
                        query += ",";
                    }
                    vartotojas.address = address;
                    query = string.Format(query + " Address = '" + address + "'");
                    //Pakeisti
                }
                query = string.Format(query + " WHERE Token LIKE '" + token + "'");

                if (atnaujinti)
                {
                    var response = await UserAuth.EditUserInfo(vartotojas);
                    if (response == "Pavyko")
                    {
                        var posts = conn.Query<UserPost>(query);
                        DisplayAlert("Pranešimas", "Informacija buvo sėkmingai redaguota", "Uždaryti");
                        Navigation.PopAsync();
                        Navigation.PushAsync(new ProfilePage());
                    }
                    else
                    {
                        await DisplayAlert("Klaida", "Nepavyko pakeisti vartotojo informacijos", "Uždaryti");
                    }
                }
                conn.Close();
            }
        }
    }
}