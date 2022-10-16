using Microsoft.Azure.Devices.Client;

namespace Device.Temperature.ConsoleApp;

class Program
{
    private static readonly DeviceClient _deviceClient = DeviceClient.CreateFromConnectionString("HostName=systemutveckling.azure-devices.net;DeviceId=Consoleapp1;SharedAccessKey=KPOX9SdPBKqgchs4elmoQJfyEvHsDVI487txDqW6vzY=");
    private static bool lightState = false;

    public static void Main()
    {
        ConfigureDeviceAsync().ConfigureAwait(false);
        Console.ReadKey();
    }

    public static async Task ConfigureDeviceAsync()
    {
        await _deviceClient.SetMethodHandlerAsync("GetDeviceName", GetDeviceNameAsync, null);
        var twin = _deviceClient.GetTwinAsync();
    }

    public static Task<MethodResponse> GetDeviceNameAsync(MethodRequest methodRequest, object userContext)
    {
        lightState = !lightState;

        System.Console.WriteLine($"Method {methodRequest.Name} has been triggered. LightState is {lightState}");
        return Task.FromResult(new MethodResponse(new byte[0],200));
    }
}