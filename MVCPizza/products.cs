//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCPizza
{
    using System;
    using System.Collections.Generic;
    
    public partial class products
    {
        public products()
        {
            this.orderdetails = new HashSet<orderdetails>();
        }
    
        public int productid { get; set; }
        public Nullable<int> categoryid { get; set; }
        public string productcode { get; set; }
        public string productname { get; set; }
        public string picturefile { get; set; }
        public Nullable<decimal> productprice { get; set; }
        public string barcode { get; set; }
        public Nullable<int> supplierid { get; set; }
        public byte[] timestamp_column { get; set; }
        public string zusaetze { get; set; }
    
        public virtual category category { get; set; }
        public virtual ICollection<orderdetails> orderdetails { get; set; }
    }
}
