using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using LiveCharts;
using LiveCharts.Wpf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity; 
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class RaportWizytViewModel : JedenViewModel<Wizyty>
    {
        private DateTime _odData;
        private DateTime _doData;

        private ObservableCollection<Pracownicy> _lekarze;
        private Pracownicy _wybranyLekarz;

        private ObservableCollection<Uslugi> _uslugi;
        private Uslugi _wybranaUsluga;

        private ObservableCollection<Wizyty> _wizytyFiltr;

        // Wykresy
        private SeriesCollection _liczbaWizytNaLekarza;
        private SeriesCollection _podzialUslug;
        private string[] _labelsLekarze;

        // Zaznaczona wizyta do wyświetlenia szczegółów
        private Wizyty _wybranaWizyta;

        public RaportWizytViewModel()
            : base("Raport wizyt")
        {
            // Domyślny zakres
            OdData = DateTime.Now.AddMonths(-1);
            DoData = DateTime.Now;

            WizytyFiltr = new ObservableCollection<Wizyty>();
            PodzialUslug = new SeriesCollection();

            using (var db = new DentCareEntities())
            {
                // Lekarze: dodajemy "Wszyscy"
                Lekarze = new ObservableCollection<Pracownicy>
                {
                    new Pracownicy { IdPracownika = 0, Imie = "Wszyscy", Nazwisko = "Wszyscy" }
                };
                foreach (var lekarz in db.Pracownicy.ToList())
                    Lekarze.Add(lekarz);

                // Usługi: dodajemy "Wszystkie"
                Uslugi = new ObservableCollection<Uslugi>
                {
                    new Uslugi { IdUslugi = 0, Nazwa = "Wszystkie" }
                };
                foreach (var usluga in db.Uslugi.ToList())
                    Uslugi.Add(usluga);
            }

            WybranyLekarz = Lekarze.First();
            WybranaUsluga = Uslugi.First();

            FiltrujWizyty();
            GenerujWykresy();
        }

        #region Właściwości

        public DateTime OdData
        {
            get => _odData;
            set
            {
                _odData = value;
                OnPropertyChanged(() => OdData);
            }
        }

        public DateTime DoData
        {
            get => _doData;
            set
            {
                _doData = value;
                OnPropertyChanged(() => DoData);
            }
        }

        public ObservableCollection<Pracownicy> Lekarze
        {
            get => _lekarze;
            set
            {
                _lekarze = value;
                OnPropertyChanged(() => Lekarze);
            }
        }

        public Pracownicy WybranyLekarz
        {
            get => _wybranyLekarz;
            set
            {
                _wybranyLekarz = value;
                OnPropertyChanged(() => WybranyLekarz);
            }
        }

        public ObservableCollection<Uslugi> Uslugi
        {
            get => _uslugi;
            set
            {
                _uslugi = value;
                OnPropertyChanged(() => Uslugi);
            }
        }

        public Uslugi WybranaUsluga
        {
            get => _wybranaUsluga;
            set
            {
                _wybranaUsluga = value;
                OnPropertyChanged(() => WybranaUsluga);
            }
        }

        public ObservableCollection<Wizyty> WizytyFiltr
        {
            get => _wizytyFiltr;
            set
            {
                _wizytyFiltr = value;
                OnPropertyChanged(() => WizytyFiltr);
                GenerujWykresy(); // << TERAZ wykresy zawsze się aktualizują
            }
        }

        // Statystyki
        public int LiczbaZnalezionychWizyt => WizytyFiltr.Count;
        public int SumarycznyCzasTrwania => WizytyFiltr.Sum(w => w.CzasTrwaniaWMinutach);

        // Wykresy
        public SeriesCollection LiczbaWizytNaLekarza
        {
            get => _liczbaWizytNaLekarza;
            set
            {
                _liczbaWizytNaLekarza = value;
                OnPropertyChanged(() => LiczbaWizytNaLekarza);
            }
        }

        public SeriesCollection PodzialUslug
        {
            get => _podzialUslug;
            set
            {
                _podzialUslug = value;
                OnPropertyChanged(() => PodzialUslug);
            }
        }

        // Etykiety osi X dla wykresu kolumnowego
        public string[] LabelsLekarze
        {
            get => _labelsLekarze;
            set
            {
                _labelsLekarze = value;
                OnPropertyChanged(() => LabelsLekarze);
            }
        }

        // Zaznaczona wizyta
        public Wizyty WybranaWizyta
        {
            get => _wybranaWizyta;
            set
            {
                _wybranaWizyta = value;
                OnPropertyChanged(() => WybranaWizyta);
            }
        }

        #endregion

        #region Komendy

        public ICommand FiltrujCommand => new BaseCommand(() =>
        {
            FiltrujWizyty();
            GenerujWykresy();
        });

        // Przyciski do szczegółów
        public ICommand PokazSzczegolyCommand => new BaseCommand(() =>
        {
            if (WybranaWizyta != null)
            {
                var lekarz = WybranaWizyta.Pracownicy;
                var usluga = WybranaWizyta.Uslugi;

                // Bez pacjenta, tylko lekarz, usługa, data, godzina, status, czas
                string msg =
                    $"Lekarz: {lekarz.Imie} {lekarz.Nazwisko}\n" +
                    $"Usługa: {usluga.Nazwa}\n\n" +
                    $"Data: {WybranaWizyta.Data:dd-MM-yyyy}\n" +
                    $"Godzina: {WybranaWizyta.Godzina:hh\\:mm}\n" +
                    $"Status: {WybranaWizyta.Status}\n" +
                    $"Czas trwania: {WybranaWizyta.CzasTrwaniaWMinutach} min";

                MessageBox.Show(msg, "Szczegóły wizyty", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Nie wybrano żadnej wizyty!",
                    "Brak wyboru",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        });

        public ICommand EksportujPdfCommand => new BaseCommand(() =>
        {
            GenerujPdf();
        });

        #endregion

        public override void Save() { /* nic */ }

        private void FiltrujWizyty()
        {
            using (var db = new DentCareEntities())
            {
                // Eager loading
                var query = db.Wizyty
                    .Include(w => w.Pracownicy)
                    .Include(w => w.Uslugi)
                    .Where(w => w.Data >= OdData && w.Data <= DoData);

                if (WybranyLekarz != null && WybranyLekarz.IdPracownika != 0)
                    query = query.Where(w => w.IdPracownika == WybranyLekarz.IdPracownika);

                if (WybranaUsluga != null && WybranaUsluga.IdUslugi != 0)
                    query = query.Where(w => w.IdUslugi == WybranaUsluga.IdUslugi);

                var wizyty = query.ToList();

                WizytyFiltr.Clear();
                foreach (var wizyta in wizyty)
                {
                    WizytyFiltr.Add(wizyta);
                }
            }

            OnPropertyChanged(() => LiczbaZnalezionychWizyt);
            OnPropertyChanged(() => SumarycznyCzasTrwania);
        }

        private void GenerujWykresy()
        {
            using (var db = new DentCareEntities())
            {
                // WYKRES KOLUMNOWY: Ilość wizyt na lekarza
                var wizytyNaLekarza = db.Wizyty
                    .GroupBy(w => w.Pracownicy.Nazwisko)
                    .Select(g => new { Lekarz = g.Key, Liczba = g.Count() })
                    .OrderBy(x => x.Lekarz)
                    .ToList();

                LabelsLekarze = wizytyNaLekarza.Select(x => x.Lekarz).ToArray();

                var values = new LiveCharts.ChartValues<int>(wizytyNaLekarza.Select(x => x.Liczba));
                var columnSeries = new ColumnSeries
                {
                    Title = "Wizyt",
                    Values = values,
                    DataLabels = true
                };
                LiczbaWizytNaLekarza = new SeriesCollection { columnSeries };

                // WYKRES KOŁOWY: Podział wizyt wg usługi
                var uslugiWizyty = db.Wizyty
                    .Where(w => w.Data >= OdData && w.Data <= DoData)
                    .GroupBy(w => w.Uslugi.Nazwa)
                    .Select(g => new { Usluga = g.Key, Ilosc = g.Count() })
                    .ToList();

                PodzialUslug = new SeriesCollection();

                var pieSeriesCollection = new SeriesCollection();
                foreach (var item in uslugiWizyty)
                {
                    pieSeriesCollection.Add(new PieSeries
                    {
                        Title = item.Usluga,
                        Values = new LiveCharts.ChartValues<int> { item.Ilosc },
                        DataLabels = true
                    });
                }
                PodzialUslug = pieSeriesCollection;
            }
        }

        private void GenerujPdf()
        {
            if (!WizytyFiltr.Any())
            {
                MessageBox.Show("Brak wizyt do wyeksportowania!",
                    "Błąd",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"RaportWizyt_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
                };

                if (dlg.ShowDialog() == true)
                {
                    using (var fs = new FileStream(dlg.FileName, FileMode.Create))
                    {
                        Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
                        PdfWriter.GetInstance(doc, fs);

                        doc.Open();

                        // Nagłówek
                        Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                        Paragraph title = new Paragraph("RAPORT WIZYT", titleFont)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingAfter = 20
                        };
                        doc.Add(title);

                        // Podsumowanie
                        Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                        doc.Add(new Paragraph($"Zakres dat: {OdData:dd-MM-yyyy} - {DoData:dd-MM-yyyy}", normalFont));
                        doc.Add(new Paragraph($"Lekarz: {(WybranyLekarz != null && WybranyLekarz.IdPracownika != 0 ? WybranyLekarz.Imie + " " + WybranyLekarz.Nazwisko : "Wszyscy")}", normalFont));
                        doc.Add(new Paragraph($"Usługa: {(WybranaUsluga != null && WybranaUsluga.IdUslugi != 0 ? WybranaUsluga.Nazwa : "Wszystkie")}", normalFont));
                        doc.Add(new Paragraph($"Liczba wizyt: {LiczbaZnalezionychWizyt}", normalFont));
                        doc.Add(new Paragraph($"Sumaryczny czas (min): {SumarycznyCzasTrwania}", normalFont));
                        doc.Add(new Chunk("\n"));

                        // Tabela: Lekarz, Usługa, Data, Godzina, Status, Czas
                        PdfPTable table = new PdfPTable(6) { WidthPercentage = 100, SpacingBefore = 10 };
                        table.SetWidths(new float[] { 20f, 20f, 15f, 15f, 15f, 10f });

                        Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                        PdfPCell cell;

                        string[] headers = { "Lekarz", "Usługa", "Data", "Godzina", "Status", "Czas" };
                        foreach (var h in headers)
                        {
                            cell = new PdfPCell(new Phrase(h, headerFont))
                            {
                                BackgroundColor = BaseColor.LIGHT_GRAY
                            };
                            table.AddCell(cell);
                        }

                        Font cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                        foreach (var wizyta in WizytyFiltr)
                        {
                            var lekarz = wizyta.Pracownicy;
                            var usluga = wizyta.Uslugi;

                            table.AddCell(new PdfPCell(new Phrase($"{lekarz.Imie} {lekarz.Nazwisko}", cellFont)));
                            table.AddCell(new PdfPCell(new Phrase(usluga.Nazwa, cellFont)));
                            table.AddCell(new PdfPCell(new Phrase(wizyta.Data.ToString("dd-MM-yyyy"), cellFont)));
                            table.AddCell(new PdfPCell(new Phrase(wizyta.Godzina.ToString(@"hh\:mm"), cellFont)));
                            table.AddCell(new PdfPCell(new Phrase(wizyta.Status, cellFont)));
                            table.AddCell(new PdfPCell(new Phrase(wizyta.CzasTrwaniaWMinutach.ToString(), cellFont)));
                        }

                        doc.Add(table);
                        doc.Close();
                    }

                    MessageBox.Show("PDF został wygenerowany pomyślnie!",
                        "Sukces",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd generowania PDF: {ex.Message}",
                    "Błąd",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
