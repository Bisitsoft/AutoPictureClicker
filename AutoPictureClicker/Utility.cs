using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;

namespace AutoPictureClicker
{
    public static class Utility
    {
        public static bool CheckMatSource(string path)
        {
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                return false;
            }
            try
            {
                new Mat(path);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #region ===AllSerialize===
        //https://www.cnblogs.com/snowcmx/archive/2013/04/15/3022292.html

        /// <summary> 
        /// 序列化 
        /// </summary> 
        /// <param name="data">要序列化的对象</param> 
        /// <returns>返回存放序列化后的数据缓冲区</returns> 
        public static byte[] Serialize(object data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream rems = new MemoryStream();
            formatter.Serialize(rems, data);
            return rems.GetBuffer();
        }

        /// <summary> 
        /// 反序列化 
        /// </summary> 
        /// <param name="data">数据缓冲区</param> 
        /// <returns>对象</returns> 
        public static object Deserialize(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream rems = new MemoryStream(data);
            data = null;
            return formatter.Deserialize(rems);
        }
        #endregion
    }
}
