using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using abdModel;
using abd_2.DataModel;

namespace abd_2
{
    /// <inheritdoc />
    /// <summary>
    /// Logika interakcji dla klasy ZajeciaOkno.xaml
    /// </summary>
    public partial class ZajeciaOkno
    {
        private abdModel.abdModel aaa = new abdModel.abdModel();
        private Random random = new Random();

        public ZajeciaOkno() {
            InitializeComponent();
            Przeladuj();
        }

        private void Przeladuj() {
            ListBox.Items.Clear();
            foreach (Zajecia zajecia in aaa.Zajecias)
                ListBox.Items.Add(zajecia.IDZajecia);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            int next = random.Next();
            try {
                aaa.Zajecias.Add(new Zajecia {
                    Harmonograms = new List<Harmonogram>(),
                    Opis = "Gotowanie",
                    Punkty = next,
                    IDZajecia = next
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
            Zajecia item = Enumerable.FirstOrDefault(aaa.Zajecias,
                                                     aaaZajecia => aaaZajecia.IDZajecia == (int)ListBox.SelectedItem);
            if (item == null)
                return;
            ProstyTextBox a = new ProstyTextBox("opis", item.Opis);
            a.ShowDialog();
            if (a.DialogResult == true)
                item.Opis = a.TextDoPrzekazania;
            a = new ProstyTextBox("punkty", item.Punkty.ToString());
            a.ShowDialog();
            if (a.DialogResult == true) {
                if (int.TryParse(a.TextDoPrzekazania, out int l2))
                    item.Punkty = l2;
            }
            try {
                aaa.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            Zajecia item = Enumerable.FirstOrDefault(aaa.Zajecias,
                                                     aaaZajecia => aaaZajecia.IDZajecia == (int)ListBox.SelectedItem);
            if (item == null)
                return;
            aaa.Zajecias.Remove(item);
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