using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCEx1.Models
{
	public partial class 客戶資料 : IValidatableObject
	{

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (!string.IsNullOrEmpty(this.統一編號)) 
			{
				CustomerEntities db = new CustomerEntities();
				var 客戶資料 = db.客戶資料
					.Where(x=>x.Id != this.Id
					&& x.統一編號.Trim() == this.統一編號.Trim()
					&& x.刪除註記 == false)
					.FirstOrDefault();
				if (null != 客戶資料)
					yield return new ValidationResult("Uniform NO. Already Exists", new[] { "統一編號" });

			}
			//List<ValidationResult> results = new List<ValidationResult>();
			//results.Add(new ValidationResult("Prop1 must be larger than Prop2"));
			//return results;
		}
	}
}