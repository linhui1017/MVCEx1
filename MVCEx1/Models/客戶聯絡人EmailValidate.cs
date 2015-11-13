using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCEx1.Models
{
	public partial class 客戶聯絡人 : IValidatableObject
	{

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (this.客戶Id >= 0) 
			{
				CustomerEntities db = new CustomerEntities();
				var 客戶聯絡人 = db.客戶聯絡人
					.Where(x => x.客戶Id == this.客戶Id &&
						x.刪除註記 == false &&
						x.Id != this.Id &&
						x.Email.ToLower() == this.Email.ToLower())
					.FirstOrDefault();
				if(null != 客戶聯絡人)
					yield return new ValidationResult("Email Already Exists", new[] { "Email" });

			}
			//List<ValidationResult> results = new List<ValidationResult>();
			//results.Add(new ValidationResult("Prop1 must be larger than Prop2"));
			//return results;
		}
	}
}