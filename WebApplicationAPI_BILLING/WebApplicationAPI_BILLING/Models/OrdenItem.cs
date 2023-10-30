using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationAPI_BILLING.Models;

namespace WebApplicationAPI_BILLING.Models
{
    public class OrdenItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public decimal UnitPrice { get; set; } = 0;
        [Required]
        public int Quantity { get; set; } = 1;
        [ForeignKey("OrdenId")]
        public OrdenC? OrdenC { get; set; }
        [ForeignKey("ProductId")]
        public Producto? Producto { get; set; }
    }
}