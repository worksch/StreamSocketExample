﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Proximity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace NFC_Example
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        ProximityDevice proximityDevice;
        private async void StartClick(object sender, RoutedEventArgs e)
        {
            string id = ProximityDevice.GetDeviceSelector();
            proximityDevice = ProximityDevice.GetDefault();
            DeviceInformation deviceinformation = null;
            //try
            //{
            //    deviceinformation = await DeviceInformation.CreateFromIdAsync(id);

            //}
            //catch (Exception ex)
            //{
            //    this.OutputString(ex.Message);
            //}
            //if (deviceinformation != null)
            //{
            //    this.OutputString("Devices Found:");
            //    OutputString(deviceinformation.Name);

            //}
            //else
            //{
            //    OutputString("No Device Found");
            //}

            //proximityDevice = ProximityDevice.GetDefault();

            if (proximityDevice == null)
            {
                this.OutputString("Devices not Found");
            }
            else
            {
                proximityDevice.DeviceArrived += DeviceArrived;
                proximityDevice.DeviceDeparted +=DeviceDeparted;
                this.OutputString("Tap to connect ready");
            }

        }

        private void DeviceDeparted(ProximityDevice sender)
        {
            this.OutputString("Our friend left us");
            if (messageId != -1)
	        {
                this.proximityDevice.StopSubscribingForMessage(messageId);
		 
            }
            //throw new NotImplementedException();
        }

        private long messageId = -1;
        private void DeviceArrived(ProximityDevice sender)
        {
            this.OutputString("Someone arrive!!!");
            messageId = this.proximityDevice.SubscribeForMessage("7ungmessage", messageReceivedHandler);
           // throw new NotImplementedException();
        }

        private void messageReceivedHandler(ProximityDevice sender, ProximityMessage message)
        {
            this.OutputString("We have a letter. Sir!");
            this.OutputString("Sender is: " + sender.DeviceId);
            if (message.MessageType == "7ungmessage")
            {
                this.OutputString("He said that: " + message.DataAsString);
            }
        }

        // xuất ra màn hình câu msg
        private void OutputString(string msg)
        {
            this.OutputTextblock.Text += msg + "\n";
        }
    }
}