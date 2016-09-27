using InventorySystem.Models;
using System.Data.Entity;

namespace InventorySystem
{
    public class InventoryDb : DbContext
    {
        public DbSet<Hammer> Hammers { get; set; }
    }
}