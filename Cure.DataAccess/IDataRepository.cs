using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal interface IDataRepository : IDisposable
    {
        ViewUserMembership GetUserMembership(string username);
        IEnumerable<ViewUserMembership> ViewUserMembership();
        void UpdateUserMembership(ViewUserMembership userMembership);

        bool CheckStopVisit(int departmentId, DateTime dateFrom, DateTime dateTo);
        IEnumerable<StopVisit> GetStopVisitsForDepartment(int departmentId);
        IEnumerable<StopVisit> GetStopVisits();
        void InsertStopVisit(StopVisit stopVisit);
        void DeleteStopVisit(StopVisit stopVisit);
        void UpdateStopVisit(StopVisit stopVisit);

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

        bool CheckChildHideFile(int childId, string fileName);
        IEnumerable<ChildHideFile> GetChildHideFiles(int childId);
        void InsertChildHideFile(ChildHideFile childHideFile);
        void DeleteChildHideFile(ChildHideFile childHideFile);

        IEnumerable<ViewChild> FilterChilds(int countryId, string regionName, int ageOption, int diagnozeId, int skipRecords);
        int CountChilds(int countryId, string regionName, int ageOption, int diagnozeId);
        IEnumerable<ViewChild> ViewChilds();
        ViewChild ViewChild(int id);
        ViewChild ViewChild(string ownerUser);
        IEnumerable<Child> GetChilds();
        IEnumerable<Child> GetChilds(int countryId);
        void InsertChild(Child child);
        void DeleteChild(Child child);
        void UpdateChild(Child child);

        IEnumerable<RefBank> GetRefBanks();
        IEnumerable<RefBank> GetRefBanks(int countryId);
        void InsertRefBank(RefBank refBank);
        void DeleteRefBank(RefBank refBank);
        void UpdateRefBank(RefBank refBank);

        IEnumerable<RefOperator> GetRefOperators();
        IEnumerable<RefOperator> GetRefOperators(int countryId);
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
