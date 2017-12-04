
namespace Cure.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using Cure.Utils;
    using DataAccess;
    using DataAccess.BLL;
    using Datasets;
    using DevExpress.Office.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraReports.UI;
    using Models;
    using Resouces;

    public partial class OrderInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly DataAccessBL dataAccess = new DataAccessBL();
        private Visit visit;
        private List<OrderInvoiceRow> rows;
        private long totalUan;

        public OrderInvoice(int visitId, List<OrderInvoiceRow> details, long totalUan)
        {
            var dal = new DataAccessBL();
            this.visit = dal.GetVisit(visitId);
            this.rows = details;
            this.totalUan = totalUan;
            if (this.visit != null)
            {
                InitializeComponent();
            }
        }

        private void OrderInvoice_DataSourceDemanded(object sender, EventArgs e)
        {
            DataSource = GenerateTable();
        }

        private void uxPacientNameHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = string.Format("{0} {1}", this.visit.Pacient.FamiliyaEn, this.visit.Pacient.NameEng);
        }

        private void uxDocNumber_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.visit.Id.ToString();
        }

        private void uxDocDate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }

        private void uxPacientNameFull_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = string.Format("{0} {1}, {2} date of birth, Nationality: {3}, passport №{4}",
                this.visit.Pacient.FamiliyaEn,
                this.visit.Pacient.NameEng,
                this.visit.Pacient.BirthDate.HasValue ? this.visit.Pacient.BirthDate.Value.ToString("dd-MM-yyyy") : "-",
                this.visit.Pacient.RefCountry.NameEn,
                this.visit.Pacient.SerialNumber);
        }

        private void uxPacientDiagnoz_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.visit.TodaysDiagnoz;
        }

        private void uxOrderDays_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var dateSpan = this.visit.Order.DateTo - this.visit.Order.DateFrom;
            var label = (XRLabel)sender;
            label.Text = string.Format("{0} дней с {1} по {2}", dateSpan.Days.ToString(), this.visit.Order.DateFrom.ToString("dd-MM-yyyy"), this.visit.Order.DateTo.ToString("dd-MM-yyyy"));
        }

        private void uxSputnikDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var sputnik = this.visit.Order.Sputniks.FirstOrDefault(x => x.IsPrimary) ?? (this.visit.Order.Sputniks.FirstOrDefault() ?? new Sputnik());
            var label = (XRLabel)sender;
            if (sputnik != null)
            {
                label.Text = string.Format("Сопровождающий - {0}: {1} {2}, {3}, {4}, passport No {5}.",
                    sputnik.RefRodstvo == null ? "" : sputnik.RefRodstvo.Name,
                    sputnik.FamiliyaEn,
                    sputnik.NameEn,
                    sputnik.BirthDate.HasValue ? sputnik.BirthDate.Value.ToString("dd-MM-yyyy") : "-",
                    sputnik.RefCountry.NameEn,
                    sputnik.SeriaNumber);
            }

        }

        private DataTable GenerateTable()
        {
            var table = new DataTable("Table");
            table.Columns.Add("Description");
            table.Columns.Add("Amount");
            table.Columns.Add("CostPerOne");
            table.Columns.Add("Price");
            foreach (var data in this.rows)
            {
                DataRow row = table.NewRow();
                row["Description"] = data.Description;
                row["Amount"] = data.Amount;
                row["CostPerOne"] = data.CostPerOne;
                row["Price"] = data.Price;
                table.Rows.Add(row);
            }
            return table;
        }

        private void uxName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = GetCurrentColumnValue("Description").ToString();
        }

        private void uxAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = GetCurrentColumnValue("Description").ToString() ==
                "Организационнные услуги (переводчик, коммуникации, и тд)" ? string.Empty : 
                (GetCurrentColumnValue("Amount").ToString() == "0" 
                    ? string.Empty 
                    : GetCurrentColumnValue("Amount").ToString());
        }

        private void xrPrice_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = GetCurrentColumnValue("Price").ToString();
        }

        private void uxCostPerOne_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = GetCurrentColumnValue("Description").ToString() ==
                         "Организационнные услуги (переводчик, коммуникации, и тд)" ? string.Empty : GetCurrentColumnValue("CostPerOne").ToString();
        }

        private void uxLineTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            label.Text = this.totalUan.ToString();
        }

        private void uxTotalTextUan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            var total = this.totalUan;
            label.Text = string.Format("{0}", Utils.RuDateAndMoneyConverter.NumeralsToTxt(total, TextCase.Accusative, false, true));
        }

        private void uxTotalTextUsd_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            var usdRate = (double)GetRate("USD");
            var cnyRate = (double)GetRate("CNY");
            var total = (this.totalUan * cnyRate) / usdRate;
            label.Text = string.Format("{0}", Utils.RuDateAndMoneyConverter.NumeralsToTxt(Convert.ToInt32(Math.Round(total, 0)), TextCase.Accusative, false, true));
        }

        private void uxLineTotalPendo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            var usdRate = (double)GetRate("USD");
            var cnyRate = (double)GetRate("CNY");
            var total = (this.totalUan * cnyRate) / usdRate;
            label.Text = string.Format("{0}", Math.Round(total, 0));
        }

        //private void uxLineTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    var label = (XRLabel)sender;
        //    label.Text = this.prices.Sum(x => x).ToString();
        //}

        //private void uxTotalTextUan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    var label = (XRLabel)sender;
        //    var total = this.prices.Sum(x => x);
        //    label.Text = string.Format("{0} юаней", Utils.RuDateAndMoneyConverter.NumeralsToTxt(total, TextCase.Accusative, false, true)) ;
        //}

        //private void uxTotalTextUsd_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    var dal = new DataAccessBL();
        //    var label = (XRLabel)sender;
        //    var usdRate = (double)GetRate("USD");
        //    var cnyRate = (double)GetRate("CNY");
        //    var total = (this.prices.Sum(x => x) * cnyRate) / usdRate;
        //    label.Text = string.Format("{0} долларов США", Math.Round(total, 0).ToString());

        //}

        private decimal GetRate(string currency)
        {
            var dal = new DataAccessBL();
            var rate = dal.GetCurrencyRates().FirstOrDefault(x => x.CurrencyFrom == currency);
            if (rate != null)
            {
                return Math.Round(rate.Rate, 2);
            }
            else
            {
                return 0;
            }
        }

        private void xrPictureBox3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var pictureBox = (XRPictureBox)sender;
            pictureBox.Image = getClinic1Picture(this.visit.Order.DepartmentId ?? 3);
        }

        private void xrPictureBox4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var pictureBox = (XRPictureBox)sender;
            pictureBox.Image = getSign1(this.visit.Order.DepartmentId ?? 3);
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var pictureBox = (XRPictureBox)sender;
            pictureBox.Image = getTopPicture1(this.visit.Order.DepartmentId ?? 3);
        }

        private Image getClinic1Picture(int departmentId)
        {
            switch (departmentId)
            {
                case 3:
                    {
                        var rnd = new Random();
                        switch (rnd.Next(1, 8))
                        {
                            case 1:
                                {
                                    return InvoiceResources.clinic1;
                                }
                            case 2:
                                {
                                    return InvoiceResources.clinic2;
                                }
                            case 3:
                                {
                                    return InvoiceResources.clinic3;
                                }
                            case 4:
                                {
                                    return InvoiceResources.clinic4;
                                }
                            case 5:
                                {
                                    return InvoiceResources.clinic5;
                                }
                            case 6:
                                {
                                    return InvoiceResources.clinic6;
                                }
                            case 7:
                                {
                                    return InvoiceResources.clinic7;
                                }
                            default:
                                {
                                    return InvoiceResources.clinic8;
                                }
                        }
                    }
                case 4:
                    {
                        return InvoiceResources.Pechat_bzgm;
                    }
                default:
                    {
                        return InvoiceResources.Pechat_bzgm;
                    }
            }
        }

        private Image getSign1(int departmentId)
        {
            switch (departmentId)
            {
                case 3:
                    {
                        var rnd = new Random();
                        switch (rnd.Next(1, 10))
                        {
                            case 1:
                                {
                                    return InvoiceResources.sign1;
                                }
                            case 2:
                                {
                                    return InvoiceResources.sign2;
                                }
                            case 3:
                                {
                                    return InvoiceResources.sign3;
                                }
                            case 4:
                                {
                                    return InvoiceResources.sign4;
                                }
                            case 5:
                                {
                                    return InvoiceResources.sign5;
                                }
                            case 6:
                                {
                                    return InvoiceResources.sign6;
                                }
                            case 7:
                                {
                                    return InvoiceResources.sign7;
                                }
                            case 8:
                                {
                                    return InvoiceResources.sign8;
                                }
                            case 9:
                                {
                                    return InvoiceResources.sign9;
                                }
                            default:
                                {
                                    return InvoiceResources.sign10;
                                }
                        }
                    }
                case 4:
                    {
                        return InvoiceResources.Podpis_bzgm;
                    }
                default:
                    {
                        return InvoiceResources.Podpis_bzgm;
                    }
            }
        }

        private Image getTopPicture1(int departmentId)
        {
            switch (departmentId)
            {
                case 3:
                    {
                        return InvoiceResources.top2;
                    }
                case 4:
                    {
                        return InvoiceResources.top1;
                    }
                default:
                    {
                        return InvoiceResources.top1;
                    }
            }
        }

        private void uxExamplePoluchatel_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            switch (this.visit.Order.DepartmentId)
            {
                case 3: //Вторая
                    {
                        label.Text = "YUNCHENG CITY SECOND PEOPLE'S HOSPITAL OF YANHU DISTRICT";
                        break;
                    }
                case 4: //БЗГМ
                    {
                        label.Text = "YUNCHENG RESEARCH INSTITUTE OF SCALP ACUPUNCTURE AFFILIATED WITH THE BRAIN DISEASE HOSPITAL";
                        break;
                    }
                default:
                    {
                        label.Text = "<нет данных>";
                        break;
                    }
            }
        }

        private void uxExampleBank_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            switch (this.visit.Order.DepartmentId)
            {
                case 3: //Вторая
                    {
                        label.Text = "FAVOURING CHINA CONSTRUCTION BANK SHANXI BRACH";
                        break;
                    }
                case 4: //БЗГМ
                    {
                        label.Text = "BANK OF CHINA YUNCHENG BRANCH";
                        break;
                    }
                default:
                    {
                        label.Text = "<нет данных>";
                        break;
                    }
            }
        }

        private void uxExampleNomer_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            switch (this.visit.Order.DepartmentId)
            {
                case 3: //Вторая
                    {
                        label.Text = "14014720100220501065";
                        break;
                    }
                case 4: //БЗГМ
                    {
                        label.Text = "142976605563";
                        break;
                    }
                default:
                    {
                        label.Text = "<нет данных>";
                        break;
                    }
            }
        }

        private void uxExampleSwift_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            switch (this.visit.Order.DepartmentId)
            {
                case 3: //Вторая
                    {
                        label.Text = "PCBCCNBJIXA";
                        break;
                    }
                case 4: //БЗГМ
                    {
                        label.Text = "BKCHCNBJ680";
                        break;
                    }
                default:
                    {
                        label.Text = "<нет данных>";
                        break;
                    }
            }
        }

        private void uxExampleAddress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            switch (this.visit.Order.DepartmentId)
            {
                case 3: //Вторая
                    {
                        label.Text = "NO.126 YINGZE STREET, TAIYUAN 030001, SHANXI CHINA (FAX: 0351-4957565)";
                        break;
                    }
                case 4: //БЗГМ
                    {
                        label.Text = "№39 ZHONGYIN ROAD YUNCHENG, SHANXI, CHINA";
                        break;
                    }
                default:
                    {
                        label.Text = "<нет данных>";
                        break;
                    }
            }
        }

        private void uxOrderDetail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            switch (this.visit.Order.DepartmentId)
            {
                case 3: //Вторая
                    {
                        label.Text = "Для прохождения курса лечение в реабилитационное отделение по лечению ДЦП второй многопрофильной";
                        break;
                    }
                case 4: //БЗГМ
                    {
                        label.Text = "Для прохождения курса лечение в реабилитационное отделение по лечению ДЦП Больницы";
                        break;
                    }
                default:
                    {
                        label.Text = "<нет данных>";
                        break;
                    }
            }
        }

        private void uxOrderDetail2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = (XRLabel)sender;
            switch (this.visit.Order.DepartmentId)
            {
                case 3: //Вторая
                    {
                        label.Text = "народной больницы района Еньху, города Юньчэн, провинция Шаньси, КНР,";
                        break;
                    }
                case 4: //БЗГМ
                    {
                        label.Text = "заболеваний головного мозга при НИИ акупунктуры головы, города Юньчэн, провинция Шаньси, КНР,";
                        break;
                    }
                default:
                    {
                        label.Text = "<нет данных>";
                        break;
                    }
            }
        }

        private void uxGlavniy_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            var label = (XRLabel)sender;
            label.Text = (this.visit.Order.DepartmentId ?? 3) == 3 ? "Hou Zheng Min" : "Zhao Ji Wei";
        }
    }
}
