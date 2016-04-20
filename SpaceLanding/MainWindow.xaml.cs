using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpaceLanding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<SpaceLand> SpaceLands = new List<SpaceLand>();
        List<SpaceCraft> SpaceCrafts = new List<SpaceCraft>();

        Landing Landing = new Landing();

        //static double currentTime = 0;
        static double dt = 0.05;
        static double secondsPerBurning = 10;

        public MainWindow()
        {
            InitializeComponent();

            ClearAndAddData();

            SpaceLands_comboBox.ItemsSource = SpaceLands;
            SpaceLands_comboBox.SelectedIndex = 0;

            SpaceCrafts_comboBox.ItemsSource = SpaceCrafts;
            SpaceCrafts_comboBox.SelectedIndex = 0;
        }

        void ClearAndAddData()
        {
            SpaceLands.Clear();
            SpaceCrafts.Clear();

            SpaceLands.Add(new SpaceLand { Name = "Луна", G0 = 1.622, Radius = 1737.1 * 1000 });
            SpaceLands.Add(new SpaceLand { Name = "Марс", G0 = 3.711, Radius = 3389.5 * 1000 });

            SpaceCrafts.Add(new SpaceCraft
            {
                Name = "Lander-1",
                Mass = 7500,
                FuelMass = 7500,
                High = 200000,
                Velocity = 1600,

                ExhaustSpeed = 2800
            });


            SpaceCrafts.Add(new SpaceCraft
            {
                Name = "Lander-2",
                Mass = 5000,
                FuelMass = 4000,
                High = 150000,
                Velocity = 1300,

                ExhaustSpeed = 3500
            });
        }

        private void SpaceLands_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpaceLands_comboBox.SelectedIndex == -1) SpaceLands_comboBox.SelectedIndex = 0;

            var land = SpaceLands[SpaceLands_comboBox.SelectedIndex];
            SpaceLand_Description_Label.Content = string.Format("Ускорение у поверхности ~ {0} м/c^2; Радиус ~ {1} км.", land.G0, (int)(land.Radius / 1000));
        }

        private void SpaceCrafts_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpaceCrafts_comboBox.SelectedIndex == -1) SpaceCrafts_comboBox.SelectedIndex = 0;

            var craft = SpaceCrafts[SpaceCrafts_comboBox.SelectedIndex];

            var desc = "";

            desc += string.Format("Масса: {0} кг", craft.Mass) + Environment.NewLine;
            desc += string.Format("Топливо: {0} кг", craft.Mass) + Environment.NewLine;
            desc += string.Format("Скорость истечения газов: {0} м/с", craft.ExhaustSpeed) + Environment.NewLine;
            desc += string.Format("Начальная высота: {0} м", craft.High) + Environment.NewLine;
            desc += string.Format("Начальная скорость: {0} м/с", craft.Velocity);

            SpaceCraft_Description_Label.Content = desc;
        }

        static string intro;
        private void Go_button_Click(object sender, RoutedEventArgs e)
        {
            intro = string.Format("Вы в космическом аппарате \"{0}\" приближаетесь к космическому телу \"{1}\". ", SpaceCrafts_comboBox.Text, SpaceLands_comboBox.Text);
            intro += "Ваш главный компьютер отказал. Каждые 10 секунд полёта Вам необходимо вручную вводить то, сколько топлива в секунду будет сжигаться последующие 10 секунд. ";
            intro += "Приземлиться необходимо со скоростью не более 10 м/с. Удачи!!! Она Вам понадобится.";

            var landIndex = SpaceLands_comboBox.SelectedIndex;
            var shipIndex = SpaceCrafts_comboBox.SelectedIndex;

            ClearAndAddData();

            SpaceLands_comboBox.SelectedIndex = landIndex;
            SpaceCrafts_comboBox.SelectedIndex = shipIndex;

            Landing = new Landing();
            Landing.SpaceCraft = SpaceCrafts[shipIndex];
            Landing.SpaceLand = SpaceLands[landIndex];

            KgToExhaust_textBox.IsEnabled = true;

            PrintCurrentStatus();
        }

        void PrintCurrentStatus()
        {
            Status_textBlock.Text = intro + Environment.NewLine + Environment.NewLine;

            Status_textBlock.Text += "Текущий статус:" + Environment.NewLine;
            Status_textBlock.Text += GetStatusData();
        }


        string GetStatusData()
        {
            var statusData = "";

            statusData += string.Format("Время: {0} с", (int)Landing.TimePassed) + Environment.NewLine;
            statusData += string.Format("Высота: {0} м", (int)Landing.SpaceCraft.High) + Environment.NewLine;
            statusData += string.Format("Скорость: {0} м/с", (int)Landing.SpaceCraft.Velocity) + Environment.NewLine;
            statusData += string.Format("Топливо: {0} кг", (int)Landing.SpaceCraft.FuelMass) + Environment.NewLine;
            statusData += string.Format("Гравитация: {0:F2} м/с^2", Landing.GetGravityChange());

            return statusData;
        }


        private void KgToExhaust_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;


            int kg;

            if (!int.TryParse(KgToExhaust_textBox.Text, out kg))
                return;

            if (kg < 0 || kg > 100)
                return;


            if(kg * secondsPerBurning > Landing.SpaceCraft.FuelMass)
            {
                MessageBox.Show(string.Format("У аппарата осталось {0:F1} кг топлива. В следующие 10 секунд невозможно сжечь {1} кг. Введите другое значение.",
                    Landing.SpaceCraft.FuelMass, kg * secondsPerBurning));

                return;
            }


            var t = 0.0;

            Landing.SpaceCraft.FuelBurningRate = kg;

            while (t < secondsPerBurning)
            {
                if (!Landing.Simulate(dt))
                {
                    ImpactHappened();

                    KgToExhaust_textBox.Text = "";

                    return;
                }

                t += dt;
            }


            if (Landing.SpaceCraft.FuelMass == 0)
            {
                FuelDepleeted();

                KgToExhaust_textBox.Text = "";

                return;
            }

            PrintCurrentStatus();

            KgToExhaust_textBox.Text = "";
        }

        void FuelDepleeted()
        {
            Landing.SpaceCraft.FuelBurningRate = 0;

            while (Landing.Simulate(dt)) ;


            var log = "У Вас закончилось топиво. После свободного падения на поверхность Ваш статус:" + Environment.NewLine;

            Landing.SpaceCraft.High = 0;

            log += GetStatusData() + Environment.NewLine + Environment.NewLine;

            Status_textBlock.Text = log;

            if (Landing.SpaceCraft.Velocity > 10)
                Status_textBlock.Text += "К сожалению, Вы разбились.";
            else
                Status_textBlock.Text += "Поздравляю с удачным падением!!!";

            EndGame();
        }

        void ImpactHappened()
        {
            Status_textBlock.Text = "Вы достигли поверхности. Статус в момент касания поверхности:" + Environment.NewLine;

            Landing.SpaceCraft.High = 0;

            Status_textBlock.Text += GetStatusData() + Environment.NewLine + Environment.NewLine;

            if (Landing.SpaceCraft.Velocity > 10)
                Status_textBlock.Text += "К сожалению, Вы разбились.";
            else
                Status_textBlock.Text += "Поздравляю с удачной посадкой!!!";

            EndGame();
        }

        void EndGame()
        {
            ClearAndAddData();

            KgToExhaust_textBox.IsEnabled = false;
        }
    }
}
