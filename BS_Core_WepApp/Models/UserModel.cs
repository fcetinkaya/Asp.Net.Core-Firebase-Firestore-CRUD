using Google.Cloud.Firestore;
using System;

namespace BS_Core_WepApp.Models
{
    [FirestoreData]
    public class UserModel:IDisposable
    {
        public string Id { get; set; }
        public DateTime date { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public string Surname { get; set; }
        [FirestoreProperty]
        public string Gender { get; set; }
        [FirestoreProperty]
        public string logoPath { get; set; }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}