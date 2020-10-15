using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Renci.SshNet.Security.Cryptography;
using SchoolTemplate.Database;
using SchoolTemplate.Models;

namespace SchoolTemplate.Controllers
{
    public class HomeController : Controller
    {

        // zorg ervoor dat je hier je gebruikersnaam (leerlingnummer) en wachtwoord invult
        //string connectionString = "Server=172.16.160.21;Port=3306;Database=110041;Uid=110041;Pwd=MEdenkgR;"; //voor school
        string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=110041;Uid=110041;Pwd=MEdenkgR;"; //voor thuis


        // [Route("Index")]
        public IActionResult Index()
        {
            List<Festival> festivals = new List<Festival>();
            festivals = GetLatestFestivals();

            return View(festivals);
        }

        [Route("account")]
        public IActionResult account()
        {
            return View();
        }

        [Route("contact")]
        public IActionResult contact()
        {
            return View();
        }

        [Route("contact")]
        [HttpPost]
        public IActionResult contact(PersonModel model)
        {
            // No valid model? Then error
            if (!ModelState.IsValid)
                return View(model);
            
            // Model is valid, and can be saved
            SavePerson(model);

            return Redirect("/succes");
        }

        [Route("succes")]
        public IActionResult succes()
        {
            return View();
        }

        [Route("field")]
        public IActionResult field()
        {
            return View();
        }

        [Route("line")]
        public IActionResult line()
        {
            return View();
        }

        [Route("FAQ")]
        public IActionResult FAQ()
        {
            return View();
        }

        [Route("festival")]
        public IActionResult Festival()
        {
            return View();
        }

        [Route("festival/{id}")]
        public IActionResult Festival(string id)
        {
            var model = GetFestival(id);

            return View(model);
        }

        private List<Festival> GetLatestFestivals()
        {
            List<Festival> festivals = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from festival limit 3", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival p = new Festival
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["naam"].ToString(),
                            Beschrijving = reader["beschrijving"].ToString(),
                            Datum = DateTime.Parse(reader["datum"].ToString()),
                            Img = reader["Img"].ToString()
                        };
                        festivals.Add(p);
                    }
                }
            }

            return festivals;
        }

        private Festival GetFestival(string id)
        {
            List<Festival> festivals = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from festival where id = {id}", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival p = new Festival
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["naam"].ToString(),
                            Beschrijving = reader["beschrijving"].ToString(),
                            Datum = DateTime.Parse(reader["datum"].ToString()),
                            Img = reader["Img"].ToString()
                        };
                    }
                }
            }

            return festivals[0];
        }

    private void SavePerson(PersonModel person)
        {
            using(MySqlConnection conn= new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO customer(firstname, lastname, phonenumber, email, subject) VALUES(?firstname, ?lastname, ?phonenumber, ?email, ?subject)", conn);
                cmd.Parameters.Add("?firstname", MySqlDbType.VarChar).Value = person.firstname;
                cmd.Parameters.Add("?lastname", MySqlDbType.VarChar).Value = person.lastname;
                cmd.Parameters.Add("?phonenumber", MySqlDbType.VarChar).Value = person.phonenumber;
                cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = person.email;
                cmd.Parameters.Add("?subject", MySqlDbType.VarChar).Value = person.subject;
                cmd.ExecuteNonQuery();
            }   
        }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
