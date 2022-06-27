using System.ComponentModel.DataAnnotations;

namespace TaskService.Plugin.Clients.Models
{
    /// <summary>
    /// Model for Clients import
    /// </summary>
    public class ClientsDBModel
    {
        [Required]
        public int? AccountCode { get; set; }

        [Required]
        [StringLength(30)]
        public string? CustomerAddress1 { get; set; }

        [StringLength(30)]
        public string? CustomerAddress2 { get; set; }

        [StringLength(30)]
        public string? CustomerAddress3 { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; } 

        [Required]
        [StringLength(30)]
        public string? CustomerFirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string? CustomerMiddleName { get; set; }

        [Required]
        [StringLength(30)]
        public string? CustomerLastName { get; set;}

        [StringLength(20)]
        public string? City { get; set; }

        [StringLength(20)]
        public string? State { get; set; }

        [StringLength(10)]
        public string? ZipCode { get; set; }

        [StringLength(40)]
        public string? EmailAddress { get; set; }

        [StringLength(12)]
        public string? CellPhone { get; set; }

        [StringLength(5)]
        public string? Residency { get; set; }

        [StringLength(35)]
        public string? KPP { get; set; }

        [StringLength(35)]
        public string? OKATO { get; set; }

        [StringLength(5)]
        public string? CountryCode { get; set; }

        [StringLength(30)]
        public string? Office_Name1 { get; set; }

        [StringLength(30)]
        public string? Office_Name2 { get; set; }

        [StringLength(30)]
        public string? Office_Name3 { get; set; }

        public bool? IsClosed { get; set; }

        public DateTime? CloseDate { get; set; }
    }
}
