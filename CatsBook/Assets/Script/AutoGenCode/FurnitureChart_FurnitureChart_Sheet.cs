﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EasyExcel.
//     Runtime Version: 4.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;
using EasyExcel;

namespace EasyExcelGenerated
{
	[Serializable]
	public class FurnitureChart : EERowData
	{
		[EEKeyField]
		[SerializeField]
		private int _ID;
		public int ID { get { return _ID; } }

		[SerializeField]
		private string _Attribute;
		public string Attribute { get { return _Attribute; } }

		[SerializeField]
		private string _Name;
		public string Name { get { return _Name; } }

		[SerializeField]
		private string _Width;
		public string Width { get { return _Width; } }

		[SerializeField]
		private string _Length;
		public string Length { get { return _Length; } }

		[SerializeField]
		private string _Price;
		public string Price { get { return _Price; } }

		[SerializeField]
		private string _GoodsType;
		public string GoodsType { get { return _GoodsType; } }

		[SerializeField]
		private string _Type;
		public string Type { get { return _Type; } }


		public FurnitureChart()
		{
		}

#if UNITY_EDITOR
		public FurnitureChart(List<List<string>> sheet, int row, int column)
		{
			TryParse(sheet[row][column++], out _ID);
			TryParse(sheet[row][column++], out _Attribute);
			TryParse(sheet[row][column++], out _Name);
			TryParse(sheet[row][column++], out _Width);
			TryParse(sheet[row][column++], out _Length);
			TryParse(sheet[row][column++], out _Price);
			TryParse(sheet[row][column++], out _GoodsType);
			TryParse(sheet[row][column++], out _Type);
		}
#endif
		public override void OnAfterSerialized()
		{
		}
	}

	public class FurnitureChart_FurnitureChart_Sheet : EERowDataCollection
	{
		[SerializeField]
		private List<FurnitureChart> elements = new List<FurnitureChart>();

		public override void AddData(EERowData data)
		{
			elements.Add(data as FurnitureChart);
		}

		public override int GetDataCount()
		{
			return elements.Count;
		}

		public override EERowData GetData(int index)
		{
			return elements[index];
		}

		public override void OnAfterSerialized()
		{
			foreach (var element in elements)
				element.OnAfterSerialized();
		}
	}
}
