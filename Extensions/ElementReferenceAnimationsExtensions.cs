using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BHub.Lib.Extensions
{
    public static class ElementReferenceAnimationsExtensions
    {
        /// <summary>
        /// Adds a slide down animation effect to the Element Reference.
        /// </summary>
        /// <param name="elementRef">The element reference.</param>
        /// <param name="jsRuntime">The js runtime.</param>
        public static async Task SlideDownAsync(this ElementReference elementRef, IJSRuntime jsRuntime)
            => await jsRuntime.InvokeVoidAsync("BHubLib.SlideDown", elementRef);
    }
}