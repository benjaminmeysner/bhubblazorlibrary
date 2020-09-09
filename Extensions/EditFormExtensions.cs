using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace BHub.Lib.Extensions
{
    /// <summary>
    /// Contains extension methods for working with the <see cref="EditForm"/> class.
    /// </summary>
    public static class EditFormExtensions
    {
        /// <summary>
        /// Clears all validation messages from the <see cref="EditContext"/> of the given <see cref="EditForm"/>.
        /// </summary>
        /// <param name="editForm">The <see cref="EditForm"/> to use.</param>
        /// <param name="revalidate">
        /// Specifies whether the <see cref="EditContext"/> of the given <see cref="EditForm"/> should revalidate after all validation messages have been cleared.
        /// </param>
        /// <param name="markAsUnmodified">
        /// Specifies whether the <see cref="EditContext"/> of the given <see cref="EditForm"/> should be marked as unmodified.
        /// This will affect the assignment of css classes to a form's input controls in Blazor.
        /// </param>
        /// <remarks>
        /// This extension method should be on EditContext, but EditForm is being used until the fix for issue
        /// <see href="https://github.com/dotnet/aspnetcore/issues/12238"/> is officially released.
        /// </remarks>
        public static void ClearValidationMessages(this EditForm editForm, bool revalidate = false, bool markAsUnmodified = false)
        {
            var editContext = editForm.EditContext ?? GetInstanceField(typeof(EditForm), editForm, "_fixedEditContext") as EditContext;

            var fieldStates = GetInstanceField(typeof(EditContext), editContext, "_fieldStates");
            var clearMethodInfo = typeof(HashSet<ValidationMessageStore>).GetMethod("Clear", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (DictionaryEntry kv in fieldStates as IDictionary)
            {
                var messageStores = GetInstanceField(kv.Value.GetType(), kv.Value, "_validationMessageStores");
                clearMethodInfo.Invoke(messageStores, null);
            }

            if (markAsUnmodified)
            {
                editContext.MarkAsUnmodified();
            }

            if (revalidate)
            {
                editContext.Validate();
            }
        }

        private static object GetInstanceField(Type type, object instance, string fieldName)
            => type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(instance);
    }
}
