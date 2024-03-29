﻿using System.ComponentModel.DataAnnotations;

namespace CostControl.BusinessEntity.Models.CostControl
{
    public class Ingredient : Base.Interfaces.IEntity<long>
    {
        public long Id { get; set; }

        public System.Guid? InstanceId { get; set; }

        public Base.Enums.ObjectState State { get; set; }

        [Required(ErrorMessage = "نام اجباریست!")]
        [Display(Name = "نام")]
        public string Name { get; set; }


        [Required(ErrorMessage = "کد اجباریست!")]
        [Display(Name = "کد")]
        public string Code { get; set; }


        [Required(ErrorMessage = "نام لاتین اجباریست!")]
        [Display(Name = "نام لاتین")]
        public string EnglishName { get; set; }


        [Required(ErrorMessage = "نوع اجباریست!")]
        [Display(Name = "نوع")]
        public Enums.IngredientType Type { get; set; }


        [Required(ErrorMessage = "قیمت اجباریست!")]
        [Display(Name = "قیمت")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "نرخ اجباریست!")]
        [Display(Name = "نرخ")]
        public decimal UsefullRatio { get; set; }

        [Display(Name = "شرح")]
        public string Description { get; set; }
    }
}