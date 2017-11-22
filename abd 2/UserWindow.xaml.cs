using System.Windows;

namespace abd_2
{
    /// <inheritdoc />
    /// <summary>
    /// Logika interakcji dla klasy UserWindow.xaml
    /// </summary>
    public partial class UserWindow
    {
        public UserWindow() {
            InitializeComponent();
        }

        private void harmonogrambutton_Click(object sender, RoutedEventArgs e) {
            HarmonogramOkno harmonogramOkno = new HarmonogramOkno();
            harmonogramOkno.Show();
        }

        private void przepustkibutton_Click(object sender, RoutedEventArgs e) {
            PrzepustkiOkno przepustkiOkno = new PrzepustkiOkno();
            przepustkiOkno.Show();
        }

        private void przestepstwabutton_Click(object sender, RoutedEventArgs e) {
            PrzestępstwaOkno przestępstwaOkno = new PrzestępstwaOkno();
            przestępstwaOkno.Show();
        }

        private void rodzajbutton_Click(object sender, RoutedEventArgs e) {
            RodzajOkno rodzajOkno = new RodzajOkno();
            rodzajOkno.Show();
        }

        private void skazanibutton_Click(object sender, RoutedEventArgs e) {
            SkazanyOkno skazanyOkno = new SkazanyOkno();
            skazanyOkno.Show();
        }

        private void wyrokibutton_Click(object sender, RoutedEventArgs e) {
            WyrokiOkno wyrokiOkno = new WyrokiOkno();
            wyrokiOkno.Show();
        }

        private void zajeciabutton_Click(object sender, RoutedEventArgs e) {
            ZajeciaOkno zajeciaOkno = new ZajeciaOkno();
            zajeciaOkno.Show();
        }
    }
}