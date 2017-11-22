using abd_2.DataModel;
using abdModel;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace abd_2
{
    /// <inheritdoc />
    /// <summary>
    /// Logika interakcji dla klasy PrzepustkiOkno.xaml
    /// </summary>
    public partial class PrzepustkiOkno
    {
        private abdModel.abdModel aaa = new abdModel.abdModel();
        private Random random = new Random();

        public PrzepustkiOkno() {
            InitializeComponent();
            Przeladuj();
        }

        private void Przeladuj() {
            ListBox.Items.Clear();
            foreach (Przepustki przepustka in aaa.Przepustkis)
                ListBox.Items.Add(przepustka.IDPrzepustki);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            int next = random.Next();
            try {
                Wyrok wyr = Enumerable.FirstOrDefault(aaa.Wyroks);
                if (wyr == null)
                    return;
                aaa.Przepustkis.Add(new Przepustki {
                    NrSprawy = wyr.NrSprawy,
                    WykorzystanePunkty = 5,
                    Wyrok = wyr,
                    DataRozpoczecia = DateTime.Today - new TimeSpan(random.Next() % 10 + 10, 0, 0, 0),
                    DataZakonczenia = DateTime.Today - new TimeSpan(random.Next() % 10, 0, 0, 0),
                    IDPrzepustki = next
                });
                aaa.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            Przeladuj();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            if (ListBox.SelectedItem == null)
                return;
            Przepustki item = Enumerable.FirstOrDefault(aaa.Przepustkis,
                                                        aaaPrzepustki =>
                                                            aaaPrzepustki.IDPrzepustki == (int)ListBox.SelectedItem);
            if (item == null)
                return;
            ProstyTextBox a = new ProstyTextBox("Punkty", item.WykorzystanePunkty.ToString());
            a.ShowDialog();
            if (a.DialogResult == true) {
                if (int.TryParse(a.TextDoPrzekazania, out int l2))
                    item.WykorzystanePunkty = l2;
            }
            a = new ProstyTextBox("Data Rozpoczecia", item.DataRozpoczecia.ToString(CultureInfo.CurrentCulture));
            a.ShowDialog();
            if (a.DialogResult == true) {
                if (DateTime.TryParse(a.TextDoPrzekazania, out DateTime l2))
                    item.DataRozpoczecia = l2;
            }
            a = new ProstyTextBox("Data zakonczenia", item.DataZakonczenia.ToString(CultureInfo.CurrentCulture));
            a.ShowDialog();
            if (a.DialogResult == true) {
                if (DateTime.TryParse(a.TextDoPrzekazania, out DateTime l2))
                    item.DataZakonczenia = l2;
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
            Przepustki item = Enumerable.FirstOrDefault(aaa.Przepustkis,
                                                        aaaPrzepustki =>
                                                            aaaPrzepustki.IDPrzepustki == (int)ListBox.SelectedItem);
            if (item == null)
                return;
            aaa.Przepustkis.Remove(item);
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