﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class Cart : INotifyPropertyChanged
    {
        private PoCart _instance;
        public PoCart Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                _instance = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Instance"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

}
