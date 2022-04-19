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
	public class Character : EERowData
	{
		[EEKeyField]
		[SerializeField]
		private int _ID;
		public int ID { get { return _ID; } }

		[SerializeField]
		private string _Name;
		public string Name { get { return _Name; } }

		[SerializeField]
		private string _CharacterTitle;
		public string CharacterTitle { get { return _CharacterTitle; } }

		[SerializeField]
		private string _ImageName;
		public string ImageName { get { return _ImageName; } }

		[SerializeField]
		private string _CharacterOpenConditionType;
		public string CharacterOpenConditionType { get { return _CharacterOpenConditionType; } }

		[SerializeField]
		private int _CharacterOpenConditionValue;
		public int CharacterOpenConditionValue { get { return _CharacterOpenConditionValue; } }

		[SerializeField]
		private int _Attack;
		public int Attack { get { return _Attack; } }

		[SerializeField]
		private int _Hp;
		public int Hp { get { return _Hp; } }

		[SerializeField]
		private float _AttackSpeed;
		public float AttackSpeed { get { return _AttackSpeed; } }

		[SerializeField]
		private float _CriticalAddDamage;
		public float CriticalAddDamage { get { return _CriticalAddDamage; } }

		[SerializeField]
		private float _CriticalRate;
		public float CriticalRate { get { return _CriticalRate; } }

		[SerializeField]
		private float _MiracleDamge;
		public float MiracleDamge { get { return _MiracleDamge; } }

		[SerializeField]
		private float _MiracleRate;
		public float MiracleRate { get { return _MiracleRate; } }

		[SerializeField]
		private float _MoveSpeed;
		public float MoveSpeed { get { return _MoveSpeed; } }

		[SerializeField]
		private string _SkillGroup;
		public string SkillGroup { get { return _SkillGroup; } }


		public Character()
		{
		}

#if UNITY_EDITOR
		public Character(List<List<string>> sheet, int row, int column)
		{
			TryParse(sheet[row][column++], out _ID);
			TryParse(sheet[row][column++], out _Name);
			TryParse(sheet[row][column++], out _CharacterTitle);
			TryParse(sheet[row][column++], out _ImageName);
			TryParse(sheet[row][column++], out _CharacterOpenConditionType);
			TryParse(sheet[row][column++], out _CharacterOpenConditionValue);
			TryParse(sheet[row][column++], out _Attack);
			TryParse(sheet[row][column++], out _Hp);
			TryParse(sheet[row][column++], out _AttackSpeed);
			TryParse(sheet[row][column++], out _CriticalAddDamage);
			TryParse(sheet[row][column++], out _CriticalRate);
			TryParse(sheet[row][column++], out _MiracleDamge);
			TryParse(sheet[row][column++], out _MiracleRate);
			TryParse(sheet[row][column++], out _MoveSpeed);
			TryParse(sheet[row][column++], out _SkillGroup);
		}
#endif
		public override void OnAfterSerialized()
		{
		}
	}

	public class Character_Character_Sheet : EERowDataCollection
	{
		[SerializeField]
		private List<Character> elements = new List<Character>();

		public override void AddData(EERowData data)
		{
			elements.Add(data as Character);
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
