using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal interface IDataRepository : IDisposable
    {
        IEnumerable<ViewRecipient> GetContacts(string username, string newContact);
        IEnumerable<Message> GetMyMessages(string username, string contact);
        IEnumerable<Message> GetMessages();
        Message GetMessage(int messageId);
        void InsertMessage(Message message);
        void DeleteMessage(Message message);
        void UpdateMessage(Message message);
        int GetUnreadCount(string username);

        IEnumerable<OnlineUser> GetOnlineUsers();
        void InsertOnlineUser(OnlineUser onlineUser);
        void DeleteOnlineUser(OnlineUser onlineUser);
        void UpdateOnlineUser(OnlineUser onlineUser);
        void BringOnlineUser(string username);
        void ClearOnlineUsers();

        Weather GetWeatherByCity(int cityId);
        IEnumerable<Weather> GetWeathers();
        void InsertWeather(Weather weather);
        void DeleteWeather(Weather weather);
        void UpdateWeather(Weather weather);

        IEnumerable<CurrencyRate> GetCurrencyRates();
        CurrencyRate GetCurrencyRate(string curFrom, string curTo, DateTime date);
        void InsertCurrencyRate(CurrencyRate currencyRate);
        void DeleteCurrencyRate(CurrencyRate currencyRate);
        void UpdateCurrencyRate(CurrencyRate currencyRate);

        IEnumerable<Currency> GetCurrencies();
        void InsertCurrency(Currency currency);
        void DeleteCurrency(Currency currency);
        void UpdateCurrency(Currency currency);

        ViewUserMembership GetUserMembership(string username);
        IEnumerable<ViewUserMembership> ViewUserMembership();
        void UpdateUserMembership(ViewUserMembership userMembership);

        IEnumerable<Newsletter> GetNewsletters();
        void InsertNewsletter(Newsletter newsletter);
        void DeleteNewsletter(Newsletter newsletter);
        void UpdateNewsletter(Newsletter newsletter);

        IEnumerable<ViewMension> ViewMensions(int filterId, int skipRecords, int takeRecords = 12);
        int CountMensions(int filterId);
        IEnumerable<Mension> GetMensions();
        IEnumerable<Mension> GetTopMensions();
        IEnumerable<Mension> GetMensionsByDepartment(int? department);
        void InsertMension(Mension mension);
        void DeleteMension(Mension mension);
        void UpdateMension(Mension mension);

        bool CheckStopVisit(int departmentId, DateTime dateFrom, DateTime dateTo);
        IEnumerable<StopVisit> GetStopVisitsForDepartment(int departmentId);
        IEnumerable<StopVisit> GetStopVisits();
        void InsertStopVisit(StopVisit stopVisit);
        void DeleteStopVisit(StopVisit stopVisit);
        void UpdateStopVisit(StopVisit stopVisit);

        IEnumerable<Post> GetMyPosts(int childId);
        IEnumerable<Post> GetPosts();
        Post GetPost(int postId);
        void InsertPost(Post post);
        void DeletePost(Post post);
        void UpdatePost(Post post);

        IEnumerable<Vipiska> GetMyVipiskas(string username);
        Vipiska GetVipiska(int visitId);
        void InsertVipiska(Vipiska vipiska);
        void DeleteVipiska(Vipiska vipiska);
        void UpdateVipiska(Vipiska vipiska);

        IEnumerable<RefStopVisitType> GetRefStopVisitTypes();
        void InsertRefStopVisitType(RefStopVisitType refStopVisitType);
        void DeleteRefStopVisitType(RefStopVisitType refStopVisitType);
        void UpdateRefStopVisitType(RefStopVisitType refStopVisitType);

        IEnumerable<NotificationLog> GetNotificationLogs();
        void InsertNotificationLog(NotificationLog notificationLog);
        void DeleteNotificationLog(NotificationLog notificationLog);
        void UpdateNotificationLog(NotificationLog notificationLog);

        IEnumerable<RefDiagnoz> GetExistingDiagnozs();
        IEnumerable<RefDiagnoz> GetRefDiagnozs();
        void InsertRefDiagnoz(RefDiagnoz refDiagnoz);
        void DeleteRefDiagnoz(RefDiagnoz refDiagnoz);
        void UpdateRefDiagnoz(RefDiagnoz refDiagnoz);

        ChildAvaFile GetChildAvaFile(int childId);
        void InsertChildAvaFile(ChildAvaFile childAvaFile);
        void DeleteChildAvaFile(ChildAvaFile childAvaFile);

        bool CheckChildHideFile(int childId, string fileName);
        IEnumerable<ChildHideFile> GetChildHideFiles(int childId);
        void InsertChildHideFile(ChildHideFile childHideFile);
        void DeleteChildHideFile(ChildHideFile childHideFile);
        void DeleteChildHideFile(int childId, string fileName);

        IEnumerable<ViewChild> FilterChilds(int countryId, string regionName, int ageOption, int diagnozeId, int skipRecords, int takeRecords = 12);
        int CountChilds(int countryId, string regionName, int ageOption, int diagnozeId);
        IEnumerable<ViewChild> ViewChilds();
        ViewChild ViewChild(int id);
        ViewChild ViewChild(string ownerUser);
        IEnumerable<Child> GetChilds();
        IEnumerable<Child> GetChilds(int countryId);
        Child GetChild(int Id);
        void InsertChild(Child child);
        void DeleteChild(Child child);
        void UpdateChild(Child child);

        IEnumerable<RefBank> GetRefBanks();
        IEnumerable<RefBank> GetRefBanks(int countryId);
        void InsertRefBank(RefBank refBank);
        void DeleteRefBank(RefBank refBank);
        void UpdateRefBank(RefBank refBank);

        IEnumerable<RefOperator> GetRefOperators();
        void InsertRefOperator(RefOperator refOperator);
        void DeleteRefOperator(RefOperator refOperator);
        void UpdateRefOperator(RefOperator refOperator);

        IEnumerable<Setting> GetSettings();
        Setting GetSettingByCode(string code);
        void InsertSetting(Setting setting);
        void DeleteSetting(Setting setting);
        void UpdateSetting(Setting setting);

        IEnumerable<SmsLog> GetSmsLogs();
        void InsertSmsLog(SmsLog smsLog);
        void DeleteSmsLog(SmsLog smsLog);
        void UpdateSmsLog(SmsLog smsLog);

        IEnumerable<ViewScheduler> GetScheduler();

        IEnumerable<string> GetRegions();
        IEnumerable<string> GetRegions(int countryId);
        IEnumerable<RefCountry> GetRefCountries();
        RefCountry GetRefCountry(int countryId);
        void InsertRefCountry(RefCountry country);
        void DeleteRefCountry(RefCountry country);
        void UpdateRefCountry(RefCountry country);

        IEnumerable<RefRodstvo> GetRefRodstvo();
        void InsertRefRodstvo(RefRodstvo rodstvo);
        void DeleteRefRodstvo(RefRodstvo rodstvo);
        void UpdateRefRodstvo(RefRodstvo rodstvo);

        IEnumerable<Department> GetDepartments();
        void InsertDepartment(Department department);
        void DeleteDepartment(Department department);
        void UpdateDepartment(Department department);

        void SwitchOrderStatusTask();
        Order GetOrder(int id);
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrders(int filter, string email, string familiya);
        IEnumerable<Order> GetMyOrders(string username);
        IEnumerable<ViewSoonVisit> ViewSoonOrders(int filter);
        IEnumerable<ViewSoonVisit> ViewSoonTransferOrders(string username);
        IEnumerable<ViewSoonVisit> ViewOutdatedStatus();
        IEnumerable<ViewSoonVisit> ViewCurrentVisits(int departmentId);
        Order GetOrderDraft(string username);
        Order GetOrderCurrent(string username);
        void InsertOrder(Order order);
        void DeleteOrder(Order order);
        void UpdateOrder(Order order);

        IEnumerable<OrderStatu> GetOrderStatus();
        void InsertOrderStatu(OrderStatu orderStatu);
        void DeleteOrderStatu(OrderStatu orderStatu);
        void UpdateOrderStatu(OrderStatu orderStatu);

        IEnumerable<Pacient> GetPacients();
        IEnumerable<Pacient> GetPacients(string email);
        IEnumerable<Pacient> GetPacients(int orderId);
        void InsertPacient(Pacient pacient);
        void DeletePacient(Pacient pacient);
        void DeletePacient(int pacientId);
        void UpdatePacient(Pacient pacient);

        IEnumerable<Sputnik> GetSputniks();
        Sputnik GetSputnik(int sputnikId);
        IEnumerable<Sputnik> GetOrderSputniks(int orderId);
        void InsertSputnik(Sputnik sputnik);
        void DeleteSputnik(Sputnik sputnik);
        void UpdateSputnik(Sputnik sputnik);

        IEnumerable<Visit> GetVisits();
        IEnumerable<Visit> GetOrderVisits(int orderId);
        IEnumerable<Visit> GetVisitsForTimespan(DateTime fromTime, DateTime toTime);
        Visit GetVisit(int visitId);
        void InsertVisit(Visit visit);
        void DeleteVisit(Visit visit);
        void UpdateVisit(Visit visit);


    }
}
