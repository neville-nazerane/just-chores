using CommunityToolkit.Maui.Markup;
using JustChores.MobileApp.Helpers;
using JustChores.MobileApp.Models;

namespace JustChores.MobileApp.Components;

public partial class FrequencySelectionComponent : Border
{
    #region Bindable Properties

    public static readonly BindableProperty SelectedFrequencyProperty = BindableProperty.Create(nameof(SelectedFrequency),
                                                                                                typeof(FrequencyType),
                                                                                                typeof(FrequencySelectionComponent),
                                                                                                defaultBindingMode: BindingMode.TwoWay,
                                                                                                propertyChanged: OnSelectedFrequencyChanged);

    public static readonly BindableProperty SelfFrequencyProperty = BindableProperty.Create(nameof(SelfFrequency),
                                                                                            typeof(FrequencyType),
                                                                                            typeof(FrequencySelectionComponent),
                                                                                            defaultBindingMode: BindingMode.OneWay,
                                                                                            propertyChanged: OnSelfFrequencyChanged);

    #endregion
    
    public FrequencyType SelectedFrequency
    {
        get => (FrequencyType)GetValue(SelectedFrequencyProperty);
        set => SetValue(SelectedFrequencyProperty, value);
    }

    public FrequencyType SelfFrequency
    {
        get => (FrequencyType)GetValue(SelfFrequencyProperty);
        set => SetValue(SelfFrequencyProperty, value);
    }

    public FrequencySelectionComponent()
	{
		InitializeComponent();
	}

    private void SelectedFrequencyChanged(FrequencyType _)
    {
        Refresh();
    }

    private void SelfFrequencyChanged(FrequencyType newValue)
    {
        lbl.Text = newValue.ToString();
        Refresh();
    }


    private void Tapped(object sender, TappedEventArgs e)
    {
        SelectedFrequency = SelfFrequency;
    }

    void Refresh()
    {
        if (SelectedFrequency == SelfFrequency)
        {
            BackgroundColor = MauiUtils.GetResource<Color>("Primary");
            Stroke = BackgroundColor;
            lbl.TextColor = MauiUtils.GetResource<Color>("White");
        }
        else
        {
            this.AppThemeBinding(BackgroundColorProperty,
                                 MauiUtils.GetResource<Color>("White"),
                                 MauiUtils.GetResource<Color>("DarkBackground"));

            this.AppThemeBinding(StrokeProperty,
                                 MauiUtils.GetResource<Color>("DescriptionText"),
                                 MauiUtils.GetResource<Color>("DescriptionTextDark"));

            lbl.AppThemeBinding(Label.TextColorProperty,
                                MauiUtils.GetResource<Color>("DescriptionText"),
                                MauiUtils.GetResource<Color>("DescriptionTextDark"));

        }
    }


    #region static onchanged

    private static void OnSelectedFrequencyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((FrequencySelectionComponent)bindable).SelectedFrequencyChanged((FrequencyType)newValue);
    }

    private static void OnSelfFrequencyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((FrequencySelectionComponent)bindable).SelfFrequencyChanged((FrequencyType)newValue);
    }


    #endregion

}