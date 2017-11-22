using System;
using abdModel;

namespace abd_2
{
    /// <inheritdoc />
    /// <summary>
    /// Logika interakcji dla klasy PrzestępstwaOkno.xaml
    /// </summary>
    public partial class PrzestępstwaOkno
    {
        public PrzestępstwaOkno() {
            InitializeComponent();
            foreach (Przestępstwo value in Enum.GetValues(typeof(Przestępstwo)))
                ListBox.Items.Add(value);
        }
    }
}