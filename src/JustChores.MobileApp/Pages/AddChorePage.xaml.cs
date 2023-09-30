namespace JustChores.MobileApp.Pages;

[QueryProperty(nameof(ChoreId), "id")]
public partial class AddChorePage : ContentPage
{

    public int? ChoreId { get; set; }

    public AddChorePage()
	{
		InitializeComponent();
	}

    partial void OnNavigatedToInternal()
    {
        ViewModel.ChoreId = ChoreId;
    }

}