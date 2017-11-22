using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using abdModel;
using abd_2.DataModel;
using Microsoft.EntityFrameworkCore.Storage;

namespace abd_2
{
    /// <inheritdoc />
    /// <summary>
    /// Logika interakcji dla klasy HarmonogramOkno.xaml
    /// </summary>
    public partial class HarmonogramOkno
    {
        private abdModel.abdModel aaa = new abdModel.abdModel();
        private Random random = new Random();

        public HarmonogramOkno() {
            InitializeComponent();
            Przeladuj();
        }

        private void Przeladuj() {
            ListBox.Items.Clear();
            foreach (Harmonogram zajecia in aaa.Harmonograms)
                ListBox.Items.Add(zajecia.IDPracowania);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e) {
            int next = random.Next();
            try {
                Wyrok wyr = Enumerable.FirstOrDefault(aaa.Wyroks);
                if (wyr == null)
                    return;
                Zajecia zaj = Enumerable.FirstOrDefault(aaa.Zajecias);
                if (zaj == null)
                    return;
                aaa.Harmonograms.Add(new Harmonogram {
                    IDZajecia = zaj.IDZajecia,
                    Wyrok = wyr,
                    NrSprawy = wyr.NrSprawy,
                    Zajecia = zaj,
                    IDPracowania = random.Next(),
                    DataZakonczenia = DateTime.Today - new TimeSpan(random.Next() % 100, 0, 0, 0),
                    DataRozpoczecia = DateTime.Today - new TimeSpan(random.Next() % 100 + 100, 0, 0, 0),
                    DodatkowePunkty = 0,
                    GodzinaRozpoczecia = TimeSpan.FromHours(1),
                    GodzinaZakonczenia = TimeSpan.FromHours(1),
                    Uwagi = ""
                });
                aaa.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            //aaa.Zajecias.Local.Add(zajecia);
            Przeladuj();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedItem == null)
                return;
            Harmonogram item = Enumerable.FirstOrDefault(aaa.Harmonograms,
                                                         aaaZajecia =>
                                                             aaaZajecia.IDPracowania == (int)ListBox.SelectedItem);
            if (item == null)
                return;
            ProstyTextBox a = new ProstyTextBox("uwagi", item.Uwagi);
            a.ShowDialog();
            a = new ProstyTextBox("dodatkowe punkty", item.DodatkowePunkty.ToString());
            a.ShowDialog();
            a = new ProstyTextBox("data rozpoczecia", item.DataRozpoczecia.ToString(CultureInfo.CurrentCulture));
            a.ShowDialog();
            if (a.DialogResult == true) {
                if (DateTime.TryParse(a.TextDoPrzekazania, out DateTime l2))
                    item.DataRozpoczecia = l2;
            }
            a = new ProstyTextBox("data zakonczenia", item.DataZakonczenia.ToString(CultureInfo.CurrentCulture));
            a.ShowDialog();
            if (a.DialogResult == true) {
                if (DateTime.TryParse(a.TextDoPrzekazania, out DateTime l2))
                    item.DataZakonczenia = l2;
            }
            a = new ProstyTextBox("godzina rozpoczecia", item.GodzinaRozpoczecia.ToString());
            a.ShowDialog();
            if (a.DialogResult == true) {
                if (TimeSpan.TryParse(a.TextDoPrzekazania, out TimeSpan l2))
                    item.GodzinaRozpoczecia = l2;
            }
            a = new ProstyTextBox("godzina zakonczenia", item.GodzinaZakonczenia.ToString());
            a.ShowDialog();
            if (a.DialogResult == true) {
                if (TimeSpan.TryParse(a.TextDoPrzekazania, out TimeSpan l2))
                    item.GodzinaZakonczenia = l2;
            }
            try {
                aaa.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            if (ListBox.SelectedItem == null)
                return;
            Harmonogram item = Enumerable.FirstOrDefault(aaa.Harmonograms,
                                                         aaaZajecia =>
                                                             aaaZajecia.IDPracowania == (int)ListBox.SelectedItem);
            if (item == null)
                return;
            aaa.Harmonograms.Remove(item);
            try {
                aaa.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            Przeladuj();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) {
            if (ListBox.SelectedItem==null)
                return;
            using (var transaction = aaa.Database.BeginTransaction()) {
                Harmonogram item = Enumerable.FirstOrDefault(aaa.Harmonograms,
                                                             aaaZajecia =>
                                                                 aaaZajecia.IDPracowania == (int)ListBox.SelectedItem);
                if (item == null)
                    return;
                try {
                    ProstyTextBox a = new ProstyTextBox("uwagi", item.Uwagi);
                    a.ShowDialog();
                    if (a.DialogResult == true)
                        item.Uwagi = a.TextDoPrzekazania;
                    aaa.SaveChanges();
                    a = new ProstyTextBox("dodatkowe punkty", item.DodatkowePunkty.ToString());
                    a.ShowDialog();
                    if (a.DialogResult == true) {
                        if (int.TryParse(a.TextDoPrzekazania, out int l2))
                            item.DodatkowePunkty = l2;
                        else
                            throw new Exception("nieprawidłowa liczba");
                    }
                    aaa.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }
            }
            Przeladuj();
        }
    }
}