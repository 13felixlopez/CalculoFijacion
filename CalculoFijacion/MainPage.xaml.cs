using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace CalculoFijacion
{
    public partial class MainPage : ContentPage
    {
        List<string> FTDCList;
        List<string> RendimientoList;
        //Variables asignadas por el usuario
        double NetoPagado = 0, KgNeto = 0;
        //Variables estaticas
        readonly static double IR = 0.985, Bolsagro = 0.9985, Pto = 0.999, Reintegro = 0.985;
        //Variables Calculadas
        double Ftdc;
        //Variables de resultado
        double PrecioB, ImportB, ir, bolsagro, pto, ftdc, reintegro;
        public MainPage()
        {
            InitializeComponent();
            CargarListas();
        }

        private void BtnCalcular_Clicked(object sender, EventArgs e)
        {
            if (Validar())
            {
                calcularFtdc();
                Calculos();
            }
            else
            {
                DisplayAlert("Error, Datos incompletos", "Existen campos Vacíos", "OK");
            }
        }

        private void BtnBorrar_Clicked(object sender, EventArgs e)
        {
            Limpiar();
        }
        private void CargarListas()
        {
            FTDCList = new List<string>
            {
                "1.75",
                "2",
                "2.25",
                "3",
                "4"
            };
            PicFTDC.ItemsSource = FTDCList;

            RendimientoList = new List<string>
            {
                "39",
                "40",
                "41",
                "42",
                "43",
                "44",
                "45",
                "46",
                "47",
                "48",
                "4.1",
                "4.2",
                "4.3",
                "4.4",
                "4.5",
                "4.6",
                "4.7",
                "4.8",
                "4.9",
                "5"
            };
            PicRedimiento.ItemsSource = RendimientoList;
        }
        private void calcularFtdc()
        {
            double ren = 0;  // Valor inicial
            string seleccion = PicRedimiento.SelectedItem.ToString();
            double ValorFtdc = double.Parse(PicFTDC.SelectedItem.ToString(), CultureInfo.InvariantCulture);
            switch (seleccion)
            {
                case "39":
                    ren = 2.56410256;
                    break;
                case "40":
                    ren = 2.5;
                    break;
                case "41":
                    ren = 2.43902439;
                    break;
                case "42":
                    ren = 2.38095238;
                    break;
                case "43":
                    ren = 2.32558139;
                    break;
                case "44":
                    ren = 2.27272727;
                    break;
                case "45":
                    ren = 2.22222222;
                    break;
                case "46":
                    ren = 2.17391304;
                    break;
                case "47":
                    ren = 2.12765957;
                    break;
                case "48":
                    ren = 2.08333333;
                    break;
                case "4.1":
                    ren = 4.1;
                    break;
                case "4.2":
                    ren = 4.2;
                    break;
                case "4.3":
                    ren = 4.3;
                    break;
                case "4.4":
                    ren = 4.4;
                    break;
                case "4.5":
                    ren = 4.5;
                    break;
                case "4.6":
                    ren = 4.6;
                    break;
                case "4.7":
                    ren = 4.7;
                    break;
                case "4.8":
                    ren = 4.8;
                    break;
                case "4.9":
                    ren = 4.9;
                    break;
                case "5":
                    ren = 5;
                    break;
                default:
                    // En caso de que no se seleccione ningún valor válido
                    ren = 0;
                    break;
            }

            //Calcular Ftdc
            Ftdc = (ValorFtdc / ren / 46) * 36.6243;
        }
        private void Calculos()
        {
            NetoPagado = double.Parse(TxtNetoPagado.Text);
            KgNeto = double.Parse(TxtKgNeto.Text);
            //Reintegro
            reintegro = NetoPagado / Reintegro - NetoPagado;
            //FTDC
            ftdc = KgNeto * Ftdc;
            //Pto
            pto = NetoPagado / Pto - NetoPagado;
            //Bolsagro
            bolsagro = NetoPagado / Bolsagro - NetoPagado;
            //IR
            ir = NetoPagado / IR - NetoPagado;
            //Importe Bruto
            ImportB = NetoPagado - reintegro + (ftdc + pto + bolsagro + ir) + 0.35;
            //Precio Bruto
            PrecioB = ImportB / KgNeto;
            //Asignar a los controles
            LblPrecioBruto.Text = PrecioB.ToString("N2");
            LblImporteBruto.Text = ImportB.ToString("N2");
            LblIR.Text = ir.ToString("N2");
            LblBolsagro.Text = bolsagro.ToString("N2");
            LblPto.Text = pto.ToString("N2");
            LblFTDC.Text = ftdc.ToString("N2");
            LblReintegro.Text = reintegro.ToString("N2");
        }
        private bool Validar()
        {
            double entero;
            if (!double.TryParse(TxtKgNeto.Text,out entero)||!double.TryParse(TxtNetoPagado.Text,out entero))
            {
                return false;
            }
            return !(TxtNetoPagado.Text == "" || TxtKgNeto.Text == "" || PicFTDC.SelectedItem == null || PicRedimiento.SelectedItem == null);
        }
        private void Limpiar()
        {
            LblPrecioBruto.Text = "--";
            LblImporteBruto.Text = "--";
            LblIR.Text = "--";
            LblBolsagro.Text = "--";
            LblPto.Text = "--";
            LblFTDC.Text = "--";
            LblReintegro.Text = "--";
            TxtKgNeto.Text = "";
            TxtNetoPagado.Text = "";
            PicFTDC.SelectedItem = null;
            PicRedimiento.SelectedItem = null;
        }
    }
}
