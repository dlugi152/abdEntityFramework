using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using abdModel;
using abd_2.DataModel;

namespace abd_2
{
    /// <summary>
    /// Logika interakcji dla klasy WyrokiOkno.xaml
    /// </summary>
    public partial class WyrokiOkno : Window
    {
        private abdModel.abdModel aaa = new abdModel.abdModel();
        private Random random = new Random();

        public WyrokiOkno() {
            InitializeComponent();
            Przeladuj();
        }

        private void Przeladuj() {
            ListBox.Items.Clear();
            foreach (Wyrok zajecia in aaa.Wyroks)
                ListBox.Items.Add(zajecia.NrSprawy);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            int next = random.Next();
            try {
                Skazany skz = Enumerable.FirstOrDefault(aaa.Skazanies);
                if (skz == null)
                    return;
                aaa.Wyroks.Add(new Wyrok {
                    Harmonograms = new List<Harmonogram>(),
                    NrSprawy = next,
                    RodzajPrzestepstwa = "Kradzież",
                    Skazany = skz,
                    DoDnia = DateTime.Today - new TimeSpan(random.Next() % 100, 0, 0, 0),
                    OdDnia = DateTime.Today - new TimeSpan(random.Next() % 100 + 100, 0, 0, 0),
                    IDSkazany = skz.IDSkazany,
                    Przepustkis = new List<Przepustki>()
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
            Wyrok item = Enumerable.FirstOrDefault(aaa.Wyroks,
                                                   aaaZajecia => aaaZajecia.NrSprawy == (int)ListBox.SelectedItem);
            if (item == null)
                return;
            ProstyTextBox a = new ProstyTextBox("rodzaj", item.RodzajPrzestepstwa);
            a.ShowDialog();
            if (a.DialogResult == true)
                item.RodzajPrzestepstwa = a.TextDoPrzekazania;
            a = new ProstyTextBox("Do dnia", item.DoDnia.ToString(CultureInfo.CurrentCulture));
            a.ShowDialog();
            if (a.DialogResult == true) {
                if (DateTime.TryParse(a.TextDoPrzekazania, out DateTime l2))
                    item.DoDnia = l2;
            }
            a = new ProstyTextBox("Od dnia", item.OdDnia.ToString(CultureInfo.CurrentCulture));
            a.ShowDialog();
            if (a.DialogResult == true) {
                if (DateTime.TryParse(a.TextDoPrzekazania, out DateTime l2))
                    item.OdDnia = l2;
            }
            try {
                aaa.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            Wyrok item = Enumerable.FirstOrDefault(aaa.Wyroks,
                                                   aaaZajecia => aaaZajecia.NrSprawy == (int)ListBox.SelectedItem);
            if (item == null)
                return;
            aaa.Wyroks.Remove(item);
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