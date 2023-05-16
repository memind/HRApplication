using System.ComponentModel.DataAnnotations;

namespace IKApplication.Domain.Enums
{
    public enum ExpenseType
    {
        [Display(Name = "Food Expense")]
        FoodExpense = 1,
        [Display(Name = "Renting Expense")]
        RentingExpense = 2,
        [Display(Name = "Office Operating Expense")]
        OfficeOperatingExpense = 3,
        [Display(Name = "Advertising And Marketing Expense")]
        AdvertisingAndMarketingExpense = 4,
        [Display(Name = "Accounting Expense")]
        AccountingExpense = 5,
        [Display(Name = "Maintenance And Repair Expense")]
        MaintenanceAndRepairExpense = 6,
        [Display(Name = "Office Stuff Expense")]
        OfficeStuffExpense = 7,
        [Display(Name = "Lawyer Fee")]
        LawyerFee = 8,
        [Display(Name = "Vehicle Expense")]
        VehicleExpense = 9,
        [Display(Name = "Transportation Expense")]
        TransportationExpense = 10,
        [Display(Name = "Trip Expense")]
        TripExpense = 11,
        [Display(Name = "Notary Expense")]
        NotaryFee = 12,
        [Display(Name = "Hospitality Expense")]
        HospitalityExpense = 13
    }
}