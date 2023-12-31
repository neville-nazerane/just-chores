using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.Extensions
{
    public static class SelectionState
    {

        #region Bindable Properties

        public static readonly BindableProperty SelfKeyProperty = BindableProperty.Create("SelfKey",
                                                                                  typeof(string),
                                                                                  typeof(SelectionState),
                                                                                  defaultBindingMode: BindingMode.OneWay,
                                                                                  propertyChanged: SelfKeyChanged);

        public static readonly BindableProperty SelectedKeyProperty = BindableProperty.Create("SelectedKey",
                                                                                              typeof(string),
                                                                                              typeof(SelectionState),
                                                                                              defaultBindingMode: BindingMode.OneWay,
                                                                                              propertyChanged: SelectedKeyChanged);


        #endregion

        #region Get and Set

        public static string GetSelfKey(BindableObject bindable) => (string)bindable.GetValue(SelfKeyProperty);

        public static void SetSelfKey(BindableObject bindable, string value) => bindable.SetValue(SelfKeyProperty, value);

        public static string GetSelectedKey(BindableObject bindable) => (string)bindable.GetValue(SelectedKeyProperty);

        public static void SetSelectedKey(BindableObject bindable, string value) => bindable.SetValue(SelectedKeyProperty, value);

        #endregion

        #region Changed functions
        private static void SelfKeyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Refresh(bindable);
        }

        private static void SelectedKeyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Refresh(bindable);
        }

        #endregion

        static void Refresh(BindableObject bindable)
        {
            var element = (VisualElement)bindable;
            var self = GetSelfKey(bindable);
            var selectedKey = GetSelectedKey(bindable);

            if (self == selectedKey)
                VisualStateManager.GoToState(element, "Selected");
            else VisualStateManager.GoToState(element, "Normal");
        }

    }
}
