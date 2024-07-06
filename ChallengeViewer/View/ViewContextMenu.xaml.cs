using System.Windows;
using System.Windows.Controls;

namespace ChallengeViewer.View;

public partial class ViewContextMenu : UserControl
{
    public ViewContextMenu()
    {
        InitializeComponent();
    }

    private async void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
    {
        await ((ChallengeView)this.Parent).UpdateChallenges();
    }

    private void SliderOpacity_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        ((ChallengeView)this.Parent).Opacity = e.NewValue;
    }
}