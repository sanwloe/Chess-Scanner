using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Scanner.Pattern
{
    internal static class PatternButton
    {
        static public readonly string MenuColorThemeDark = @"<Style TargetType=""Button"" xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
            <Setter Property=""Background"" Value=""#FF626262""></Setter>
            <Setter Property=""Height"" Value=""45""></Setter>
            <Setter Property=""FontSize"" Value=""19""></Setter>
            <Setter Property=""Foreground"" Value=""White""></Setter>
            <Setter Property=""Template"">
                <Setter.Value>
                    <ControlTemplate TargetType=""Button"">
                        <Border Background=""{TemplateBinding Background}"">
                            <ContentPresenter HorizontalAlignment=""Center"" VerticalAlignment=""Center""></ContentPresenter>
                        </Border>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
            <Style.Triggers>
                <Trigger Property=""IsMouseOver"" Value=""True"">
                    <Setter Property=""Background"" Value=""#FF6260DD""></Setter>
                </Trigger>
            </Style.Triggers>
        </Style>";
        static public readonly string MenuColorThemeWhite = @"<Style TargetType=""Button"" xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
            <Setter Property=""BorderBrush"" Value=""Black""></Setter>
            <Setter Property=""Background"" Value=""#FFF6FCF5""></Setter>
            <Setter Property=""FontSize"" Value=""19""></Setter>
            <Setter Property=""Foreground"" Value=""Black""></Setter>
            <Setter Property=""Height"" Value=""45""></Setter>
            <Setter Property=""Template"">
                <Setter.Value>
                    <ControlTemplate TargetType=""Button"">
                        <Border Background=""{TemplateBinding Background}"">
                            <ContentPresenter HorizontalAlignment=""Center"" VerticalAlignment=""Center""></ContentPresenter>
                        </Border>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
            <Style.Triggers>
                <Trigger Property=""IsMouseOver"" Value=""True"">
                    <Setter Property=""Background"" Value=""#FF00FFFF""></Setter>
                </Trigger>
            </Style.Triggers>
        </Style>";
        static public readonly string ColorThemeButtonCloseDark = @"<Style TargetType=""Button"" xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
            <Setter Property=""Background"" Value=""DarkRed""></Setter>
            <Setter Property=""Height"" Value=""45""></Setter>
            <Setter Property=""Template"">
                <Setter.Value>
                    <ControlTemplate TargetType=""Button"">
                        <Border Background=""{TemplateBinding Background}"">
                            <ContentPresenter HorizontalAlignment=""Center"" VerticalAlignment=""Center""></ContentPresenter>
                        </Border>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
            <Style.Triggers>
                <Trigger Property=""IsMouseOver"" Value=""True"">
                    <Setter Property=""Background"" Value=""Red""></Setter>
                </Trigger>
            </Style.Triggers>
        </Style>";
        static public readonly string ColorThemeButtonCloseWhite = @"<Style TargetType=""Button"" xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
            <Setter Property=""Background"" Value=""DarkRed""></Setter>
            <Setter Property=""Height"" Value=""45""></Setter>
            <Setter Property=""Template"">
                <Setter.Value>
                    <ControlTemplate TargetType=""Button"">
                        <Border Background=""{TemplateBinding Background}"">
                            <ContentPresenter HorizontalAlignment=""Center"" VerticalAlignment=""Center""></ContentPresenter>
                        </Border>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
            <Style.Triggers>
                <Trigger Property=""IsMouseOver"" Value=""True"">
                    <Setter Property=""Background"" Value=""Red""></Setter>
                </Trigger>
            </Style.Triggers>
        </Style>";
        static public readonly string ColorThemeButtonHideDark = @"<Style TargetType=""Button"" xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
            <Setter Property=""Background"" Value=""DarkRed""></Setter>
            <Setter Property=""Height"" Value=""45""></Setter>
            <Setter Property=""Template"">
                <Setter.Value>
                    <ControlTemplate TargetType=""Button"">
                        <Border Background=""{TemplateBinding Background}"">
                            <ContentPresenter HorizontalAlignment=""Center"" VerticalAlignment=""Center""></ContentPresenter>
                        </Border>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
            <Style.Triggers>
                <Trigger Property=""IsMouseOver"" Value=""True"">
                    <Setter Property=""Background"" Value=""Red""></Setter>
                </Trigger>
            </Style.Triggers>
        </Style>";
        static public readonly string ColorThemeButtonHideWhite = @"<Style TargetType=""Button"" xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
            <Setter Property=""Background"" Value=""""></Setter>
            <Setter Property=""Height"" Value=""45""></Setter>
            <Setter Property=""Template"">
                <Setter.Value>
                    <ControlTemplate TargetType=""Button"">
                        <Border Background=""{TemplateBinding Background}"">
                            <ContentPresenter HorizontalAlignment=""Center"" VerticalAlignment=""Center""></ContentPresenter>
                        </Border>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
            <Style.Triggers>
                <Trigger Property=""IsMouseOver"" Value=""True"">
                    <Setter Property=""Background"" Value=""Red""></Setter>
                </Trigger>
            </Style.Triggers>
        </Style>";

    }
}
