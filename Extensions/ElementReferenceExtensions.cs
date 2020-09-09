using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BHub.Lib.Extensions
{
    public static class ElementReferenceExtensions
    {
        /// <summary>
        /// Clear the Input field and add focus to it.
        /// </summary>
        /// <param name="elementRef">The element reference.</param>
        /// <param name="jsRuntime">The js runtime.</param>
        public static async Task ClearInputAndFocusAsync(this ElementReference elementRef, IJSRuntime jsRuntime)
            => await jsRuntime.InvokeVoidAsync("BHubLib.ClearInputAndFocusElement", elementRef);
    }
}