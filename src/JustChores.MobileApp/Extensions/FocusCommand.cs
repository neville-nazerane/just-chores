using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JustChores.MobileApp.Extensions
{

    public class EntryTrigger : TriggerAction<Entry>
    {



        protected override void Invoke(Entry sender)
        {

        }
    }

    public class FocusCommand : Behavior<Entry>
    {
        public static readonly BindableProperty TriggerCommandProperty = BindableProperty.Create(nameof(TriggerCommand),
                                                                                          typeof(object),
                                                                                          typeof(FocusCommand),
                                                                                          defaultBindingMode: BindingMode.OneWay,
                                                                                          propertyChanged: Sample);

        private static void Sample(BindableObject bindable, object oldValue, object newValue)
        {
        }

        public object TriggerCommand
        {
            get => (object)GetValue(TriggerCommandProperty);
            set => SetValue(TriggerCommandProperty, value);
        }

        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry);
            entry.Focused += OnEntryFocused;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            base.OnDetachingFrom(entry);
            entry.Focused -= OnEntryFocused;
        }

        private void OnEntryFocused(object sender, FocusEventArgs e)
        {
            //TriggerCommand.Execute(null);
            //return;
            //if (TriggerCommand is not null || TriggerCommand?.CanExecute(null) == true)
            //    TriggerCommand.Execute(null);
        }



    }
}
