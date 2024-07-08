using System.Windows;
using System.Windows.Controls;

namespace ChallengeViewer.View;

public partial class ViewContextMenu : UserControl
{
    private ChallengeView Parent;
    
    public ViewContextMenu(ChallengeView pParent)
    {
        Parent = pParent;
        InitializeComponent();
    }

    private async void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
    {
        await Parent.UpdateChallenges();
    }

    private void SliderOpacity_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Parent.UpdateWindowOpacity(e.NewValue);
    }
}