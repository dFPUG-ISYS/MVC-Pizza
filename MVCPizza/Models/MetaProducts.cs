using MVCPizza.RulesEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVCPizza
{
    [MetadataType(typeof(MetaProducts))]
    public partial class products : IControllerHooks
    {
        public void OnAdd() 
        {            
            Valid();
        }
        public void OnUpdate() 
        {
            ErrorMessages = new List<string>();
            Valid();
        }

        public void Valid()
        {
            //Validierung "klassisch":
            //ErrorMessages = new List<string>();
            //if (String.IsNullOrWhiteSpace(productcode))
            //    ErrorMessages.Add("ProductCode darf nicht leer sein");
            //if (!String.IsNullOrWhiteSpace(barcode) && barcode.Contains('a'))
            //    ErrorMessages.Add("Barcode sollte kein a enthalten");
            //if (productprice <= 3)
            //    ErrorMessages.Add("Pizza darf nicht weniger als 3 Euro kosten");
            
            // Validierung "modern" (mit Rules-Engine und Fluent)
            RulesProcessor rp = new RulesProcessor();
            rp.NotEmptyRule(productcode, "ProductCode darf nicht leer sein")
              .NotContainRule(barcode, "a", "Barcode sollte kein a enthalten")
              .NotLessThanRule(productprice, 3, "Pizza darf nicht weniger als 3 Euro kosten");
            ErrorMessages = rp.ValidationResults;

        }
        public List<string> ErrorMessages { get; set;}
        // Menüeinträge :
        public List<ControllerAction> actions
        {
            get{
                var _actions = new List<ControllerAction>();
                _actions.Add( new ControllerAction()
                        {
                            text = "Raise Prices",
                            action = "RaisePrices",
                            view = "Index",
                            controller = "products"
                        });
                _actions.Add( new ControllerAction()
                        {
                            text = "Cut Prices",
                            action = "CutPrices",
                            view = "Index",
                            controller = "products"
                        });
                return _actions;
                }
        }
    }
    
    public class MetaProducts
    {
        public int productid { get; set; }
        
        public Nullable<int> categoryid { get; set; }
        
        [Display(Name="Code")]
        [Required]
        public string productcode { get; set; }
        
        [Display(Name="Name")]
        public string productname { get; set; }

        [DataType(DataType.ImageUrl)]
        public string picturefile { get; set; }
        
        [Display(Name="Price")]
        public Nullable<decimal> productprice { get; set; }
        
        public string barcode { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<int> supplierid { get; set; }
        
        [ScaffoldColumn(false)]
        public byte[] timestamp_column { get; set; }

        [Display(Name="Zusätze")]
        [DataType(DataType.MultilineText)]
        public string zusaetze { get; set; }
    }

    public class productsActions
    {
        public void RaisePrices(PizzaEntities db)
        {
            var products = db.products;
            foreach (var item in products)
            {
                item.productprice += 1;
            }
            db.SaveChanges();
        }
        public void CutPrices(PizzaEntities db)
        {
            var products = db.products;
            foreach (var item in products)
            {
                item.productprice -= 1;
            }
            db.SaveChanges();
        }
        public void ExecAction(string action, PizzaEntities db)
        {
            switch (action)
            {
                case "RaisePrices" :
                    RaisePrices(db);
                    break;

                case "CutPrices" :
                    CutPrices(db);
                    break;

                default:
                    break;
            }
        }
    }
}