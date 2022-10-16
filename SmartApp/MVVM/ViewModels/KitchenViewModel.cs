using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using SmartApp.Helpers;
using SmartApp.MVVM.Models;
using SmartApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace SmartApp.MVVM.ViewModels
{
    internal class KitchenViewModel : ObservableObject
    {
        private DispatcherTimer timer;

        //private readonly RegistryManager registryManager = RegistryManager.CreateFromConnectionString("HostName=systemutveckling.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=hT4k+bf1rCd8+cKT3vA8qjpe8wqlidzikjU7C3TK6y4=");
        private readonly NavigationStore _navigationStore;
        private readonly IDeviceService _deviceService;
        private readonly IWeatherService _weatherService;

        public ICommand NavigateToSettings { get; }

        public KitchenViewModel(NavigationStore navigationStore, IDeviceService deviceService, IWeatherService weatherService)
        {
            _navigationStore = navigationStore;
            _deviceService = deviceService;
            _weatherService = weatherService;
            DeviceItems = new ObservableCollection<DeviceItem>();

            NavigateToSettings = new NavigateCommand<KitchenViewModel>(navigationStore, () => new KitchenViewModel(_navigationStore, _deviceService, _weatherService));

            SetClock();
            SetWeatherAsync().ConfigureAwait(false);

            SetupPopulateDeviceItems().ConfigureAwait(false);


        }

        private async Task SetupPopulateDeviceItems()
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

            await PopulateDeviceItemsAsync();

            while (await timer.WaitForNextTickAsync())
            {
                await PopulateDeviceItemsAsync();
            }
        }

        private ObservableCollection<DeviceItem>? _deviceItems;
        public ObservableCollection<DeviceItem>? DeviceItems
        {
            get => _deviceItems;
            set
            {
                _deviceItems = value;
                OnPropertyChanged();
            }
        }

        private string? _currentWeatherCondition;
        public string CurrentWeatherCondition
        {
            get => _currentWeatherCondition!;
            set
            {
                _currentWeatherCondition = value;
                OnPropertyChanged();
            }
        }

        private string? _currentTemperature;
        public string CurrentTemperature
        {
            get => _currentTemperature!;
            set
            {
                _currentTemperature = value;
                OnPropertyChanged();
            }
        }

        private string? _currentTime;
        public string CurrentTime
        {
            get => _currentTime!;
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }

        private string? _currentDate;
        public string CurrentDate
        {
            get => _currentDate!;
            set
            {
                _currentDate = value;
                OnPropertyChanged();
            }
        }

        protected override async void second_timer_tick(object? sender, EventArgs e)
        {
            SetClock();
            base.second_timer_tick(sender, e);
        }

        protected override async void hour_timer_tick(object? sender, EventArgs e)
        {
            await SetWeatherAsync();
            base.hour_timer_tick(sender, e);
        }


        private void SetClock()
        {
            CurrentTime = DateTime.Now.ToString("HH:mm");
            CurrentDate = DateTime.Now.ToString("dd MMMM yyyy");
        }

        private async Task SetWeatherAsync()
        {
            var weather = await _weatherService.GetWeatherDataAsync();
            CurrentTemperature = weather.Temperature.ToString();
            CurrentWeatherCondition = weather.WeatherCondition ?? "";
        }

        private async Task PopulateDeviceItemsAsync()
        {
            var result = await _deviceService.GetDevicesAsync("SELECT * from devices");

            result.ForEach(device =>
            {
                var item = DeviceItems?.FirstOrDefault(x => x.DeviceId == device.DeviceId);
                if (item == null)
                    DeviceItems?.Add(device);
                else
                {
                    var index = _deviceItems!.IndexOf(item);
                    _deviceItems[index] = device;
                }
            });
        }
    }
}
