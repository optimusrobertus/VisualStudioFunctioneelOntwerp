using System;

namespace SchoolTemplate.Database
{
  public class Festival
  {
    public int Id { get; set; }
    
    public string Naam { get; set; }

    public string Beschrijving{ get; set; }

    public string KorteBeschrijving() { return this.Beschrijving.Substring(0, this.Beschrijving.IndexOf(" ", 400)); }

    public string Img { get; set; }
        
    public DateTime Datum { get; set; }

    public Decimal Prijs { get; set; }

    /// <summary>
    /// Gebruik altijd decimal voor geldzaken. Dit doe je om te voorkomen dat er afrondingsfouten optreden
    /// </summary>
   

  }
}
