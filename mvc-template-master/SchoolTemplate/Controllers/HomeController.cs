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
    string connectionString = "Server=172.16.160.21;Port=3306;Database=110041;Uid=110041;Pwd=MEdenkgR;";

    public IActionResult Index()
    {
      List<Festival> festivals = new List<Festival>();
        // uncomment deze regel om producten uit je database toe te voegen
        festivals = GetFestivals();

        return View(festivals);
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


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
