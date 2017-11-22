using System;
using abdModel;

namespace abd_2
{
    /// <inheritdoc />
    /// <summary>
    /// Logika interakcji dla klasy RodzajOkno.xaml
    /// </summary>
    public partial class RodzajOkno
    {
        public RodzajOkno() {
            InitializeComponent();
            foreach (Rodzaj value in Enum.GetValues(typeof(Rodzaj)))
                ListBox.Items.Add(value);
        }
    }
}