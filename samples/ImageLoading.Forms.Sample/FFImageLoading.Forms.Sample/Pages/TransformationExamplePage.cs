﻿using System;

using Xamarin.Forms;
using DLToolkit.PageFactory;
using FFImageLoading.Forms.Sample.ViewModels;
using FFImageLoading.Forms.Transformations;
using FFImageLoading.Forms.Sample.Models;
using System.Collections.Generic;

namespace FFImageLoading.Forms.Sample.Pages
{
	public class TransformationExamplePage :  PFContentPage<TransformationExampleViewModel>
	{
		public TransformationExamplePage()
		{
			Title = "Transformation Demo";

			var listView = new ListView() {
				HorizontalOptions = LayoutOptions.FillAndExpand, 
				VerticalOptions = LayoutOptions.FillAndExpand,
				ItemsSource = ViewModel.Items,
				ItemTemplate = new DataTemplate(typeof(TransformationExampleCell)),
				HasUnevenRows = false,
				RowHeight = 210,
			};

			Content = listView;
		}

		class TransformationExampleCell : ViewCell
		{
			public TransformationExampleCell()
			{
				var image = new CachedImage() {
					WidthRequest = 200,
					HeightRequest = 200,
					DownsampleHeight = 200,
					TransparencyEnabled = false,
					Aspect = Aspect.AspectFill,
					CacheDuration = TimeSpan.FromDays(30),
					RetryCount = 3,
					RetryDelay = 500,
					LoadingPlaceholder = "loading.png",
					Transformations = new List<IFormsTransformation>() {
						new GrayscaleTransformation(),
						new CircleTransformation(),
					}
				};
				image.SetBinding<ListExampleItem>(CachedImage.SourceProperty, v => v.ImageUrl);

				var fileName = new Label() {
					LineBreakMode = LineBreakMode.CharacterWrap,
					YAlign = TextAlignment.Center,
					XAlign = TextAlignment.Center,
				};
				fileName.SetBinding<ListExampleItem>(Label.TextProperty, v => v.FileName);

				var root = new AbsoluteLayout() {
					HorizontalOptions = LayoutOptions.FillAndExpand,
					VerticalOptions = LayoutOptions.FillAndExpand,
					Padding = 5,
				};

				root.Children.Add(image, new Rectangle(0f, 0f, 200f, 200f));
				root.Children.Add(fileName, new Rectangle(200f, 0f, 150f, 200f));

				View = root;	
			}
		}
	}
}


