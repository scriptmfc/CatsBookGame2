using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FurnitureChartData
{
    public int id;
    public string name;
    public FurnitureType type; // chair, shelf, desk..
    public string attribute; // basic, summer, winter, ..
    public GoodsId goodsType; // gold, cash
    public int price;
    public int width;
    public int length;

    public void setData(string _id, string _name, string _type, string _attribute, string _price, string _width, string _length, string _goodsType)
    {
        id = int.Parse(_id);
        name = _name;
        type = (FurnitureType)Enum.Parse(typeof(FurnitureType), _type);
        goodsType = (GoodsId)Enum.Parse(typeof(GoodsId), _goodsType);
        attribute = _attribute;
        price = int.Parse(_price);
        width = int.Parse(_width);
        length = int.Parse(_length);

    }

    public void setData(EasyExcelGenerated.FurnitureChart _furniture)
    {
        id = _furniture.ID;
        name = _furniture.Name;
        type = (FurnitureType)Enum.Parse(typeof(FurnitureType), _furniture.Type);
        goodsType = (GoodsId)Enum.Parse(typeof(GoodsId), _furniture.GoodsType);
        attribute = _furniture.Attribute;
        price = int.Parse(_furniture.Price);
        width = int.Parse(_furniture.Width);
        length = int.Parse(_furniture.Length);

    }

}
