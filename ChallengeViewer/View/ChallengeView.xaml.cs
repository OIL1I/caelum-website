using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChallengeViewer.Models;
using System.Windows.Threading;

namespace ChallengeViewer.View;

public partial class ChallengeView : Window
{
    private Winchallenge _winchallenge;

    public ChallengeView(Winchallenge pWinchallenge)
    {
        InitializeComponent();
        _winchallenge = pWinchallenge;
        this.SizeToContent = SizeToContent.WidthAndHeight;
        StackPanel stackPanel = this.StackPanel1;

        //DragMove
        this.MouseLeftButtonDown += (_, _) => this.DragMove();
        
        //ContextMenu
        var contextMenu = new ContextMenu();
        var menuItem = new MenuItem();
        menuItem.Header = "Refresh";
        menuItem.Style = this.FindResource("NoHighlightMenuItem") as Style;
        menuItem.Width = 100;
        contextMenu.Items.Add(menuItem);

        menuItem = new MenuItem();
        var viewContextMenu = new ViewContextMenu(this);
        menuItem.Header = viewContextMenu;
        menuItem.Style = this.FindResource("NoHighlightMenuItem") as Style;
        contextMenu.Items.Add(menuItem);
        
        this.ContextMenu = contextMenu;
        
        //Timer
        DispatcherTimer timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(20)
        };
        timer.Tick += UpdateChallengesOnTick;
        timer.Start();

        //Challenge Name
        stackPanel.Children.Add(new TextBlock()
        {
            Margin = new Thickness(10, 10, 25, 0),
            Text = _winchallenge.Name,
            FontSize = 20,
            Foreground = new SolidColorBrush(Colors.White),
            FontWeight = FontWeight.FromOpenTypeWeight(700)
        });

        //Challenges
        stackPanel.Children.Add(new TextBlock()
        {
            Margin = new Thickness(10, 20, 35, 0),
            Text = "Challenges: ",
            Foreground = new SolidColorBrush(Colors.White),
            FontSize = 17
        });

        //Challenges List
        foreach (var challenge in _winchallenge.Challenges)
        {
            var textBlock = new TextBlock
            {
                Text = $"{challenge.Name} ({challenge.Completions}/{challenge.RequiredCompletions})",
                Margin = new Thickness(10, 20, 0, 0),
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.White)
            };
            stackPanel.Children.Add(textBlock);
        }

        //Buttons 
        // Button Stackpane
        var buttonStackpanel = new StackPanel()
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom
        };
        StackPanel1.Children.Add(buttonStackpanel);
        
        //back button
        var backButton = new Button
        {
            Width = 30,
            Height = 30,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom,
            BorderBrush = Brushes.Transparent,
            Background = Brushes.Transparent,
            Margin = new Thickness(10, 15, 0, 10),
            Style = NoHoverStyleButton
        };
        var backImage = new Image
        {
            Source = (BitmapImage)FindResource("BackIcon"),
            Stretch = Stretch.Uniform
        };
        backButton.Content = backImage;
        backButton.Click += (_, _) => { this.Close(); };
        buttonStackpanel.Children.Add(backButton);
    }

    private Style NoHoverStyleButton
    {
        get
        {
            //ButtonStyle
            Style noHoverStyle = new Style(typeof(Button));

            ControlTemplate template = new ControlTemplate(typeof(Button));
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Border));
            factory.Name = "buttonBorder";
            factory.SetBinding(Border.BackgroundProperty,
                new Binding("Background") { RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent) });
            factory.SetValue(Border.BorderBrushProperty, Brushes.Transparent);

            FrameworkElementFactory contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.SetBinding(ContentPresenter.ContentProperty,
                new Binding("Content") { RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent) });
            contentPresenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);

            factory.AppendChild(contentPresenter);
            template.VisualTree = factory;

            noHoverStyle.Setters.Add(new Setter(Button.TemplateProperty, template));

            return noHoverStyle;
        }
    }

    // private Style NoHoverStyleMenuItem
    // {
    //     get
    //     {
    //         Style noHoverStyle = new Style { TargetType = typeof(MenuItem) };
    //
    //         noHoverStyle.Setters.Add(new Setter(MenuItem.TemplateProperty, new ControlTemplate(typeof(MenuItem))
    //         {
    //             VisualTree = new FrameworkElementFactory(typeof(ContentPresenter))
    //             {
    //                 Name = "MenuItemContent",
    //                 Settdoesers =
    //                 {
    //                     new Setter(ContentPresenter.ContentProperty, new Binding { })
    //                 }
    //             }
    //         }
    //         // mouseOverTrigger.Setters.Add(backgroundSetter);
    //         // mouseOverTrigger.Setters.Add(borderSetter);
    //         // noHoverStyle.Triggers.Add(mouseOverTrigger);
    //
    //         return noHoverStyle;
    //     }
    // }

    private void UpdateChallengesOnTick(object? sender, EventArgs e)
    {
        UpdateChallenges();
    }

    public async Task UpdateChallenges()
    {
        //TODO: Add Logic for "stoped sharing" Challenge
        _winchallenge =
            await new HttpClient().GetFromJsonAsync<Winchallenge>(
                $"https://oil1i-caelum.de/API/GetChallenge/{this._winchallenge.ShareCode}");
        if (_winchallenge != null)
        {
            int i = 2;
            foreach (var challenge in _winchallenge.Challenges)
            {
                if (StackPanel1.Children[i] is TextBlock textBlock)
                {
                    if (challenge.Completions == challenge.RequiredCompletions)
                    {
                        //TODO: Add TextDecoration "StrikeThrough"
                    }

                    textBlock.Text = $"{challenge.Name} ({challenge.Completions}/{challenge.RequiredCompletions})";
                }

                i++;
            }
        }
        else
        {
            this.Close();
        }
    }

    public void UpdateWindowOpacity(double NewValue)
    {
        this.BackgroundBorder.Background.Opacity = NewValue is >= 0.0d and <= 1.0d ? NewValue : 0.3d;
    }
}