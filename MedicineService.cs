using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PharmacyInventoryManager
{
    public class MedicineService
    {
        private List<Medicine> _medicines = new List<Medicine>();
        private int _nextId = 1;
        private string filePath = "medicine_inventory.csv";  // CSV file to persist data

        public MedicineService()
        {
            LoadFromFile();  // Load data when app starts
        }

        // Add a new medicine
        public void AddMedicine(Medicine med)
        {
            med.Id = _nextId++;
            _medicines.Add(med);
            SaveToFile();  // Save changes
        }

        // Get all medicines
        public List<Medicine> GetAllMedicines()
        {
            return _medicines;
        }

        // Update an existing medicine by ID
        public bool UpdateMedicine(Medicine updatedMed)
        {
            var existing = _medicines.FirstOrDefault(m => m.Id == updatedMed.Id);
            if (existing != null)
            {
                existing.Name = updatedMed.Name;
                existing.Category = updatedMed.Category;
                existing.Quantity = updatedMed.Quantity;
                existing.Supplier = updatedMed.Supplier;
                existing.Price = updatedMed.Price;
                SaveToFile();  // Save changes
                return true;
            }
            return false;
        }

        // Delete a medicine by ID
        public bool DeleteMedicine(int id)
        {
            var med = _medicines.FirstOrDefault(m => m.Id == id);
            if (med != null)
            {
                _medicines.Remove(med);
                SaveToFile();  // Save changes
                return true;
            }
            return false;
        }

        // Save data to CSV file
        private void SaveToFile()
        {
            var lines = _medicines.Select(m =>
                $"{m.Id},{m.Name},{m.Category},{m.Quantity},{m.Supplier},{m.Price}");
            File.WriteAllLines(filePath, lines);
        }

        // Load data from CSV file
        private void LoadFromFile()
        {
            if (!File.Exists(filePath))
                return;

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 6)
                {
                    Medicine m = new Medicine
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[1],
                        Category = parts[2],
                        Quantity = int.Parse(parts[3]),
                        Supplier = parts[4],
                        Price = decimal.Parse(parts[5])
                    };
                    _medicines.Add(m);
                    _nextId = Math.Max(_nextId, m.Id + 1);
                }
            }
        }
    }
}
