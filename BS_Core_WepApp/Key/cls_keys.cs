using System;

namespace BS_Core_WepApp.Key
{
    public class cls_keys : IDisposable
    {
        public static string ApiKey = "AIzaSyCovYj5Q_2MrSlZ4q0v1gBhvYXsLoFg-NQ";
        public static string Bucket = "nodejs-tutorial-4dbfe-default-rtdb.europe-west1.firebasedatabase.app";
        public static string BucketFile = "nodejs-tutorial-4dbfe.appspot.com";
        public static string projectId = "nodejs-tutorial-4dbfe";

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}