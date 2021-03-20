using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AutoPictureClicker
{
    public static class AllCopy
    {
        //https://www.cnblogs.com/Soar1991/archive/2012/11/04/2754319.html
        public static TChild CopyFromParent<TParent, TChild>(TParent parent) where TChild : TParent, new()
        {
            TChild child = new TChild();
            var ParentType = typeof(TParent);
            var Properties = ParentType.GetProperties();
            foreach (var Propertie in Properties)
            {
                //循环遍历属性
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    //进行属性拷贝
                    Propertie.SetValue(child, Propertie.GetValue(parent, null), null);
                }
            }
            return child;
        }

        public static void CopyTo<T>(T origin, T target)
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(target, propertyInfo.GetValue(origin), null);
                }
            }
        }
    }

    //https://www.cnblogs.com/chenwolong/p/MemberwiseClone.html
    ///// <summary>
    ///// 通用的深复制方法
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //[Serializable]
    //public class BaseClone<T>
    //{
    //    public virtual T Clone()
    //    {
    //        MemoryStream memoryStream = new MemoryStream();
    //        BinaryFormatter formatter = new BinaryFormatter();
    //        formatter.Serialize(memoryStream, this);
    //        memoryStream.Position = 0;
    //        return (T)formatter.Deserialize(memoryStream);
    //
    //    }
    //}
}
