using Core.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInValid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem Bakımda" ;
        public static string ProductListed="Ürünler Listelendi";
        public static string ProductCountOfCategoryError = "Aynı katagoride zaten 10 ürün var";
        public static string ProductNameError = "Aynı isimde ürün var";
        public static string MoreThanFifteen = "Kategori 15'i geçtiği için daha fazla ürün eklenemez";
        public static string AuthorizationDenied = "Yetkilendirme rededildi";
        internal static string UserRegistered = "Kayıt başarılı";
        internal static string UserNotFound = "Kullanıcı bulunamadı";
        internal static string PasswordError = "Şifre Yanlış";
        internal static string SuccessfulLogin = "Başarıyla giriş yapıldı";
        internal static string UserAlreadyExists = "Kulanıcı zaten kayıtlı";
        internal static string AccessTokenCreated = "Giriş Tokeni Oluşturuldu";
    }
}
