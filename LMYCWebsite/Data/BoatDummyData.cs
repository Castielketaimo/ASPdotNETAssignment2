using LmycDataLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMYCWebsite.Data
{
    public class BoatDummyData
    {
        public static System.Collections.Generic.List<Boat> getBoats()
        {
            List<Boat> boats = new List<Boat>()
            {
                new Boat()
                {
                    BoatId = 1,
                    BoatName = "Fireball",
                    Picture = "https://en.wiktionary.org/wiki/cat",
                    LengthInFeet = 1,
                    Make = "make1",
                    Year = 1902,
                    RecordCreationDate = DateTime.Now,
                    CreatedBy = "a"
                },
                new Boat()
                {
                    BoatId = 1,
                    BoatName = "Waterball",
                    Picture = "https://en.wiktionary.org/wiki/cat",
                    LengthInFeet = 1,
                    Make = "make1",
                    Year = 1902,
                    RecordCreationDate = DateTime.Now,
                    CreatedBy = "a"
                },
                new Boat()
                {
                    BoatId = 1,
                    BoatName = "Lazerball",
                    Picture = "https://en.wiktionary.org/wiki/cat",
                    LengthInFeet = 1,
                    Make = "make1",
                    Year = 1902,
                    RecordCreationDate = DateTime.Now,
                    CreatedBy = "a"
                }
            };
            return boats;
        }
    }
}