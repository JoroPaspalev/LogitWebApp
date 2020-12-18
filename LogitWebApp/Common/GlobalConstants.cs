namespace LogitWebApp.Common
{
    public static class GlobalConstants
    {
        public const string GoogleReCaptchaSecretKey = "6LfTG-QZAAAAAGsc_gFr1r8i5ZTtUR6GPX6Ebe_e";

        public const string Admin_RoleName = "Admin";

        public const string User_RoleName = "User";

        public const string Driver_RoleName = "Driver";

        public const string Admin_Username = "Admin";

        public const string Admin_Pass = "Admin";

        public const string Admin_Email = "admin@abv.bg";

        public const string Admin_Phone = "+359888222777";

        public const string Admin_Names = "admin"; // This is equal to property Manager in ApplicationUser

        public const int countOfPalletsInOneTruck = 30;

        public const decimal costPerKilometer = 2M; //Costs are 2lv/km

        public const decimal profit = 2;

        public const string Driver_Added = "Вие успешно добавихте шофьор към базата данни!";

        public const string Driver_Deleted = "Вие успешно премахнахте шофьора от базата данни!";

        public const string Order_Deleted = "Вие успешно премахнахте поръчката от базата данни!";
    }
}
