using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Manager
{
    /// <summary>
    /// 단일 객체 패턴
    /// </summary>
    /// <typeparam name="T">단일로 가지고 있을 클래스</typeparam>
    public class Singleton<T> : IDisposable where T : class, new()
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = new T();

                return instance;
            }
        }

        public static void DestroyInstance()
        {
            if (instance == null)
                return;

            instance = null;
        }

        public virtual void Dispose()
        {

        }
    }
}