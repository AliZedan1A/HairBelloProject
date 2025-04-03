using ClientSide;

public class LocalizationService
{
    public event Action OnLanguageChanged;

    public string CurrentLanguage { get; private set; } = StaticValues.LangSelected;

    private readonly Dictionary<string, Dictionary<string, string>> _translations = new()
    {
        {
            "ar", new Dictionary<string, string>
            {
                {"Title", "للإستشارة"},
                {"Call", "اتصل بنا"},
                {"reminder-title", "لديك حجز قادم"},
                {"reminder-Day", "اليوم:"},
                {"LoginNav", "تسجيل الدخول"},
                {"HomeAlert-CantBocking", "عليك تسجيل رقمك قبل حجز دور - هذا الاجراء يتم اتخاذه مرة واحدة"},
                {"reminder-Time", ":الوقت"},
                {"reminder-Price", "السعر:"},
                {"reminder-countdown", "المتبقّي"},
                {"cancel-order", "إلغاء الحجز"},
                {"SHIKL", "شيكل"},
                {"cancel-confirmation", "هل أنت متأكد من إلغاء الحجز"},
                {"confirm-btn", "نعم"},
                {"cancel-btn", "لا"},
                {"section-heading", "HairBello صالون واكاديمية  للحلاقة "},
                {"NoBarbers", "لا يوجد حلاقين للعرض"},
                {"barber", "حلاق"},
                {"Mints", "دقيقة"},
                {"selected-info-totalprice", "التكلفة الكلية:"},
                {"selected-info-totalTime", "الوقت الكلي: "},
                {"OrderNow", "احجز الآن"},
                {"selected-info", "انت تمتلك حجز بالفعل , اٍنتظر حتى قدوم موعد حجزك"},
                {"Place", "محلقة"},
                {"Waze", "افتح الموقع في"},
                {"Chose-BarberDate", "اختار الحلاق لعرض مواعيد عمله:"},
                {"Loading-Barber", "جاري تحميل قائمة الحلاقين..."},
                {"BarberDates", "جدول مواعيد الحلاق:"},
                {"Day-About", "اليوم"},
                {"From-Time", "من الساعة"},
                {"End-Time", "الى الساعة"},
                {"No-BarberTable", "لا يوجد جدول مواعيد مسجل لهذا الحلاق"},
                {"Set-PhonNumber-Name", "أدخل رقم هاتفك واسمك"},
                {"PhonNumber", "رقم الهاتف:"},
                {"PhonNumber-Change", "تغيير الرقم"},
                {"ConfirmCode", "تأكيد الكود"},
                {"EnterPhon", "أدخل رقم هاتفك واسمك"},
                {"Sending", "جاري الإرسال..."},
                {"SendValdCode", "إرسال كود التحقق"},
                {"Use-BackCode", "استخدام كود سابق"},
                {"placeholder-Exampil", "مثال"},
                {"placeholder-EnterName", "ادخل اسمك"},
                {"placeholder-Code", "أدخل الكود"},
                {"AvailbleTimes", "مواعيد الحجز"},
                {"back", "رجوع"},
                {"Loading", "...جاري التحميل"},
                {"LoadTimes", "لا يوجد أوقات متاحة في هذا اليوم."},
                {"ComfirmOrder", "أكد الطلب"},
                {"video-gallery-Title", "معرض الفيديوهات"},
                {"video-gallery-Error", "متصفحك لا يدعم تشغيل الفيديو."},
                {"video-gallery-NoVed", "لا توجد فيديوهات حالياً."},
                //alerts
                {"HomeAlert-Cansel", "تم الغاء الحجز بنجاح" },
                {"HomeAlert-ErrorOrder", "حصل خطأ اثناء محاولة الغاء الحجز , قد لا تتمكن من الغاء الحجز قبل بساعتين" },
                {"HomeAlert-FaildBarberFitch", "فشل في جلب الحلاقين المتوفرين" },
                {"HomeAlert-CantDoCall", "لا يمكن إجراء المكالمة على هذا الجهاز" },
                {"HomeAlert-sucss", "تم انشاء الحجز بنجاح" },
                {"LoginAlert-Waiting", "يرجى الانتظار قبل إعادة إرسال الكود" },
                {"LoginAlert-PhonError", "رقم الهاتف يجب أن يبدأ بـ '05' ويحتوي على 10 أرقام" },
                {"LoginAlert-InputError", "يجب إدخال رقم الهاتف والاسم" },
                {"LoginAlert-Info", "قد يستغرق وصول الكود من 2 إلى 20 ثانية. إذا لم يصلك الكود، يمكنك طلب كود جديد خلال دقيقة" },
                {"LoginAlert-FaildCodeVal", "فشل إرسال كود التحقق. الرجاء إعادة التسجيل" },
                {"LoginAlert-InputCodeError", "يرجى إدخال الكود" },
                {"LoginAlert-ErrorCode", "الكود غير صحيح. حاول مرة أخرى" },
                //nav
                {"nav-home", "الرئيسية"},
                {"nav-Welcome", "مرحباً"},
                {"nav-Admin", "لوحة الإدارة"},
                {"nav-About", "عن المحلقة"},
                { "Settings-Title", "الإعدادات" },
                { "DeleteAccount", "حذف الحساب" },
                { "Logout", "تسجيل الخروج" },
                { "DeleteConfirmText", "اٍذا كنت متأكد من أنك تريد حذف الحساب - اٍضغط مرة اخرى" },
                { "DeleteSuccess", "لقد تم إلغاء تفعيل حسابك - يمكنك الذهاب وإعادة تسجيل الدخول" },
                { "DeleteFail", "فشل إرسال طلب حذف الحساب - قم بالتواصل مع مالك التطبيق" },
                { "LogoutSuccess", "لقد تم تسجيل خروجك من الحساب - يمكنك إعادة تسجيل الدخول" },
                { "SettingNav", "الاعدادات" }


            }
        },
        {
            "he", new Dictionary<string, string>
            {
                {"Title", "להתייעצות"},
                {"reminder-title", "יש לך הזמנה קרובה"},
                {"reminder-Day", "היום:"},
                {"reminder-Time", ":שעה"},
                {"reminder-Price", "המחיר:"},
                {"reminder-countdown", "נותרו"},
                {"LoginNav", "התחברות"},
                {"cancel-order", "ביטול ההזמנה"},
                {"SHIKL", "שקל"},
                {"cancel-confirmation", "האם אתה בטוח שברצונך לבטל את ההזמנה?"},
                {"confirm-btn", "כן"},
                {"cancel-btn", "לא"},
                {"HomeAlert-CantBocking", "עליך לרשום את מספרך לפני קביעת תור - פעולה זו מתבצעת פעם אחת בלבד"},
                {"section-heading", "HairBello מספרה ואקדמיה  לעיצוב שיער"},
                {"NoBarbers", "אין ספרים להצגה"},
                {"barber", "ספר"},
                {"Mints", "דקה"},
                {"selected-info-totalprice", "העלות הכוללת:"},
                {"selected-info-totalTime", "הזמן הכולל:"},
                {"OrderNow", "הזמן עכשיו"},
                {"selected-info", "כבר יש לך הזמנה, המתן עד למועד ההזמנה שלך"},
                {"Place", "מספרה"},
                {"Waze", "פתח את המיקום ב"},
                {"Chose-BarberDate", "בחר את הספר כדי להציג את מועדי העבודה שלו:"},
                {"Loading-Barber", "טוען את רשימת הספרים..."},
                {"BarberDates", "לוח הזמנים של הספר:"},
                {"Day-About", "היום"},
                {"From-Time", "מהשעה"},
                {"End-Time", "עד השעה"},
                {"No-BarberTable", "אין לוח זמנים רשום לספר זה"},
                {"Set-PhonNumber-Name", "הזן את מספר הטלפון ושמך"},
                {"PhonNumber", "מספר הטלפון:"},
                {"PhonNumber-Change", "שנה את המספר"},
                {"ConfirmCode", "אישור הקוד"},
                {"EnterPhon", "הזן את מספר הטלפון ושמך"},
                {"Sending", "שולח..."},
                {"SendValdCode", "שלח קוד אימות"},
                {"Use-BackCode", "השתמש בקוד קודם"},
                {"placeholder-Exampil", "דוגמה"},
                {"placeholder-EnterName", "הזן את שמך"},
                {"placeholder-Code", "הזן את הקוד"},
                {"AvailbleTimes", "זמני הזמנה"},
                {"back", "חזור"},
                {"Loading", "...טוען"},
                {"LoadTimes", "אין זמנים זמינים ביום זה."},
                {"ComfirmOrder", "אשר את ההזמנה"},
                {"video-gallery-Title", "גלריית הווידאו"},
                {"video-gallery-Error", "הדפדפן שלך אינו תומך בניגון וידאו."},
                {"video-gallery-NoVed", "אין סרטונים כעת."},
                 //alerts
                {"HomeAlert-Cansel", "הזמנתך בוטלה בהצלחה"},
                {"HomeAlert-ErrorOrder", "אירעה שגיאה במהלך ביטול ההזמנה, ייתכן שלא ניתן לבטל את ההזמנה כשנותרו שעתיים בלבד."},
                {"HomeAlert-FaildBarberFitch", "נכשל באחזור הספרים הזמינים"},
                {"HomeAlert-CantDoCall", "לא ניתן לבצע שיחה במכשיר זה"},
                {"HomeAlert-sucss", "ההזמנה נוצרה בהצלחה"},
                {"LoginAlert-Waiting", "אנא המתן לפני שליחת הקוד מחדש"},
                {"LoginAlert-PhonError", "מספר הטלפון חייב להתחיל ב-'05' ולהכיל 10 ספרות"},
                {"LoginAlert-InputError", "יש להזין מספר טלפון ושם"},
                {"LoginAlert-Info", "ייתכן שהקוד יגיע בתוך 2 עד 20 שניות. אם לא קיבלת את הקוד, תוכל לבקש קוד חדש בתוך דקה"},
                {"LoginAlert-FaildCodeVal", "נכשל בשליחת קוד האימות. אנא הירשם מחדש"},
                {"LoginAlert-InputCodeError", "אנא הזן את הקוד"},
                {"LoginAlert-ErrorCode", "הקוד שגוי. נסה שוב"},
                  //nav
                {"nav-home", "דף הבית"},
                {"nav-Welcome", "ברוך הבא"},
                {"nav-Admin", "לוח הניהול"},
                {"nav-About", "אודות המספרה"},
                { "Settings-Title", "הגדרות" },
                { "DeleteAccount", "מחיקת החשבון" },
                { "Logout", "התנתקות" },
                { "DeleteConfirmText", "אם אתה בטוח שברצונך למחוק את החשבון - לחץ שוב" },
                { "DeleteSuccess", "החשבון שלך בוטל - תוכל להתחבר מחדש בכל עת" },
                { "DeleteFail", "נכשל בבקשת מחיקת החשבון - אנא פנה לבעל האפליקציה" },
                { "LogoutSuccess", "התנתקת מהחשבון - תוכל להתחבר מחדש" },
                { "SettingNav", "הגדרות" }

                }
        }
    };

    public string this[string key] => _translations[CurrentLanguage].TryGetValue(key, out var value) ? value : key;

    public void SetLanguage(string language)
    {
        if (_translations.ContainsKey(language))
        {
            CurrentLanguage = language;
            StaticValues.LangSelected = language;
            OnLanguageChanged?.Invoke();
        }
    }
}
