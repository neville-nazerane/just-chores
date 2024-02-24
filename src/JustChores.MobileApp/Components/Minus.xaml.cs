namespace JustChores.MobileApp.Components;

public partial class Minus : Grid
{

	public Minus()
	{
		InitializeComponent();
	}

    protected override void OnSizeAllocated(double width, double height)
    {
        line.X1 = 0;
		line.X1 = width;
		line.Y1 = height / 2;
		line.Y2 = height / 2;
        base.OnSizeAllocated(width, height);
    }

}