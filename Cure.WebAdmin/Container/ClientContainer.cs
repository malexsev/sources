namespace Cure.WebAdmin.Logic
{
    using System;
    using System.Linq;
    using DataAccess;
    using DataAccess.BLL;
    using DataAccess.Enums;

    public class ClientContainer
    {
        public Order NewOrder { get; set; }
        public Order CurrentOrder { get; set; }

        private readonly string _userName;
        private readonly DataAccessBL dataAccess = new DataAccessBL();
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="user"></param>
        public ClientContainer(string user)
        {
            this._userName = user;
            this.RefreshData();
        }

        public void RemovePacient(int visitId)
        {
            var visit = dataAccess.GetVisit(visitId);
            if (visit != null)
            {
                int? pacientIdToRemove = null;
                if (visit.Pacient.Visits.Count() == 1)
                {
                    pacientIdToRemove = visit.PacientId;
                }
                dataAccess.DeleteVisit(visit);
                if (pacientIdToRemove != null)
                {
                    dataAccess.DeletePacient(pacientIdToRemove.Value);
                }
                RefreshData();
            }
        }

        public void RemoveSputnik(int sputnikId)
        {
            var sputnik = dataAccess.GetSputnik(sputnikId);
            if (sputnik != null)
            {
                dataAccess.DeleteSputnik(sputnik);
                RefreshData();
            }
        }

        public void UpdatePacient(Pacient pacient)
        {
            dataAccess.UpdatePacient(pacient);
            RefreshData();
        }

        public void UpdateSputnik(Sputnik sputnik)
        {
            dataAccess.UpdateSputnik(sputnik);
            RefreshData();
        }

        public void UpdateCurrentOrder()
        {
            dataAccess.UpdateOrder(this.CurrentOrder);
            RefreshData();
        }

        public void Save()
        {
            dataAccess.UpdateOrder(this.NewOrder);
        }

        /// <summary>
        /// Обновляет содержимое контейнера из базы данных
        /// </summary>
        public void RefreshData()
        {
            // Новый Заказ
            this.NewOrder = dataAccess.GetOrderDraft(_userName);
            if (this.NewOrder == null)
            {
                this.NewOrder = new Order
                {
                    CreateDate = DateTime.Today,
                    LastDate = DateTime.Today,
                    StatusId = (int)OrderStatus.Черновик,
                    OwnerUser = _userName,
                    LastUser = _userName,
                    DepartmentId = 3,
                };
                dataAccess.InsertOrder(this.NewOrder);
            }

            //Текущий заказ. Статусы: 
            this.CurrentOrder = dataAccess.GetOrderCurrent(_userName);
        }

        public bool FillFromPrevious()
        {
            if (this.CurrentOrder != null)
            {
                for (int i = 0; i == this.NewOrder.Visits.Count - 1; i++)
                {
                    dataAccess.DeleteVisit(this.NewOrder.Visits.ToList()[i]);
                }
                for (int i = 0; i == this.NewOrder.Sputniks.Count - 1; i++)
                {
                    dataAccess.DeleteSputnik(this.NewOrder.Sputniks.ToList()[i]);
                }
                this.RefreshData();

                this.NewOrder.DepartmentId = this.CurrentOrder.DepartmentId;
                this.NewOrder.Description = this.CurrentOrder.Description;
                this.NewOrder.Name = this.CurrentOrder.Name;
                this.NewOrder.ServicePekinIsHotel = this.CurrentOrder.ServicePekinIsHotel;
                this.NewOrder.ServicePekinIsPerevod = this.CurrentOrder.ServicePekinIsPerevod;
                this.NewOrder.ServicePekinOther = this.CurrentOrder.ServicePekinOther;
                this.NewOrder.ServiceRoomIsMilo = this.CurrentOrder.ServiceRoomIsMilo;
                this.NewOrder.ServiceRoomIsOpolask = this.CurrentOrder.ServiceRoomIsOpolask;
                this.NewOrder.ServiceRoomIsPaper = this.CurrentOrder.ServiceRoomIsPaper;
                this.NewOrder.ServiceRoomIsPosuda = this.CurrentOrder.ServiceRoomIsPosuda;
                this.NewOrder.ServiceRoomIsStiral = this.CurrentOrder.ServiceRoomIsStiral;
                this.NewOrder.ServiceRoomIsVoda = this.CurrentOrder.ServiceRoomIsVoda;
                this.NewOrder.ServiceUnchenIsVstrecha = this.CurrentOrder.ServiceUnchenIsVstrecha;
                this.NewOrder.ServiceUnchenOther = this.CurrentOrder.ServiceUnchenOther;

                foreach (Sputnik sputnik in this.CurrentOrder.Sputniks)
                {
                    var copy = new Sputnik()
                    {
                        BirthDate = sputnik.BirthDate,
                        Contacts = sputnik.Contacts,
                        Email = sputnik.Email,
                        CreateUser = sputnik.CreateUser,
                        CreateDate = sputnik.CreateDate,
                        Familiya = sputnik.Familiya,
                        FamiliyaEn = sputnik.FamiliyaEn,
                        IsPrimary = sputnik.IsPrimary,
                        Name = sputnik.Name,
                        Otchestvo = sputnik.Otchestvo,
                        OrderId = this.NewOrder.Id,
                        NameEn = sputnik.NameEn,
                        RodstvoId = sputnik.RodstvoId,
                        SeriaNumber = sputnik.SeriaNumber,
                        OwnerUser = sputnik.OwnerUser
                    };
                    this.NewOrder.Sputniks.Add(copy);
                }
                foreach (Visit visit in this.CurrentOrder.Visits)
                {
                    var copy = new Visit()
                    {
                        Additional = visit.Additional,
                        Appetit = visit.Appetit,
                        Alergiya = visit.Alergiya,
                        DangerousDiseases = visit.DangerousDiseases,
                        Diet = visit.Diet,
                        Dihalka = visit.Dihalka,
                        Dispanser = visit.Dispanser,
                        DispanserNarko = visit.DispanserNarko,
                        Dispanser2 = visit.Dispanser2,
                        Eating = visit.Eating,
                        EatingProblems = visit.EatingProblems,
                        Encefalogram = visit.Encefalogram,
                        Epilispiya = visit.Epilispiya,
                        Fisical = visit.Fisical,
                        Fiznagruzki = visit.Fiznagruzki,
                        Fond = visit.Fond,
                        Hirurg = visit.Hirurg,
                        HystoryA = visit.HystoryA,
                        Hystoryb = visit.Hystoryb,
                        Imunitet = visit.Imunitet,
                        Infections = visit.Infections,
                        KursesChinaRanee = visit.KursesChinaRanee,
                        KursesRanee = visit.KursesRanee,
                        MainGoal = visit.MainGoal,
                        NonTradicial = visit.NonTradicial,
                        OtherDiseases = visit.OtherDiseases,
                        PacientId = visit.PacientId,
                        Razgovor = visit.Razgovor,
                        Instructcii = visit.Instructcii,
                        Razvitie = visit.Razvitie,
                        ProstupUp = visit.ProstupUp,
                        Remission = visit.Remission,
                        RequiredDocs = visit.RequiredDocs,
                        Serdce = visit.Serdce,
                        Son = visit.Son,
                        Requirements = visit.Requirements,
                        Stul = visit.Stul,
                        SudorogiCount = visit.SudorogiCount,
                        SudorogiMedcine = visit.Requirements,
                        SudorogiType = visit.SudorogiType,
                        TodaysDiagnoz = visit.TodaysDiagnoz,
                        Travmi = visit.Travmi
                    };
                    this.NewOrder.Visits.Add(copy);
                }
                this.Save();
                this.RefreshData();
            }
            return false;
        }
    }
}