using GeletaApp.Helpers;
using GeletaApp.Model;
using SQLite;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GeletaApp.Logic
{
    public class UserAuth
    {
        public static async Task<string> Login(string email, string password)
        {
            string hashedPass = StringHash.HashedPassword(password);

            string result = "";
            var response = await UserAPI_Logic.GetToken(email, hashedPass);
            if (response[0].ToString() == "Token")
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    var userObject = response[1] as User;
                    conn.CreateTable<UserPost>();
                    try
                    {
                        string dekoduotasTokenas = StringHash.DecryptToken(userObject.userToken);
                        var user = conn.Find<UserPost>(email);
                        if (user == null)
                        {
                            UserPost post = new UserPost()
                            {
                                Token = dekoduotasTokenas,
                                Email = email,
                                Password = password,
                                Name = userObject.name,
                                Address = userObject.address,
                                Phone_number = Convert.ToInt32(userObject.phone_number),
                                Postal_code = Convert.ToInt32(userObject.postal_code),
                                Discount_code = userObject.discount_code,
                                City = userObject.city
                            };
                            conn.InsertOrReplace(post);
                        }
                        else
                        {
                            /* if (response[1] != user.Token)
                             {
                                 //Padaryt tokeno update
                             }*/
                        }
                    }
                    catch (NullReferenceException nrex)
                    {

                    }
                    conn.Close();
                }
            }
            else
            {
                result = response[1].ToString();
            }
            return result;
        }

        public static async Task<string> Register(string email, string password)
        {
            string hashedPass = StringHash.HashedPassword(password);

            string result = "";
            var response = await UserAPI_Logic.RegisterUser(email, hashedPass);

            if (response[0] == "Token")
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<UserPost>();
                    try
                    {
                        string dekoduotasTokenas = StringHash.DecryptToken(response[1]);
                        var user = conn.Find<UserPost>(email);
                        if (user == null)
                        {
                            UserPost post = new UserPost()
                            {
                                Token = dekoduotasTokenas,
                                Email = email,
                                Password = password

                            };
                            conn.InsertOrReplace(post);
                        }
                        else
                        {
                            /* if (response[1] != user.Token)
                             {
                                 //Padaryt tokeno update
                             }*/
                        }
                    }
                    catch (NullReferenceException nrex)
                    {

                    }
                    conn.Close();
                }
            }
            else
            {
                result = response[1];
            }
            return result;
        }

        public static async Task<string> EditUserInfo(User user)
        {
            string hashedToken = StringHash.HashedPassword(user.userToken);
            var response = await UserAPI_Logic.EditUserData(hashedToken, user);
            return response;
        }
        public static async Task<string> ChangePass(string oldPass, string newPass)
        {
            string result = "";
            string token = "";
            string passDb = "";
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<UserPost>();
                var userD = conn.Table<UserPost>().First();
                token = userD.Token;
                passDb = userD.Password;
                conn.Close();
            }
            if (passDb.Equals(oldPass))
            {
                string hashedToken = StringHash.HashedPassword(token);
                string hashedoldPassword = StringHash.HashedPassword(oldPass);
                string hashednewPassword = StringHash.HashedPassword(newPass);

                var response = await UserAPI_Logic.ChangeUserPass(hashedToken, hashedoldPassword, hashednewPassword);
                if (response == "Pavyko")
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<UserPost>();
                        string query = string.Format("UPDATE UserPost SET Password = '{0}' WHERE Token LIKE '{1}'", newPass, token);
                        var userD = conn.Query<UserPost>(query);
                        conn.Close();
                    }
                }
                else if (response == "Klaida")
                {
                    result = "Įvyko klaida";
                }
                else
                    result = "Neteisingai įvestas senas slaptažodis";

            }
            else
            {
                result = "Neteisingai įvestas senas slaptažodis";
            }

            return result;
        }
        public static async Task<string> AddAdress(UserAddress adr)
        {
            string result = "";
            string token = "";
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<UserPost>();
                var userD = conn.Table<UserPost>().First();
                token = userD.Token;

                string hashedToken = StringHash.HashedPassword(token);
                var rep = await UserAPI_Logic.AddUserAddress(hashedToken, adr);
                if (rep[0].ToString() == "Id")
                {
                    conn.CreateTable<UserAddressPost>();
                    var adresses = conn.Table<UserAddressPost>().ToList();

                    UserAddressPost post = new UserAddressPost()
                    {
                        Id = (int)Convert.ToInt64(rep[1]),
                        Name = adr.name,
                        Address = adr.address,
                        City = adr.city,
                        Postal_code = (int)Convert.ToInt64(adr.postal_code),
                        Phone_number = (int)Convert.ToInt64(adr.phone_number)
                    };
                    conn.InsertOrReplace(post);
                }
                else
                {
                    result = rep[1].ToString();
                }


                conn.Close();

            }
            return result;
        }
        public static async void GetAddresses()
        {
            string token = "";
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<UserPost>();
                var userD = conn.Table<UserPost>().First();
                token = userD.Token;

                string hashedToken = StringHash.HashedPassword(token);
                var rep = await UserAPI_Logic.GetUserAddresses(hashedToken);
                if (rep != null)
                {
                    int count = rep.Count;
                    for (int i = 0; i < count; i++)
                    {
                        UserAddressPost post = new UserAddressPost()
                        {
                            Id = rep[i].id,
                            Name = rep[i].name,
                            Address = rep[i].address,
                            Postal_code = (int)Convert.ToUInt64(rep[i].postal_code),
                            Phone_number = (int)Convert.ToUInt64(rep[i].phone_number),
                            City = rep[i].city
                        };
                        conn.CreateTable<UserAddressPost>();

                        int rows = conn.InsertOrReplace(post);
                        if (rows > 0)
                        {
                            //success++;
                        }
                        else
                        {
                            //fail++;
                        }
                    }
                    var apiLast = rep.Last().id;
                    var apiCount = rep.Count;
                    var posts = conn.Table<UserAddressPost>().ToList();
                    var dbLast = posts.Last().Id;
                    int dbCount = posts.Count;
                    if (apiLast != dbLast && dbCount != apiCount)
                    {
                        foreach (var a in posts)
                        {
                            if (!rep.Any(n => n.id == a.Id))
                            {
                                conn.Delete<UserAddressPost>(a.Id);
                            }
                        }
                    }
                }
                conn.Close();
            }
        }
        public static async Task<string> EditUserAddress(UserAddress useraddress)
        {
            string hashedToken = getHashedToken();
            var response = await UserAPI_Logic.EditUserAddress(hashedToken, useraddress);
            return response;
        }

        public static async Task<string> DeleteUserAddress(int id)
        {
            string hashedToken = getHashedToken();
            var response = await UserAPI_Logic.DeleteUserAddress(hashedToken, id);
            return response;
        }
        /// <summary>
        /// Paimamas tokenas iš sqlite duombazes
        /// </summary>
        /// <returns>Grąžinamas heshuotas tokenas</returns>
        public static string getHashedToken()
        {
            string token = "";
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<UserPost>();
                var userD = conn.Table<UserPost>().First();
                token = userD.Token;
            }
            string hashedToken = StringHash.HashedPassword(token);
            return hashedToken;
        }
    }

}

