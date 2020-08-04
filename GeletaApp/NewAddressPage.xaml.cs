using GeletaApp.Logic;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp.Model
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewAddressPage
    {
        public static bool isEdit = false;
        public NewAddressPage(UserAddress addressobj = null)
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            if (addressobj != null)
            {
                isEdit = true;
                adresoId.Text = addressobj.id.ToString();
                langoPavadinimas.Text = "ADRESO REDAGAVIMAS";
                if (addressobj.postal_code != "0")
                {
                    pastoKodas.Text = addressobj.postal_code;
                }
                if (addressobj.phone_number != "0")
                {
                    telefonoNumeris.Text = addressobj.phone_number;
                }
                vardas.Text = addressobj.name;
                adresas.Text = addressobj.address;
                miestas.Text = addressobj.city;
            }
            else
            {
                langoPavadinimas.Text = "NAUJO ADRESO PRIDĖJIMAS";
            }

        }
        private async void atsauktiBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void issaugotiBtn_Clicked(object sender, EventArgs e)
        {
            bool isPostCodeEmpty = string.IsNullOrEmpty(pastoKodas.Text);
            bool isPhoneNumberEmpty = string.IsNullOrEmpty(telefonoNumeris.Text);
            bool isNameEmpty = string.IsNullOrEmpty(vardas.Text);
            bool isAddressEmpty = string.IsNullOrEmpty(adresas.Text);
            bool isCityEmpty = string.IsNullOrEmpty(miestas.Text);
            if (isPostCodeEmpty || isPhoneNumberEmpty || isNameEmpty || isAddressEmpty || isCityEmpty)
            {
                await App.Current.MainPage.DisplayAlert("Įspėjimas", "Visi laukai privalo būti užpildyti.", "Uždaryti");
            }
            else
            {
                int postCode = 0;
                int phoneNumber = 0;
                int id = -1;
                if (adresoId.Text != null)
                {
                    id = (int)Convert.ToInt64(adresoId.Text);
                }

                string name = vardas.Text;
                string address = adresas.Text;
                string city = miestas.Text;
                bool isPostCodeDigit = pastoKodas.Text.All(char.IsDigit);
                bool isPhoneNumberDigit = telefonoNumeris.Text.All(char.IsDigit);
                bool vykdytiToliau = false;
                if (isPostCodeDigit && isPhoneNumberDigit)
                {
                    postCode = (int)Convert.ToInt64(pastoKodas.Text);
                    phoneNumber = (int)Convert.ToInt64(telefonoNumeris.Text);
                    vykdytiToliau = true;
                }
                else if (isPostCodeDigit && !isPhoneNumberDigit)
                {
                    await App.Current.MainPage.DisplayAlert("Įspėjimas", "Telefono numeris sudaromas tik iš skaičių", "Uždaryti");
                }
                else if (!isPostCodeDigit && isPhoneNumberDigit)
                {
                    await App.Current.MainPage.DisplayAlert("Įspėjimas", "Pašto kodas sudaromas tik iš skaičių", "Uždaryti");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Įspėjimas", "Pašto kodas ir telefono numeris sudaromas tik iš skaičių", "Uždaryti");
                }

                if (vykdytiToliau)
                {
                    //esamo adreso redagavimas
                    if (isEdit)
                    {
                        //jei adresas is profilio lango
                        if (id == 0)
                        {
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
                                if (name != vardasDB)
                                {
                                    atnaujinti = true;
                                    if (query != "UPDATE UserPost SET")
                                    {
                                        query += ",";
                                    }
                                    query = string.Format(query + " Name = '" + name + "'");
                                    vartotojas.name = name;
                                    //Pakeisti
                                }
                                if (city != miestDB)
                                {
                                    atnaujinti = true;
                                    if (query != "UPDATE UserPost SET")
                                    {
                                        query += ",";
                                    }
                                    query = string.Format(query + " City = '" + city + "'");
                                    vartotojas.city = city;
                                    //Pakeisti
                                }
                                if (phoneNumber != numerisDB)
                                {
                                    atnaujinti = true;
                                    if (query != "UPDATE UserPost SET")
                                    {
                                        query += ",";
                                    }
                                    query = string.Format(query + " Phone_number = " + phoneNumber);
                                    vartotojas.phone_number = phoneNumber.ToString();
                                    //Pakeisti
                                }

                                if (postCode != pastKodasDB)
                                {
                                    atnaujinti = true;
                                    if (query != "UPDATE UserPost SET")
                                    {
                                        query += ",";
                                    }
                                    vartotojas.postal_code = postCode.ToString();
                                    query = string.Format(query + " Postal_code = " + postCode);
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

                                        PopupNavigation.Instance.PopAsync();
                                        DisplayAlert("Pranešimas", "Informacija buvo sėkmingai redaguota", "Uždaryti");
                                        Navigation.PopAsync();
                                        Navigation.PushAsync(new AdressPage());
                                    }
                                    else
                                    {
                                        await DisplayAlert("Klaida", "Nepavyko pakeisti vartotojo informacijos", "Uždaryti");
                                    }
                                }
                                conn.Close();
                            }
                        }
                        //jei adresas papildomas
                        else
                        {
                            bool atnaujinti = false;
                            string vardasDB;
                            int numerisDB;
                            int pastKodasDB;
                            string miestDB;
                            //string nuolaidaDB;
                            string addressDB;
                            //string token;

                            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                            {
                                //conn.CreateTable<UserPost>();
                                UserAddress vartotojas = new UserAddress();
                                vartotojas.id = id;
                                var user = conn.Find<UserAddressPost>(id);
                                //token = user[0].Token;
                                vardasDB = user.Name;
                                numerisDB = user.Phone_number;
                                pastKodasDB = user.Postal_code;
                                // nuolaidaDB = user[0].Discount_code;
                                addressDB = user.Address;
                                miestDB = user.City;

                                //vartotojas.userToken = token;
                                string query = "UPDATE UserAddressPost SET";
                                if (name != vardasDB)
                                {
                                    atnaujinti = true;
                                    if (query != "UPDATE UserAddressPost SET")
                                    {
                                        query += ",";
                                    }
                                    query = string.Format(query + " Name = '" + name + "'");
                                    vartotojas.name = name;
                                    //Pakeisti
                                }
                                if (city != miestDB)
                                {
                                    atnaujinti = true;
                                    if (query != "UPDATE UserAddressPost SET")
                                    {
                                        query += ",";
                                    }
                                    query = string.Format(query + " City = '" + city + "'");
                                    vartotojas.city = city;
                                    //Pakeisti
                                }
                                if (phoneNumber != numerisDB)
                                {
                                    atnaujinti = true;
                                    if (query != "UPDATE UserAddressPost SET")
                                    {
                                        query += ",";
                                    }
                                    query = string.Format(query + " Phone_number = " + phoneNumber);
                                    vartotojas.phone_number = phoneNumber.ToString();
                                    //Pakeisti
                                }

                                if (postCode != pastKodasDB)
                                {
                                    atnaujinti = true;
                                    if (query != "UPDATE UserAddressPost SET")
                                    {
                                        query += ",";
                                    }
                                    vartotojas.postal_code = postCode.ToString();
                                    query = string.Format(query + " Postal_code = " + postCode);
                                    //Pakeisti
                                }

                                if (address != addressDB)
                                {
                                    atnaujinti = true;
                                    if (query != "UPDATE UserAddressPost SET")
                                    {
                                        query += ",";
                                    }
                                    vartotojas.address = address;
                                    query = string.Format(query + " Address = '" + address + "'");
                                    //Pakeisti
                                }
                                query = string.Format(query + " WHERE Id LIKE '" + id + "'");

                                if (atnaujinti)
                                {
                                    var response = await UserAuth.EditUserAddress(vartotojas);
                                    if (response == "Pavyko")
                                    {
                                        var posts = conn.Query<UserAddressPost>(query);

                                        PopupNavigation.Instance.PopAsync();
                                        DisplayAlert("Pranešimas", "Informacija buvo sėkmingai redaguota", "Uždaryti");
                                        Navigation.PopAsync();
                                        Navigation.PushAsync(new AdressPage());
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

                    //Naujo adreso sukurimas
                    else
                    {
                        UserAddress adr = new UserAddress()
                        {
                            name = name,
                            address = address,
                            city = city,
                            postal_code = postCode.ToString(),
                            phone_number = phoneNumber.ToString()
                        };
                        string result = await UserAuth.AddAdress(adr);
                        if (result == "")
                        {
                            await App.Current.MainPage.DisplayAlert("Pavyko", "Adresas sėkmingai pridėtas", "OK");
                            await Navigation.PushAsync(new AdressPage());
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Klaida", "Nepavyko pridėti adreso", "OK");
                        }

                    }

                }
            }
        }
    }
}