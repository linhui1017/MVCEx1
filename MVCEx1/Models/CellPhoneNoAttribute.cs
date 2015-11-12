// Kfsyscc
// Copyright (c) Kfsyscc. All rights reserved.
//
// UniformNoAttribute.cs
//
// 黃林輝(Linhui)      2015-11-12 - Creation
//
// 中華民國統一編號檢查
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MVCEx1.Models
{
	public class CellPhoneNoAttribute : DataTypeAttribute
	{
		public CellPhoneNoAttribute()
			: base(DataType.Text)
		{
			this.ErrorMessage = "手機號碼錯誤";

		}

		public override bool IsValid(object value)
		{
			//^( 0 )(9)([0-9]{8})$

			string text = value.ToString();
			string number = @"^(0)(9)\d{2}\d{6}$";
												  
			// Instantiate the regular expression object.
			Regex r = new Regex(number, RegexOptions.IgnoreCase);

			// Match the regular expression pattern against a text string.
			Match m = r.Match(text);


			return m.Success;
		}



	}
}