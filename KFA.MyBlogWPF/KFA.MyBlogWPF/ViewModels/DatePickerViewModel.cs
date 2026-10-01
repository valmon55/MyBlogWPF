using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.ViewModels
{
    public class DatePickerViewModel : ViewModelBase
    {
        // ─── Списки для ComboBox ─────────────────────────────────────────

        public ObservableCollection<int> Days { get; }
        public ObservableCollection<string> Months { get; }
        public ObservableCollection<int> Years { get; }

        // ─── Выбранные значения ─────────────────────────────────────────

        private int? _selectedDay;
        public int? SelectedDay
        {
            get => _selectedDay;
            set
            {
                if (SetField(ref _selectedDay, value))
                {
                    UpdateDaysInMonth();
                    OnPropertyChanged(nameof(SelectedDate));
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        private string? _selectedMonth;
        public string? SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                if (SetField(ref _selectedMonth, value))
                {
                    UpdateDaysInMonth();
                    OnPropertyChanged(nameof(SelectedDate));
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        private int? _selectedYear;
        public int? SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (SetField(ref _selectedYear, value))
                {
                    UpdateDaysInMonth();
                    OnPropertyChanged(nameof(SelectedDate));
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        // ─── Вычисляемые свойства ───────────────────────────────────────

        /// <summary>
        /// Итоговая выбранная дата
        /// </summary>
        public DateTime? SelectedDate
        {
            get
            {
                if (SelectedDay == null || SelectedMonth == null || SelectedYear == null)
                    return null;

                var monthIndex = Months.IndexOf(SelectedMonth) + 1;

                try
                {
                    return new DateTime(SelectedYear.Value, monthIndex, SelectedDay.Value);
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                if (value.HasValue)
                {
                    SelectedYear = value.Value.Year;
                    SelectedMonth = Months[value.Value.Month - 1];
                    SelectedDay = value.Value.Day;
                }
                else
                {
                    SelectedYear = null;
                    SelectedMonth = null;
                    SelectedDay = null;
                }
            }
        }

        /// <summary>
        /// Валидна ли выбранная дата
        /// </summary>
        public bool IsValid => SelectedDate.HasValue;

        // ─── Конструктор ────────────────────────────────────────────────

        public DatePickerViewModel(DateTime? initialDate = null)
        {
            // Месяцы
            Months = new ObservableCollection<string>
            {
                "Январь", "Февраль", "Март", "Апрель",
                "Май", "Июнь", "Июль", "Август",
                "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
            };

            // Годы (от 1900 до текущего)
            var currentYear = DateTime.Now.Year;
            Years = new ObservableCollection<int>(
                Enumerable.Range(1900, currentYear - 1900 + 1).Reverse()
            );

            // Дни (по умолчанию 31, потом пересчитается)
            Days = new ObservableCollection<int>(
                Enumerable.Range(1, 31)
            );

            // Устанавливаем начальную дату
            if (initialDate.HasValue)
            {
                SelectedDate = initialDate.Value;
            }
        }

        // ─── Вспомогательные методы ─────────────────────────────────────

        /// <summary>
        /// Пересчитывает количество дней в выбранном месяце
        /// </summary>
        private void UpdateDaysInMonth()
        {
            if (SelectedMonth == null || SelectedYear == null)
            {
                // Если месяц не выбран — показываем все 31 день
                if (Days.Count != 31)
                {
                    Days.Clear();
                    for (int i = 1; i <= 31; i++)
                        Days.Add(i);
                }
                return;
            }

            var monthIndex = Months.IndexOf(SelectedMonth) + 1;
            var daysInMonth = DateTime.DaysInMonth(SelectedYear.Value, monthIndex);

            // Если текущий выбранный день больше, чем дней в месяце — сбрасываем
            if (SelectedDay > daysInMonth)
            {
                SelectedDay = daysInMonth;
            }

            // Перестраиваем список дней
            if (Days.Count != daysInMonth)
            {
                Days.Clear();
                for (int i = 1; i <= daysInMonth; i++)
                    Days.Add(i);
            }
        }
    }
}
