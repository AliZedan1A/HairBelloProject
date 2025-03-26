

namespace ClientSide.Services.Class
{
    public class PhoneNumberService
    {
        private const string PhoneNumberKey = "UserPhoneNumber";

        // حفظ الرقم
        public void SavePhoneNumber(string phoneNumber)
        {
            Preferences.Set(PhoneNumberKey, phoneNumber);
        }

        // جلب الرقم إذا كان محفوظًا، وإرجاع null إذا لم يكن موجودًا
        public string? GetPhoneNumber()
        {
            return Preferences.Get(PhoneNumberKey, null);
        }

        // حذف الرقم
        public void DeletePhoneNumber()
        {
            Preferences.Remove(PhoneNumberKey);
        }

        // التحقق مما إذا كان الرقم موجودًا
        public bool IsPhoneNumberSaved()
        {
            return GetPhoneNumber() != null;
        }
    }
}
