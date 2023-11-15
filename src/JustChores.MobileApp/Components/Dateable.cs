using JustChores.MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.Components
{
    public class Dateable : DatePicker
    {

        public Dateable()
        {
            IsVisible = false;
        }

        public static readonly BindableProperty ControlledDateProperty =
            BindableProperty.Create(
                nameof(ControlledDate),
                typeof(AsyncBindable<DateTime>),
                typeof(Dateable),
                null,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: OnControlledDateChanged);

        public AsyncBindable<DateTime> ControlledDate
        {
            get => (AsyncBindable<DateTime>)GetValue(ControlledDateProperty);
            set => SetValue(ControlledDateProperty, value);
        }

        private void OnBegin(object sender, EventArgs e)
        {
            Focus();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent is null)
                Unfocused -= OnUnfocused;
            else
                Unfocused += OnUnfocused;
        }

        private void OnUnfocused(object sender, FocusEventArgs e)
        {
            ControlledDate?.NotifyResponse(Date);
        }

        static void OnControlledDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (Dateable)bindable;
            var oldVal = oldValue as AsyncBindable<DateTime>;
            var newVal = newValue as AsyncBindable<DateTime>;

            if (oldVal is not null)
                oldVal.OnBegin -= control.OnBegin;
            if (newVal is not null)
                newVal.OnBegin += control.OnBegin;
        }

    }
}
