


using CommunityToolkit.Maui.Markup;
using JustChores.MobileApp.Helpers;

namespace JustChores.MobileApp.Components;

public partial class ThemeButtonComponent : Border
{

	#region BindableProperties

	public static readonly BindableProperty TextProperty = BindableProperty.Create(
																	nameof(Text),
																	typeof(string),
																	typeof(ThemeButtonComponent),
																	defaultBindingMode: BindingMode.OneWay,
																	propertyChanged: TextChanged);

	public static readonly BindableProperty IconImageProperty = BindableProperty.Create(
																					nameof(IconImage),
																					typeof(string),
																					typeof(ThemeButtonComponent),
																					defaultBindingMode: BindingMode.OneWay,
																					propertyChanged: IconImageChanged);

	public static readonly BindableProperty SelectedThemeProperty = BindableProperty.Create(
																						nameof(SelectedTheme),
																						typeof(AppTheme),
																						typeof(ThemeButtonComponent),
																						defaultBindingMode: BindingMode.TwoWay,
																						propertyChanged: SelectedThemeChanged);

	public static readonly BindableProperty SelfThemeProperty = BindableProperty.Create(
																					nameof(SelfTheme),
																					typeof(AppTheme),
																					typeof(ThemeButtonComponent),
																					defaultBindingMode: BindingMode.OneWay,
																					propertyChanged: SelfThemeChanged);

	#endregion

	#region Properties

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}

	public string IconImage
	{
		get => (string)GetValue(IconImageProperty);
		set => SetValue(IconImageProperty, value);
	}

	public AppTheme SelectedTheme
	{
		get => (AppTheme)GetValue(SelectedThemeProperty);
		set => SetValue(SelectedThemeProperty, value);
	}

    public AppTheme SelfTheme
    {
        get => (AppTheme)GetValue(SelfThemeProperty);
        set => SetValue(SelfThemeProperty, value);
    }

    #endregion


    public ThemeButtonComponent()
	{
		InitializeComponent();
	}

	void SetText(string text)
	{
		titleLbl.Text = text;
	}

	void SetIconImage(string image)
	{
		icon.SetAppTheme(Image.SourceProperty,
						 ImageSource.FromFile($"{image}_dark.png"),
						 ImageSource.FromFile($"{image}_light.png"));
		//icon.Source = ImageSource.FromFile($"{image}.png");
	}

	void SetSelectedTheme(AppTheme _) => Recheck();

	void SetSelfTheme(AppTheme _) => Recheck();

	void Recheck()
	{
		if (SelfTheme == SelectedTheme)
		{
			var btnColor = MauiUtils.GetResource<Color>("ButtonColor");
			border.Stroke = btnColor;
			titleLbl.TextColor = btnColor;
			icon.Source = ImageSource.FromFile($"{IconImage}_selected.png");
        }
        else
        {
			SetIconImage(IconImage);
            border.Stroke = Colors.Transparent;
			titleLbl.AppThemeBinding(Label.TextColorProperty, Colors.Black, Colors.White);
        }
    }

    private void Tapped(object sender, TappedEventArgs e)
    {
		SelectedTheme = SelfTheme;
    }

    #region Changed

    private static void TextChanged(BindableObject bindable, object oldValue, object newValue) => ((ThemeButtonComponent)bindable).SetText((string)newValue);

	private static void IconImageChanged(BindableObject bindable, object oldValue, object newValue) => ((ThemeButtonComponent)bindable).SetIconImage((string)newValue);

	private static void SelectedThemeChanged(BindableObject bindable, object oldValue, object newValue) => ((ThemeButtonComponent)bindable).SetSelectedTheme((AppTheme)newValue);

    private static void SelfThemeChanged(BindableObject bindable, object oldValue, object newValue) => ((ThemeButtonComponent)bindable).SetSelfTheme((AppTheme)newValue);


    #endregion


}