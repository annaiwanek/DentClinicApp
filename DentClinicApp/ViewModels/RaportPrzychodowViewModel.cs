using DentClinicApp.Helper;
using DentClinicApp.Models.BusinessLogic;
using DentClinicApp.Models.DTO;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class RaportPrzychodowViewModel : WorkspaceViewModel
    {
        private DentCareEntities db = new DentCareEntities();

        public RaportPrzychodowViewModel()
        {
            base.DisplayName = "Raport przychodów";

            DataOd = DateTime.Now.AddMonths(-1);
            DataDo = DateTime.Now;

            var list = new UslugaB(db).GetUslugiKeyAndValueItems().ToList();
            list.Insert(0, new KeyAndValue { Key = 0, Value = "Wszystkie" });
            UslugiCombo = list;

            IdUslugi = 0;
            ListaPrzychodow = new ObservableCollection<PrzychodDto>();
            SeriesPrzychody = new SeriesCollection();

            ObliczPrzychody();
        }

        #region Właściwości
        private DateTime _dataOd;
        public DateTime DataOd
        {
            get => _dataOd;
            set { _dataOd = value; OnPropertyChanged(nameof(DataOd)); }
        }

        private DateTime _dataDo;
        public DateTime DataDo
        {
            get => _dataDo;
            set { _dataDo = value; OnPropertyChanged(nameof(DataDo)); }
        }

        private int _idUslugi;
        public int IdUslugi
        {
            get => _idUslugi;
            set { _idUslugi = value; OnPropertyChanged(nameof(IdUslugi)); }
        }

        private decimal _lacznyPrzychod;
        public decimal LacznyPrzychod
        {
            get => _lacznyPrzychod;
            set { _lacznyPrzychod = value; OnPropertyChanged(nameof(LacznyPrzychod)); }
        }

        public ObservableCollection<PrzychodDto> ListaPrzychodow { get; set; }
        public System.Collections.Generic.List<KeyAndValue> UslugiCombo { get; set; }
        public SeriesCollection SeriesPrzychody { get; set; }
        public string[] LabelsPrzychody { get; set; }
        #endregion

        #region Komendy
        public ICommand ObliczCommand => new BaseCommand(ObliczPrzychody);
        public ICommand EksportPdfCommand => new BaseCommand(GenerujPdf);
        #endregion

        #region Metody
        private void ObliczPrzychody()
        {
            var przychodB = new PrzychodB(db);
            var list = przychodB.GetPrzychodyOkresUslug(IdUslugi, DataOd, DataDo);

            ListaPrzychodow.Clear();
            foreach (var item in list)
            {
                ListaPrzychodow.Add(item);
            }

            LacznyPrzychod = ListaPrzychodow.Sum(x => x.Kwota);
            GenerujWykres();
        }

        private void GenerujWykres()
        {
            if (ListaPrzychodow.Any())
            {
                LabelsPrzychody = ListaPrzychodow.Select(x => x.UslugaNazwa).ToArray();
                var values = new LiveCharts.ChartValues<decimal>(ListaPrzychodow.Select(x => x.Kwota));

                SeriesPrzychody.Clear();
                SeriesPrzychody.Add(new ColumnSeries
                {
                    Title = "PLN",
                    Values = values,
                    DataLabels = true
                });
            }
            else
            {
                LabelsPrzychody = null;
                SeriesPrzychody.Clear();
            }
        }

        private void GenerujPdf()
        {
            if (!ListaPrzychodow.Any())
            {
                MessageBox.Show("Brak danych do wygenerowania PDF", "Brak", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"RaportPrzychodow_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
                };

                if (dlg.ShowDialog() == true)
                {
                    using (var fs = new FileStream(dlg.FileName, FileMode.Create))
                    {
                        Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
                        PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                        writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);

                        doc.Open();
                        BaseFont bf = BaseFont.CreateFont("c:/windows/fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        Font normalFont = new Font(bf, 12);
                        Font titleFont = new Font(bf, 18, Font.BOLD);

                        doc.Add(new Paragraph("RAPORT PRZYCHODÓW", titleFont) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 20 });
                        doc.Add(new Paragraph($"Okres: {DataOd:dd-MM-yyyy} - {DataDo:dd-MM-yyyy}", normalFont));
                        doc.Add(new Paragraph($"Usługa: {(IdUslugi == 0 ? "Wszystkie" : UslugiCombo.FirstOrDefault(x => x.Key == IdUslugi)?.Value)}", normalFont));
                        doc.Add(new Paragraph($"Łączny przychód: {LacznyPrzychod} PLN", normalFont));
                        doc.Add(new Chunk("\n"));

                        PdfPTable table = new PdfPTable(2) { WidthPercentage = 100, SpacingBefore = 10 };
                        table.SetWidths(new float[] { 70f, 30f });
                        table.AddCell(new PdfPCell(new Phrase("Usługa", normalFont)) { BackgroundColor = BaseColor.LIGHT_GRAY });
                        table.AddCell(new PdfPCell(new Phrase("Kwota (PLN)", normalFont)) { BackgroundColor = BaseColor.LIGHT_GRAY });

                        foreach (var row in ListaPrzychodow)
                        {
                            table.AddCell(new PdfPCell(new Phrase(row.UslugaNazwa, normalFont)));
                            table.AddCell(new PdfPCell(new Phrase(row.Kwota.ToString("F2"), normalFont)));
                        }
                        doc.Add(table);
                        doc.Close();
                        writer.Close();
                    }
                    MessageBox.Show("PDF wygenerowany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd generowania PDF: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}