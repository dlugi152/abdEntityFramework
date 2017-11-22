using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using abdModel;
using abd_2.DataModel;

namespace abd_2
{
    /// <inheritdoc />
    /// <summary>
    /// Logika interakcji dla klasy SkazanyOkno.xaml
    /// </summary>
    public partial class SkazanyOkno
    {
        private abdModel.abdModel aaa = new abdModel.abdModel();
        private Random random = new Random();

        public SkazanyOkno() {
            InitializeComponent();
            Przeladuj();
        }

        private void Przeladuj() {
            ListBox.Items.Clear();
            foreach (Skazany zajecia in aaa.Skazanies)
                ListBox.Items.Add(zajecia.IDSkazany);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            try {
                aaa.Skazanies.Add(new Skazany {
                    Wyroks = new List<Wyrok>(),
                    IDSkazany = random.Next(),
                    PESEL = "12345678901",
                    Imie = "Jan",
                    Nazwisko = "Kowalski",
                    DataUrodzenia = DateTime.Today - TimeSpan.FromDays(365 * 20)
                });
                aaa.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            //aaa.Zajecias.Local.Add(zajecia);
            Przeladuj();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            if (ListBox.SelectedItem == null)
                return;
            Skazany item = Enumerable.FirstOrDefault(aaa.Skazanies,
                                                     aaaZajecia => aaaZajecia.IDSkazany == (int)ListBox.SelectedItem);
            if (item == null)
                return;
            ProstyTextBox a = new ProstyTextBox("imie", item.Imie);
            a.ShowDialog();
            if (a.DialogResult == true)
                item.Imie = a.TextDoPrzekazania;
            a = new ProstyTextBox("nazwisko", item.Nazwisko);
            a.ShowDialog();
            if (a.DialogResult == true)
                item.Nazwisko = a.TextDoPrzekazania;
            a = new ProstyTextBox("pesel", item.Nazwisko);
            a.ShowDialog();
            if (a.DialogResult == true)
                item.PESEL = a.TextDoPrzekazania;
            a = new ProstyTextBox("Od dnia", item.DataUrodzenia.ToString(CultureInfo.CurrentCulture));
            a.ShowDialog();
            if (a.DialogResult == true) {
                if (DateTime.TryParse(a.TextDoPrzekazania, out DateTime l2))
                    item.DataUrodzenia = l2;
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
            Skazany item = Enumerable.FirstOrDefault(aaa.Skazanies,
                                                     aaaZajecia => aaaZajecia.IDSkazany == (int)ListBox.SelectedItem);
            if (item == null)
                return;
            aaa.Skazanies.Remove(item);
            try {
                aaa.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            Przeladuj();
        }
    }
}