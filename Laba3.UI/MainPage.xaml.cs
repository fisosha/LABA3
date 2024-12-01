using Laba3.Models;

namespace Laba3.UI;

public partial class MainPage : ContentPage
{
    private Schedule _schedule = new Schedule();

    public MainPage()
    {
        InitializeComponent();
        UpdateGrid();
    }

    private void UpdateGrid()
    {
        ScheduleGrid.Clear(); 
        int rowIndex = 1; 

        foreach (var lecture in _schedule.Lectures)
        {
            ScheduleGrid.Add(new Label { Text = lecture.Day }, 0, rowIndex);
            ScheduleGrid.Add(new Label { Text = lecture.Time }, 1, rowIndex);
            ScheduleGrid.Add(new Label { Text = lecture.Lecturer }, 2, rowIndex);
            ScheduleGrid.Add(new Label { Text = lecture.Department }, 3, rowIndex);
            ScheduleGrid.Add(new Label { Text = lecture.Room }, 4, rowIndex);
            ScheduleGrid.Add(new Label { Text = lecture.Students.Count.ToString() }, 5, rowIndex);

            var editButton = new Button { Text = "Редагувати" };
            editButton.Clicked += (s, e) => OnEditLectureClicked(s, e, lecture);
            ScheduleGrid.Add(editButton, 6, rowIndex); 

            var deleteButton = new Button { Text = "Видалити" };
            deleteButton.Clicked += (s, e) => OnDeleteLectureClicked(s, e, lecture); 
            ScheduleGrid.Add(deleteButton, 7, rowIndex); 

            rowIndex++;
        }
    }

    private async void OnOpenJsonClicked(object sender, EventArgs e)
    {
        try
        {
            string filePath = await OpenFileDialog();
            if (!string.IsNullOrEmpty(filePath))
            {
                _schedule = JsonFileManager.LoadSchedule(filePath);
                UpdateGrid();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка", $"Не вдалося відкрити файл: {ex.Message}", "ОК");
        }
    }

    // Зберегти JSON
    private async void OnSaveJsonClicked(object sender, EventArgs e)
    {
        try
        {
            string filePath = await SaveFileDialog();
            if (!string.IsNullOrEmpty(filePath))
            {
                JsonFileManager.SaveSchedule(filePath, _schedule);
                await DisplayAlert("Успіх", "Дані збережено!", "ОК");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка", $"Не вдалося зберегти файл: {ex.Message}", "ОК");
        }
    }

    private static async Task<string> OpenFileDialog()
    {
        var result = await FilePicker.PickAsync(PickOptions.Default);
        return result?.FullPath;
    }

    private static async Task<string> SaveFileDialog()
    {
        var result = await FilePicker.PickAsync(new PickOptions { PickerTitle = "Оберіть файл для збереження" });
        return result?.FullPath;
    }

    private async void OnAddLectureClicked(object sender, EventArgs e)
    {
        var lectureForm = new LectureFormPage();
        await Navigation.PushAsync(lectureForm);

        lectureForm.LectureSaved += (s, lecture) =>
        {
            _schedule.Lectures.Add(lecture);
            UpdateGrid();
        };
    }

    private async void OnEditLectureClicked(object sender, EventArgs e, Lecture lecture)
    {
        var lectureForm = new LectureFormPage(lecture);
        await Navigation.PushAsync(lectureForm);

        lectureForm.LectureSaved += (s, updatedLecture) =>
        {
            var index = _schedule.Lectures.IndexOf(lecture);
            _schedule.Lectures[index] = updatedLecture;
            UpdateGrid();
        };
    }

    private void OnDeleteLectureClicked(object sender, EventArgs e, Lecture lecture)
    {
        _schedule.Lectures.Remove(lecture);
        UpdateGrid();
    }

    private void OnSearchClicked(object sender, EventArgs e)
    {
        string query = SearchEntry.Text?.ToLower() ?? "";
        var filteredLectures = _schedule.Lectures.Where(l =>
            l.Day.ToLower().Contains(query) ||
            l.Lecturer.ToLower().Contains(query) ||
            l.Department.ToLower().Contains(query)).ToList();

        ScheduleGrid.Clear();
        int rowIndex = 1;
        foreach (var lecture in filteredLectures)
        {
            ScheduleGrid.Add(new Label { Text = lecture.Day }, 0, rowIndex);
            ScheduleGrid.Add(new Label { Text = lecture.Time }, 1, rowIndex);
            ScheduleGrid.Add(new Label { Text = lecture.Lecturer }, 2, rowIndex);
            ScheduleGrid.Add(new Label { Text = lecture.Department }, 3, rowIndex);
            ScheduleGrid.Add(new Label { Text = lecture.Room }, 4, rowIndex);
            ScheduleGrid.Add(new Label { Text = lecture.Students.Count.ToString() }, 5, rowIndex);
            rowIndex++;
        }
    }

    private async void OnAboutClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Про програму", "Автор: Софія Шаламай\nКурс: 2\nГрупа: K-23\nРік: 2024\nПроект для роботи з даними поданими у форматі JSON\nПредметна область: Розклад занять", "OK");
    }
}