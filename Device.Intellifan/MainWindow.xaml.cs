﻿using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Device.Intellifan.Models;


namespace Device.Intellifan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _deviceId = "intellifan";
        private string _deviceType = "IntelliFAN";
        private string _location = "Living Room";
        private string _owner = "Zacharias Lönnqvist";
        private bool _isRunning = false;
        private bool _isConnected = false;
        private DeviceClient deviceClient;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (_isConnected)
                tblockConnectionState.Text = "Connected";
            else
                tblockConnectionState.Text = "Not Connected";
        }

        private void Initialize()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            dispatcherTimer.Start();

            _isConnected = Task.Run(async () =>
            {
                while (!_isConnected)
                {
                    await Task.Delay(1000);
                    try
                    {
                        using var http = new HttpClient();
                        var result = await http.PostAsJsonAsync("https://systemutveckling-functionapp.azurewebsites.net/api/devices/connect", new
                        {
                            deviceId = _deviceId,
                            deviceType = _deviceType,
                            location = _location,
                            owner = _owner
                        });

                        if (result.IsSuccessStatusCode || result.StatusCode.ToString() == "Conflict")
                        {
                            var data = JsonConvert.DeserializeObject<AddDeviceResponse>(await result.Content.ReadAsStringAsync());
                            deviceClient = DeviceClient.CreateFromConnectionString(data.DeviceConnectionString);

                            var twin = await deviceClient.GetTwinAsync();
                            if (twin != null)
                            {
                                TwinCollection reported = new TwinCollection();
                                reported["owner"] = _owner;
                                reported["deviceType"] = _deviceType;
                                reported["location"] = _location;

                                await deviceClient.UpdateReportedPropertiesAsync(reported);
                                return true;
                            }
                        }
                    }
                    catch { }
                }
                return false;
            }).Result;
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            var iconRotateBladeStoryBoard = (BeginStoryboard)TryFindResource("iconRotateBladeStoryBoard");

            if(!_isRunning)
            {
                iconRotateBladeStoryBoard.Storyboard.Begin();
                _isRunning = true;
                btnAction.Content = "Stop";
            }
            else
            {
                iconRotateBladeStoryBoard.Storyboard.Stop();
                _isRunning = false;
                btnAction.Content = "Start";
            }
        }
    }
}
