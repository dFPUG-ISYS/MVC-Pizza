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
    
    public partial class orders
    {
        public orders()
        {
            this.orderdetails = new HashSet<orderdetails>();
        }
    
        public int orderid { get; set; }
        public Nullable<System.DateTime> orderdate { get; set; }
        public Nullable<int> customerid { get; set; }
        public string shiptoname { get; set; }
        public string shiptoaddress { get; set; }
        public Nullable<decimal> totalsum { get; set; }
        public Nullable<bool> paid { get; set; }
    
        public virtual customers customers { get; set; }
        public virtual ICollection<orderdetails> orderdetails { get; set; }
    }
}
