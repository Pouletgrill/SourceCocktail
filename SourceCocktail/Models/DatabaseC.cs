using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DatabaseC
{
    public class ImageGUIDReference
    {
        public String DefaultImage { get; set; }
        public String BasePath { get; set; }
        public String GUID { get; set; }
        public ImageGUIDReference(String basePath, String defautImage)
        {
            this.BasePath = basePath;
            this.DefaultImage = defautImage;
            GUID = "";
        }

        public String GetURL()
        {
            String url;
            if (String.IsNullOrEmpty(GUID))
                url = BasePath + DefaultImage;
            else
                url = BasePath + GUID + ".png";
            return url;
        }

        public String GetURI()
        {
            String uri;
            if (String.IsNullOrEmpty(GUID))
                uri = "";
            else
                uri = BasePath + GUID + ".png";
            return uri;
        }

        public String GetImageURL(String GUID)
        {
            String url;
            if (String.IsNullOrEmpty(GUID))
                url = BasePath + DefaultImage;
            else
                url = BasePath + GUID + ".png";
            return url;
        }

        public String UpLoadImage(HttpRequestBase Request, String PreviousGUID)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    GUID = PreviousGUID;
                    if (!String.IsNullOrEmpty(GUID))
                    {
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(GetURI()));
                    }
                    GUID = Guid.NewGuid().ToString();
                    file.SaveAs(HttpContext.Current.Server.MapPath(GetURI()));
                    return GUID;
                }
            }
            return PreviousGUID;
        }

        public void Remove(String GUID)
        {
            if (!String.IsNullOrEmpty(GUID))
            {
                this.GUID = GUID;
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(GetURI()));
            }
        }
    }

    ////////////////////////////////////////COCKTAIL//////////////////////////////////
    public class Cocktail
    {
        public long Id { get; set; }

        [Display(Name = "Nom")]
        [StringLength(50), Required]
        [RegularExpression(@"^((?!^Name$)[-a-zA-Z0-9 àâäçèêëéìîïòôöùûüÿñÀÂÄÇÈÊËÉÌÎÏÒÔÖÙÛÜ_'])+$", ErrorMessage = "Caractères illégaux.")]
        public String Nom { get; set; }

        [Display(Name = "Description")]
        [StringLength(1024), Required]
        public String Description { get; set; }

        [Display(Name = "Affiche")]        
        [StringLength(50), Required]
        public String Affiche_Id { get; set; }

        private ImageGUIDReference ImageReference;

        public Cocktail()
        {
            Nom = "";
            Description = "";
            Affiche_Id = "";
            ImageReference = new ImageGUIDReference(@"/Images/Films/", @"UnknownPoster.png");
        }

        public String GetPosterURL()
        {
            return ImageReference.GetImageURL(Affiche_Id);
        }

        public void UpLoadPoster(HttpRequestBase Request)
        {
            Affiche_Id = ImageReference.UpLoadImage(Request, Affiche_Id);
        }

        public void RemovePoster()
        {
            ImageReference.Remove(Affiche_Id);
        }
    }
    public class Cocktails : SqlExpressUtilities.SqlExpressWrapper
    {
        public Cocktail cocktail { get; set; }
        public Cocktails(object cs)
            : base(cs)
        {
            cocktail = new Cocktail();
        }
        public Cocktails() { cocktail = new Cocktail(); }

        public List<Cocktail> ToList()
        {
            List<object> list = this.RecordsList();
            List<DatabaseC.Cocktail> cocktail_list = new List<Cocktail>();
            foreach (Cocktail cocktail in list)
            {
                cocktail_list.Add(cocktail);
            }
            return cocktail_list;
        }

        public override void DeleteRecordByID(String ID)
        {
            if (this.SelectByID(ID))
            {
                this.cocktail.RemovePoster();
                base.DeleteRecordByID(ID);
            }
        }
    }

    ////////////////////////////////////////COMPOSANT//////////////////////////////////
    public class Composant
    {
        public long Id { get; set; }
        public long Id_Cocktail { get; set; }
        public long Id_Ingredient { get; set; }
        public int Qte { get; set; }

        public Composant() { }
    }
    public class Composants : SqlExpressUtilities.SqlExpressWrapper
    {
        public Composant composant { get; set; }
        public Composants(object cs)
            : base(cs)
        {
            composant = new Composant();
        }
        public Composants() { composant = new Composant(); }
    }

    ///////////////////////////////////////INGREDIANT//////////////////////////////////
    public class Ingrediant
    {
        public long Id { get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }

        public Ingrediant() { }
    }
    public class Ingrediants : SqlExpressUtilities.SqlExpressWrapper
    {
        public Ingrediant ingrediant { get; set; }
        public Ingrediants(object cs)
            : base(cs)
        {
            ingrediant = new Ingrediant();
        }
        public Ingrediants() { ingrediant = new Ingrediant(); }
    }
    /*
    public class Ingredients : SqlExpressUtilities.SqlExpressWrapper
    {
        public long Id { get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }
        public Ingredients(String cs)
            : base(cs)
        {
        }
    }*/
}