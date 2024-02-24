using Microsoft.Maui.Controls.Shapes;

namespace JustChores.MobileApp.Components;

public partial class Plus : Grid
{
	public Plus()
	{
		InitializeComponent();
	}

    protected override void OnSizeAllocated(double width, double height)
    {
        horizontal.X1 = 0;
        horizontal.X2 = width;
        horizontal.Y1 = height / 2;
        horizontal.Y2 = height / 2;

        vertical.X1 = width / 2;
        vertical.X2 = width / 2;
        vertical.Y1 = 0;
        vertical.Y2 = height;

        base.OnSizeAllocated(width, height);
    }


}