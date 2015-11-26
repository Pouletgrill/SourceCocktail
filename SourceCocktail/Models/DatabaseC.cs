﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatabaseC
{
    public class Cocktail : SqlExpressUtilities.SqlExpressWrapper
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        public Cocktail(String cs)
            : base(cs)
        {
        }
    }

    public class Composants : SqlExpressUtilities.SqlExpressWrapper
    {
        public int Id { get; set; }
        public int Id_Cocktail { get; set; }
        public int Id_Ingredient { get; set; }
        public int Qte { get; set; }
        public Composants(String cs)
            : base(cs)
        {
        }
    }

    public class Ingredients : SqlExpressUtilities.SqlExpressWrapper
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }
        public Ingredients(String cs)
            : base(cs)
        {
        }
    }
}