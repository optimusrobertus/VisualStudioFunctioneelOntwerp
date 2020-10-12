using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
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
            // uncomment deze regel om producten uit je database toe te voegen
            festivals = GetFestivals();

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

    private List<Festival> GetFestivals()
    {
      List<Festival> products = new List<Festival>();

      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("select * from festival", conn);

        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            int Id = Convert.ToInt32(reader["Id"]);
            string Naam = reader["Naam"].ToString();
            string Img = reader["Img"].ToString();
            Festival p = new Festival
            {
              Id = Id,
              Naam = Naam,
              Beschrijving = reader["beschrijving"].ToString(),
              datum = DateTime.Parse(reader["datum"].ToString()),
              Img = reader["Img"].ToString(),
            };
            products.Add(p);
          }
        }
      }

      return products;
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
