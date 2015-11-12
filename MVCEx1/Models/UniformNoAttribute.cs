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
using System.Web;

namespace MVCEx1.Models
{
	public class UniformNoAttribute : DataTypeAttribute
	{
		public UniformNoAttribute()
			: base(DataType.Text)
		{
			this.ErrorMessage = "統一編號錯誤";

		}

		public override bool IsValid(object value)
		{
			return UniformNoAttribute.IsValid(value.ToString());
		}


		public static bool IsValid(string bano)
		{
			int i = 0;
			int c1, c2, c3, c4, a1, a2, a3, a4, b1, b2, b3, b4, a5 = 0;
			bool bResult = false;

			int[] cArray = new int[8];

			if (bano.Trim().Length != 8) { return false; }

			char[] arrs = bano.Trim().ToCharArray();

			int j = 0;
			foreach (char c in arrs)
			{
				int number = -1;

				bool result = Int32.TryParse(c.ToString(), out number);

				if (!result || number < 0)
				{
					return false;
				}
				else
				{
					cArray[j] = number;
				}

				j = j + 1;

			}
			c1 = cArray[0];

			a1 = (cArray[1] * 2) / 10;
			b1 = (cArray[1] * 2) % 10;
	
			c2 = cArray[2];

			a2 = (cArray[3] * 2) / 10;
			b2 = (cArray[3] * 2) % 10;

			c3 = cArray[4];

			a3 = (cArray[5] * 2) / 10;
			b3 = (cArray[5] * 2) % 10;


			a4 = (cArray[6] * 4) / 10;
			b4 = (cArray[6] * 4) % 10;

			c4 = cArray[7];

			i = (c1 + c2 + c3 + c4 + a1 + a2 + a3 + a4 + b1 + b2 + b3 + b4) % 10;

			if (i == 0) { bResult = true; }

			if (cArray[6] == 7)
			{
				a5 = (a4 + b4) % 10;
				i = ((a1 + b1 + c1 + a2 + b2 + c2 + a3 + b3 + c3 + a5 + c4) % 10);

				if (i == 0)
				{
					bResult = true;
				}
				else
				{
					bResult = false;
				}
			}

			return bResult;
		}

	}
}