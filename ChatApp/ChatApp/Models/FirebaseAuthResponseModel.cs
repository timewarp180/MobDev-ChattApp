﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChatApp
{
    public class FirebaseAuthResponseModel : INotifyPropertyChanged
    {
        bool _Status { get; set; }
        public bool Status { get { return _Status; } set { _Status = value; OnPropertyChanged(nameof(Status)); } }
        string _Response { get; set; }
        public string Response { get { return _Response; } set { _Response = value; OnPropertyChanged(nameof(Response)); } }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
