using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using GalaSoft.MvvmLight.Messaging;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class WystawRecepteViewModel : JedenViewModel<Recepty>
    {
        public WystawRecepteViewModel()
            : base("Wystaw Receptę")
        {
            item = new Recepty { DataWystawienia = DateTime.Now };
            ListaLekow = new ObservableCollection<ReceptyLeki>();

            // Załaduj listę lekarzy
            Lekarze = new ObservableCollection<Pracownicy>(
                dentCareEntities.Pracownicy.Where(p => p.Status == "Aktywny").ToList()
            );

            // Rejestracja Messengerów
            Messenger.Default.Register<Pacjenci>(this, getWybranyPacjent);
            Messenger.Default.Register<Leki>(this, getWybranyLek);
        }

        #region Properties

        public string WybranyPacjent => item.Pacjenci != null ? $"{item.Pacjenci.Imie} {item.Pacjenci.Nazwisko}" : string.Empty;

        public ObservableCollection<Pracownicy> Lekarze { get; set; }

        private Pracownicy _wybranyLekarz;
        public Pracownicy WybranyLekarz
        {
            get => _wybranyLekarz;
            set
            {
                _wybranyLekarz = value;
                if (value != null)
                {
                    item.IdPracownika = value.IdPracownika;
                }
                OnPropertyChanged(() => WybranyLekarz);
            }
        }

        public ObservableCollection<ReceptyLeki> ListaLekow { get; set; }

        private string _uwagi;
        public string Uwagi
        {
            get => _uwagi;
            set
            {
                if (_uwagi != value)
                {
                    _uwagi = value;
                    item.Uwagi = value;
                    OnPropertyChanged(() => Uwagi);
                }
            }
        }

        #endregion

        #region Commands

        public ICommand ShowPacjenciWindowCommand => new BaseCommand(() =>
        {
            Messenger.Default.Send("PacjenciWindowAll");
        });

        public ICommand ShowLekiWindowCommand => new BaseCommand(() =>
        {
            Messenger.Default.Send("LekiWindowAll");
        });

        public ICommand ZapiszReceptęCommand => new BaseCommand(Save);

        public ICommand GenerujPdfCommand => new BaseCommand(() =>
        {
            Save();
            GenerujPdf();
        });

        #endregion

        #region Helpers

        private void getWybranyPacjent(Pacjenci pacjent)
        {
            if (pacjent != null)
            {
                item.Pacjenci = pacjent;
                item.IdPacjenta = pacjent.IdPacjenta;
                OnPropertyChanged(() => WybranyPacjent);
            }
        }

        private void getWybranyLek(Leki lek)
        {
            if (lek != null)
            {
                var nowyLek = new ReceptyLeki
                {
                    IdLeku = lek.IdLeku,
                    Ilosc = 1,
                    Leki = lek
                };

                ListaLekow.Add(nowyLek);
                OnPropertyChanged(() => ListaLekow);
            }
        }

        public override void Save()
        {
            if (item.Pacjenci == null || item.IdPacjenta == 0)
                throw new InvalidOperationException("Nie wybrano pacjenta.");
            if (WybranyLekarz == null)
                throw new InvalidOperationException("Nie wybrano lekarza.");
            if (!ListaLekow.Any())
                throw new InvalidOperationException("Recepta musi zawierać co najmniej jeden lek.");
            if (string.IsNullOrWhiteSpace(Uwagi))
                throw new InvalidOperationException("Pole Uwagi jest wymagane.");

            using (var db = new DentCareEntities()) // Tworzymy nowy kontekst bazy danych
            {
                // Pobieramy pacjenta i lekarza z bazy, aby uniknąć konfliktów kontekstu
                var pacjent = db.Pacjenci.Find(item.IdPacjenta);
                var lekarz = db.Pracownicy.Find(item.IdPracownika);

                if (pacjent == null || lekarz == null)
                    throw new InvalidOperationException("Nie znaleziono pacjenta lub lekarza w bazie.");

                var nowaRecepta = new Recepty
                {
                    IdPacjenta = pacjent.IdPacjenta,
                    Pacjenci = pacjent,
                    IdPracownika = lekarz.IdPracownika,
                    Pracownicy = lekarz,
                    DataWystawienia = item.DataWystawienia,
                    Uwagi = Uwagi,
                    ReceptyLeki = new List<ReceptyLeki>()
                };

                foreach (var lek in ListaLekow)
                {
                    var lekZBazy = db.Leki.Find(lek.IdLeku); // Pobieramy lek z nowego kontekstu

                    if (lekZBazy == null)
                        throw new InvalidOperationException("Nie znaleziono leku w bazie.");

                    nowaRecepta.ReceptyLeki.Add(new ReceptyLeki
                    {
                        IdLeku = lekZBazy.IdLeku,
                        Ilosc = lek.Ilosc,
                        Leki = lekZBazy,
                        DataDodania = DateTime.Now
                    });
                }

                db.Recepty.Add(nowaRecepta);
                db.SaveChanges();
            }


        }


        private void GenerujPdf()
        {
            if (item == null || item.Pacjenci == null || WybranyLekarz == null || !ListaLekow.Any())
            {
                MessageBox.Show("Nie można wygenerować PDF – brak wymaganych danych!",
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                //string kodRecepty = $"{item.IdRecepty}-{DateTime.Now:yyyyMMdd}";
                string kodRecepty = $"{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"Recepta_{item.Pacjenci.Imie}_{item.Pacjenci.Nazwisko}.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        Document document = new Document(PageSize.A4, 50, 50, 50, 50);
                        PdfWriter.GetInstance(document, fs);
                        document.Open();

                        // Ustawienia czcionek
                        Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
                        Font subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                        Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                        Font smallHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9); // Zmniejszona czcionka nagłówków

                        // Tytuł dokumentu
                        Paragraph title = new Paragraph("RECEPTA", titleFont)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingAfter = 20
                        };
                        document.Add(title);

                        // Kod recepty
                        Paragraph kod = new Paragraph($"Kod recepty: {kodRecepty}", subtitleFont)
                        {
                            Alignment = Element.ALIGN_RIGHT,
                            SpacingAfter = 15
                        };
                        document.Add(kod);

                        // Informacje o pacjencie
                        document.Add(new Paragraph($"Pacjent: {item.Pacjenci.Imie} {item.Pacjenci.Nazwisko}", normalFont));
                        document.Add(new Paragraph($"Data urodzenia: {item.Pacjenci.DataUrodzenia?.ToShortDateString()}", normalFont));
                        document.Add(new Paragraph($"PESEL: {item.Pacjenci.PESEL}", normalFont));
                        document.Add(new Chunk("\n"));

                        // Informacje o lekarzu
                        document.Add(new Paragraph($"Lekarz: {WybranyLekarz.Imie} {WybranyLekarz.Nazwisko}", normalFont));
                        document.Add(new Paragraph($"Data wystawienia: {item.DataWystawienia:dd-MM-yyyy}", normalFont));
                        document.Add(new Chunk("\n"));

                        // Uwagi
                        document.Add(new Paragraph("Uwagi:", subtitleFont));
                        document.Add(new Paragraph(item.Uwagi, normalFont));
                        document.Add(new Chunk("\n"));

                        // Tworzenie tabeli leków
                        PdfPTable table = new PdfPTable(5)
                        {
                            WidthPercentage = 90,  // Mniejsza szerokość tabeli
                            SpacingBefore = 20
                        };
                        table.SetWidths(new float[] { 35f, 35f, 25f, 20f, 15f }); // Poprawione szerokości kolumn

                        // Nagłówki tabeli
                        BaseColor headerColor = new BaseColor(200, 200, 200);
                        PdfPCell cell;

                        string[] headers = { "Nazwa leku", "Substancja", "Postać", "Dawka", "Ilość" };
                        foreach (var header in headers)
                        {
                            cell = new PdfPCell(new Phrase(header, smallHeaderFont)) // Użycie mniejszej czcionki
                            {
                                BackgroundColor = headerColor,
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                Padding = 4
                            };
                            table.AddCell(cell);
                        }

                        // Dodanie leków do tabeli
                        foreach (var lek in ListaLekow)
                        {
                            table.AddCell(new PdfPCell(new Phrase(lek.Leki.Nazwa, normalFont)));
                            table.AddCell(new PdfPCell(new Phrase(lek.Leki.SubstancjaCzynna, normalFont)));
                            table.AddCell(new PdfPCell(new Phrase(lek.Leki.Postac, normalFont)));
                            table.AddCell(new PdfPCell(new Phrase(lek.Leki.Dawka, normalFont)));
                            table.AddCell(new PdfPCell(new Phrase(lek.Ilosc.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        }

                        document.Add(table);
                        document.Close();
                    }

                    MessageBox.Show("PDF został wygenerowany!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd generowania PDF: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ResetFormularz();
            MessageBox.Show("Recepta została poprawnie zapisana!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
        }



        private void ResetFormularz()
        {
            item = new Recepty { DataWystawienia = DateTime.Now };
            ListaLekow.Clear();
            WybranyLekarz = null;
            Uwagi = string.Empty;
            OnPropertyChanged(() => WybranyPacjent);
            OnPropertyChanged(() => WybranyLekarz);
            OnPropertyChanged(() => ListaLekow);
            OnPropertyChanged(() => Uwagi);
        }

        #endregion
    }
}