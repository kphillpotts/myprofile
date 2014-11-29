using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR.Client;
using Xamarin.Forms;

namespace LetsTalk
{
    public class App
    {
        public static Page GetMainPage()
        {

            
            // Top Section 
            //StackLayout enterMsgStackLayout = new StackLayout {Orientation = StackOrientation.Horizontal, Padding = 5};

            //Entry entryEnterMsg = new Entry
            //{
            //    MinimumHeightRequest = 90,
            //    MinimumWidthRequest = 2500,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    VerticalOptions = LayoutOptions.StartAndExpand,
            //    BackgroundColor = Color.Blue
            //};

            //Button sendButton = new Button
            //{
            //    MinimumWidthRequest = 2500,
            //    MinimumHeightRequest = 90,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    BackgroundColor = Color.Green
            //};

            //enterMsgStackLayout.Children.Add(entryEnterMsg);
            //enterMsgStackLayout.Children.Add(sendButton);
            
            //// Bottom Section
            //Label chatHistoryLabel = new Label();
            //chatHistoryLabel.BackgroundColor = Color.White;
            
            //ScrollView viewMsgScrollView = new ScrollView {Content = chatHistoryLabel};
            

            //StackLayout bodyLayout = new StackLayout
            //{
            //    Orientation = StackOrientation.Vertical,
            //    HorizontalOptions = LayoutOptions.CenterAndExpand,
            //    VerticalOptions = LayoutOptions.CenterAndExpand,
            //    BackgroundColor = Color.White,
            //    Padding = 20
            //};

            //bodyLayout.Children.Add(enterMsgStackLayout);
            //bodyLayout.Children.Add(viewMsgScrollView);

            //GetChatty getChatty = new GetChatty();
            //getChatty.StartListening();

            //return new ContentPage { Content = bodyLayout };
            
            ChatPage chatPage = new ChatPage();
            return chatPage;
        }
    }
}
