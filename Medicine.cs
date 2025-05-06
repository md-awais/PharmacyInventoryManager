using System;

namespace PharmacyInventoryManager
{
    public class Medicine
    {
        public int Id { get; set; }  // Unique identifier (used for update/delete)
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string Supplier { get; set; }
        public decimal Price { get; set; }
    }
}
