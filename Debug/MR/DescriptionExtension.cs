using System;
using System.ComponentModel;
using System.Reflection;

/// <summary>
/// 描述特性的读取扩展类
/// </summary>
public static class DescriptionExtension {
    /*
     * 使用方法
        //获取类的描述特性
        string str3 = typeof(TestClass).GetDescription();

        //获取属性的描述特性（也可以反射遍历属性列表）
        string str4 = typeof(TestClass).GetDescription();
        TestClass test = new TestClass();
        str4 = typeof(TestClass).GetDescription(nameof(test.Test1));

     */
    /// <summary>
    /// 获取枚举的描述信息
    /// </summary>
    public static string GetDescription(this Enum em) {
        Type type = em.GetType();
        FieldInfo fd = type.GetField(em.ToString());
        string des = fd.GetDescription();
        return des;
    }



    /// <summary>
    /// 获取属性的描述信息
    /// </summary>
    public static string GetDescription(this Type type, string proName) {
        PropertyInfo pro = type.GetProperty(proName);
        string des = proName;
        if(pro != null) {
            des = pro.GetDescription();
        }
        return des;
    }



    /// <summary>
    /// 获取属性的描述信息
    /// </summary>
    public static string GetDescription(this MemberInfo info) {
        var attrs = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
        string des = info.Name;
        foreach(DescriptionAttribute attr in attrs) {
            des = attr.Description;
        }
        return des;
    }
}