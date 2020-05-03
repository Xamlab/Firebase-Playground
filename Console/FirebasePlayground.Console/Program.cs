using System.Net.Http;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace FirebasePlayground.Console
{
    class Program
    {
        private static FirebaseApp _firebaseApp;

        static async Task Main(string[] args)
        {
            using var httpClient = new HttpClient();

            do
            {
                await SendNotification();
            } while(System.Console.ReadLine() != "exit");
        }
        

        private static async Task SendNotification()
        {
            _firebaseApp ??= FirebaseApp.Create(new AppOptions
                                                 {
                                                     Credential = GoogleCredential.FromFile("firebase-service-account.json")
                                                 }, "fir-playground-d5e07");;
           
 
            var fcm = FirebaseMessaging.GetMessaging(_firebaseApp);
            Message message = new Message
                              {
                                  Notification = new Notification
                                                 {
                                                     Title = "My push notification title",
                                                     Body = "Content for this push notification"
                                                 },

                                  Token = "fNITDY5xq0tSv-IYAJ8JIC:APA91bGNnK0ApIbEMVsHOPt5jMIxmqWn-svxEWLLxuH0qKgW30StR6eT-jzH9blPwNd9z3y_-LUhXxvepPe3S4kD6x8WK25THQ-UIliRc24HMd1L9xCHQ5zY6X1eUKUfn4EVviXMIrZ9"
                              };
            
            var result = await fcm.SendAsync(message);
            System.Console.WriteLine($"Push notification completed with result {result}");
        }
    }
}