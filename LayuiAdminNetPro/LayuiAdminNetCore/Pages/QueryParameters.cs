using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LayuiAdminNetCore.Pages
{
    public class QueryParameters : INotifyPropertyChanged
    {
        private const int DefaultPageSize = 10;
        private const int DefaultMaxPageSize = 50;
        private const int DefaultOffset = 0;

        #region OffSet

        private int _offSet = DefaultOffset;

        public int OffSet
        {
            get => _offSet;
            set => _offSet = value;
        }

        #endregion OffSet

        #region PageIndex

        private int _pageIndex;

        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value >= 0 ? value : 1;
        }

        #endregion PageIndex

        #region PageSize

        private int _pageSize = DefaultPageSize;

        public virtual int PageSize
        {
            get => _pageSize;
            set => SetField(ref _pageSize, value);
        }

        #endregion PageSize

        #region OrderBy

        private string? _orderBy;

        public string? OrderBy
        {
            get => _orderBy;
            set => _orderBy = value; // = value ?? "id"; //空合并运算符
        }

        #endregion OrderBy

        #region MaxPageSize

        private int _maxPageSize = DefaultMaxPageSize;

        protected internal virtual int MaxPageSize
        {
            get => _maxPageSize;
            set => SetField(ref _maxPageSize, value);
        }

        #endregion MaxPageSize

        public string? Fields { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);
            if (propertyName == nameof(PageSize) || propertyName == nameof(MaxPageSize))
            {
                SetPageSize();
            }
            return true;
        }

        private void SetPageSize()
        {
            if (_maxPageSize <= 0)
            {
                _maxPageSize = DefaultMaxPageSize;
            }
            if (_pageSize <= 0)
            {
                _pageSize = DefaultPageSize;
            }
            _pageSize = _pageSize > _maxPageSize ? _maxPageSize : _pageSize;
        }
    }
}