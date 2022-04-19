using LitJson;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using GameUtils;
using EasyExcel;
using EasyExcelGenerated;
using System;
using System.Linq;

public class ChartDataLoad : MonoBehaviour
{
    private string version = "1.0.0";
    private enum TableList
    {
        FurnitureChart,
    }


    private class DataLoadPath : IEEDataLoader
    {
        public EERowDataCollection Load(string sheetClassName)
        {
            var headName = sheetClassName;
            var filePath = EESettings.Current.GeneratedAssetPath.
                               Substring(EESettings.Current.GeneratedAssetPath.IndexOf("Resources/", StringComparison.Ordinal) + "Resources/".Length)
                           + headName;
            var collection = Resources.Load(filePath) as EERowDataCollection;
            return collection;
        }
    }
    private EEDataManager eeDataManager;

    private bool isLoading = false;
    private int tableRowCount = 0;
    private int loadingProgress = 0;

    private string loadingStr = "데이터 불러오는중";
    private string dev = "";


    //private void Start()
    //{
    //    //versionText.text = "ver " + version;
    //    // use ExampleDataLoaderResources as loader


    //    // use ExampleDataLoaderAssetBundle as loader
    //    //_eeDataManager = new EEDataManager(new ExampleDataLoaderAssetBundle());

    //    // Set CustomDataLoader before load.
    //}

    public void setDataInit()
    {
        eeDataManager = new EEDataManager(new DataLoadPath());
        eeDataManager.Load();
        getTableList();
    }

    public void getTableList()
    {

        var assembly = EEUtility.GetSourceAssembly();
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(EERowDataCollection)));
        var sheetClassTypes = types as Type[] ?? types.ToArray();

        if (sheetClassTypes.Any())
        {
            foreach (var sheetClassType in sheetClassTypes)
            {
                var collectClassName = sheetClassType.FullName;
                var headParts = collectClassName.Split('.');
                var fileName = headParts.Length == 1 ? null : headParts[0].Substring(EESettings.Current.NameSpacePrefix.Length);
                var sheetClassName = headParts.Length == 1 ? headParts[0] : headParts[1];
                var sheetName = EESettings.Current.GetSheetName(sheetClassType);
                var rowDataClassName = EESettings.Current.GetRowDataClassName(fileName, sheetName, true);
                var rowType = assembly.GetType(rowDataClassName);
                var dic = eeDataManager.GetList(rowType);

                getChartContents(sheetName, rowDataClassName, rowType, dic);

                CGameUtils.WaitForSeconds(0.1f);

            }

        }

    }


    public void getChartContents(string _sheetName, string _rowDataClassName, Type rowType, List<EERowData> _data)
    {
        if(_sheetName == TableList.FurnitureChart.ToString())
        {
            getFurnitureChartList(_data, _rowDataClassName);
        }
        else
        {
            Debug.Log(_sheetName + "Not Match with ChartDataLoad Script");
        }

    }

    public void getFurnitureChartList(List<EERowData> _data, string _rowDataClassName)
    {
        for(int i=0; i<_data.Count; i++)
        {
            FurnitureChartData data = new FurnitureChartData();
            data.setData(_data[i] as EasyExcelGenerated.FurnitureChart);
                
            GameManagerTemp.Instance.liFurniture.Add(data);

            //삭제필요 (저장하는 데이터 임시) 
            GameManagerTemp.Instance.liExteriorItem.Add(data);
        }

    }

}
