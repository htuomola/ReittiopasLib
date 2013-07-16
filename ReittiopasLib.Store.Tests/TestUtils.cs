using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace ReittiopasLib.Store.Tests
{
    static internal class TestUtils
    {
        internal static async Task<string> GetMockResponse(string fileName)
        {
            var installedFolder = Package.Current.InstalledLocation;
            var file = await installedFolder.GetFileAsync("MockResponses\\" + fileName);
            return await FileIO.ReadTextAsync(file);
        }
    }
}