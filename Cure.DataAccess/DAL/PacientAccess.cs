using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public IEnumerable<Pacient> GetPacients(int orderId)
        {
            return context.Pacients.Where(o => o.Visits.Select(x => x.OrderId).Contains(orderId)).ToList();
        }

        public IEnumerable<Pacient> GetPacients()
        {
            return context.Pacients.OrderBy(o => o.Familiya).ThenBy(x => x.Name).ToList();
        }

        public IEnumerable<Pacient> GetPacients(string email)
        {
            int[] sputnikIDs = { };
            string[] userIDs = { };
            if (!string.IsNullOrEmpty(email))
            {
                sputnikIDs = context.Sputniks.Where(x => x.Email.ToLower() == email.ToLower()).Select(x => x.Id).ToArray();
                userIDs = context.ViewUserMemberships.Where(x => x.Email.ToLower() == email.ToLower()).Select(x => x.UserName).ToArray();
            }

            return context.Pacients.Where(x => string.IsNullOrEmpty(email) || x.Visits.Any(v => v.Order.Sputniks.Any(s => sputnikIDs.Contains(s.Id))) || x.Visits.Any(v => userIDs.Contains(v.Order.OwnerUser)) || userIDs.Contains(x.OwnerUser)).OrderByDescending(o => o.CreateDate).ToList();
        }

        public Pacient GetPacient(int id)
        {
            return context.Pacients.FirstOrDefault(o => o.Id == id);
        }

        public void InsertPacient(Pacient pacient)
        {
            try
            {
                context.Pacients.AddObject(pacient);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePacient(Pacient pacient)
        {
            try
            {
                context.Pacients.Attach(pacient);
                context.Pacients.DeleteObject(pacient);
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePacient(int pacientId)
        {
            var pacient = GetPacient(pacientId);
            DeletePacient(pacient);
        }

        public void UpdatePacient(Pacient pacient)
        {
            try
            {
                var origPacient = GetPacient(pacient.Id);
                origPacient.Anamnez = pacient.Anamnez;
                origPacient.BirthDate = pacient.BirthDate;
                origPacient.CityName = pacient.CityName;
                origPacient.CountryId = pacient.CountryId;
                origPacient.CreateDate = pacient.CreateDate;
                origPacient.CreateUser = pacient.CreateUser;
                origPacient.Diagnoz = pacient.Diagnoz;
                origPacient.Familiya = pacient.Familiya;
                origPacient.FamiliyaEn = pacient.FamiliyaEn;
                origPacient.LastDate = pacient.LastDate;
                origPacient.LastUser = pacient.LastUser;
                origPacient.Name = pacient.Name;
                origPacient.NameEng = pacient.NameEng;
                origPacient.Otchestvo = pacient.Otchestvo;
                origPacient.OtchestvoEn = pacient.OtchestvoEn;
                origPacient.OwnerUser = pacient.OwnerUser;
                origPacient.SerialNumber = pacient.SerialNumber;
                SaveChanges();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
