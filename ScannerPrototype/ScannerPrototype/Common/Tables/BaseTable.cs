using SQLite;
using System;
using System.ComponentModel;

namespace ScannerPrototype.Common.Tables
{
    public abstract class BaseTable : ITable, INotifyPropertyChanged
    {
        [PrimaryKey]
        public int Id { get; set; }

        private DateTime _creationDateTime { get; set; }

        public DateTime CreationDateTime
        {
            get
            {
                return _creationDateTime;
            }
            set
            {
                _creationDateTime = value;
                OnPropertyChanged(nameof(CreationDateTime));
            }
        }

        private DateTime _mutationDateTime { get; set; }

        public DateTime MutationDateTime
        {
            get
            {
                return _mutationDateTime;
            }
            set
            {
                _mutationDateTime = value;
                OnPropertyChanged(nameof(MutationDateTime));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
