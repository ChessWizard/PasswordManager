namespace PasswordManager.Blazor.Constants
{
    public static class ToasterResponseMessages
    {
        public struct Success
        {
            public const string CopiedSuccessfully = "Kopyalama işlemi başarılı!";

            public const string FavoritePasswordDeleted = "Şifre silindi!";
        }

        public struct Warning
        {
            public const string PasswordAlreadySaved = "Bu şifre zaten favorilerde bulunuyor!";
        }

        public struct Error
        {
            public const string NotFoundFavoritePasswordForDelete = "Silinmek üzere herhangi kayıtlı bir şifre bulunamadı!";
        }
    }
}
